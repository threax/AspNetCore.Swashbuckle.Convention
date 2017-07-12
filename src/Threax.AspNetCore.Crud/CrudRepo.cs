using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Threax.AspNetCore.Halcyon.Ext;

namespace Threax.AspNetCore.Crud
{
    /// <summary>
    /// This class can provide the basic crud operations for entities in a database. By implementing this class
    /// and providing a couple of functions that are abstract you can have a generic way of doing crud without
    /// writing a lot of code.
    /// How models move between the model types depends on how you setup automapper. Your repository subclases
    /// can also override any of the main functions if more cusomization of their behavior is needed.
    /// </summary>
    /// <typeparam name="Key">The key type for the entity.</typeparam>
    /// <typeparam name="Query">TH</typeparam>
    /// <typeparam name="InputModel"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="ViewModel"></typeparam>
    /// <typeparam name="ViewModelCollection"></typeparam>
    /// <typeparam name="CrudDbContext"></typeparam>
    public abstract class CrudRepo<Key, Query, InputModel, TEntity, ViewModel, ViewModelCollection, CrudDbContext> : ICrudRepo<Key, Query, InputModel, ViewModel, ViewModelCollection>
        where TEntity : class
        where CrudDbContext : DbContext
        where Query : PagedCollectionQuery
    {
        private CrudDbContext dbContext;
        private IMapper mapper;

        public CrudRepo(CrudDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        /// <summary>
        /// Check to see if this repository has any values at all.
        /// </summary>
        /// <returns>True if there are some values, false if not.</returns>
        public virtual async Task<bool> HasValues()
        {
            return await Entities.CountAsync() > 0;
        }

        /// <summary>
        /// Add a new item to the repository.
        /// </summary>
        /// <param name="value">The value to add.</param>
        /// <returns>The newly added value.</returns>
        public virtual async Task<ViewModel> Add(InputModel value)
        {
            var entity = CreateEntity(value);
            this.dbContext.Add(entity);
            await this.dbContext.SaveChangesAsync();
            return CreateResult(entity);
        }

        /// <summary>
        /// Add multiple new items to the repository.
        /// </summary>
        /// <param name="values">The values to add.</param>
        /// <returns>The newly added value.</returns>
        public virtual async Task AddRange(IEnumerable<InputModel> values)
        {
            var entities = values.Select(i => CreateEntity(i));
            this.dbContext.AddRange(entities);
            await this.dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Delete an entity by id.
        /// </summary>
        /// <param name="id">The id of the entity to delete.</param>
        /// <returns>Task</returns>
        public virtual async Task Delete(Key id)
        {
            var entity = await this.Entity(id);
            if (entity != null)
            {
                Entities.Remove(entity);
                await this.dbContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Get an individual entity by id.
        /// </summary>
        /// <param name="id">The id of the entity to lookup.</param>
        /// <returns>The entity or null if it does not exist.</returns>
        public virtual async Task<ViewModel> Get(Key id)
        {
            var entity = await this.Entity(id);
            return CreateResult(entity);
        }

        /// <summary>
        /// List the items by page with no other query. Can override to customize, call the base version for a plain listing.
        /// There are also 2 functions CustomizeListBeforeCountAndSkip and CustomizeListAfterSkip that can customize the base
        /// class query for simple things like search without having to override this function.
        /// </summary>
        /// <param name="query">The query to execute.</param>
        /// <returns>A ViewModelCollection of the results.</returns>
        public virtual async Task<ViewModelCollection> List(Query query)
        {
            IQueryable<TEntity> dbQuery = CustomizeListBeforeCountAndSkip(query, this.Entities);
            var total = await dbQuery.CountAsync();
            dbQuery = CustomizeListAfterSkip(query, dbQuery.Skip(query.SkipTo(total)).Take(query.Limit));
            var resultQuery = dbQuery.Select(i => CreateResult(i));
            var results = await resultQuery.ToListAsync();

            return CreateCollection(query, total, results);
        }

        /// <summary>
        /// Update the value. The id passed to the funciton will be used to lookup the entity. Following the convention your mapper will
        /// not automatically map the id from the value to the entity, but if it does you could potentially override the entity's id if the
        /// input model's id does not match the passed in id. Be careful of this.
        /// </summary>
        /// <param name="id">The id of the value to update. No matter what is set as value's id this is the entity that will be updated.</param>
        /// <param name="value">The value to update.</param>
        /// <returns></returns>
        public virtual async Task<ViewModel> Update(Key id, InputModel value)
        {
            var entity = await this.Entity(id);
            if (entity != null)
            {
                mapper.Map<InputModel, TEntity>(value, entity);
                await this.dbContext.SaveChangesAsync();
                return CreateResult(entity);
            }
            throw new KeyNotFoundException($"Cannot find item {id.ToString()}");
        }

        /// <summary>
        /// Customize the query before taking the total and skipping to the appropriate page.
        /// </summary>
        /// <param name="query">The query object passed to the repo.</param>
        /// <param name="dbQuery">The database query to customize.</param>
        /// <returns>The updated query.</returns>
        protected virtual IQueryable<TEntity> CustomizeListBeforeCountAndSkip(Query query, IQueryable<TEntity> dbQuery)
        {
            return dbQuery;
        }

        /// <summary>
        /// Customize the query after the skip call and total have been executed.
        /// </summary>
        /// <param name="query">The query object passed to the repo.</param>
        /// <param name="dbQuery">The database query to customize.</param>
        /// <returns>The updated query.</returns>
        protected virtual IQueryable<TEntity> CustomizeListAfterSkip(Query query, IQueryable<TEntity> dbQuery)
        {
            return dbQuery;
        }

        protected virtual TEntity CreateEntity(InputModel input)
        {
            return mapper.Map<TEntity>(input);
        }

        /// <summary>
        /// Customize the mapping to the result by overriding this method.
        /// </summary>
        /// <param name="dbQuery">The query for the entities.</param>
        /// <returns>The query that selects the view model.</returns>
        protected virtual ViewModel CreateResult(TEntity entity)
        {
            return mapper.Map<ViewModel>(entity);
        }

        /// <summary>
        /// Get the db context passed to the constructor.
        /// </summary>
        protected virtual CrudDbContext DbContext
        {
            get
            {
                return dbContext;
            }
        }

        /// <summary>
        /// The mapper for this repo.
        /// </summary>
        public IMapper Mapper
        {
            get
            {
                return mapper;
            }
        }

        /// <summary>
        /// Create the final ViewModelCollection instance.
        /// </summary>
        /// <param name="query">The original query.</param>
        /// <param name="total">The total number of items.</param>
        /// <param name="items">The items.</param>
        /// <returns>A new instance of ViewModelCollection.</returns>
        protected abstract ViewModelCollection CreateCollection(Query query, int total, IEnumerable<ViewModel> items);

        /// <summary>
        /// Get the set of all entities to read.
        /// </summary>
        protected abstract DbSet<TEntity> Entities
        {
            get;
        }

        /// <summary>
        /// Get an individual entity specified by key. If the entity does not exist return null.
        /// </summary>
        /// <param name="key">The key of the entity to look up.</param>
        /// <returns>The found entity or null.</returns>
        protected abstract Task<TEntity> Entity(Key key);
    }
}
