using Microsoft.EntityFrameworkCore;
using System;
using Threax.AspNetCore.Models;
using Threax.AspNetCore.UserBuilder.Entities;

namespace DevApp.Database
{
    /// <summary>
    /// By default the app db context extends the UsersDbContext from Authorization. 
    /// This gives it the Users, Roles and UsersToRoles tables.
    /// </summary>
    public partial class AppDbContext : UsersDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        //The dbset declarations are in the other parial classes. Expand the AppDbContext.cs class node to see them.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Use the reflected index finder to lookup entities with [IndexProp] attributes on them
            var propFinder = new IndexedPropertyFinder(this.GetType(), new Type[] { typeof(DbSet<>) });
            foreach(var indexable in propFinder.GetIndexProps())
            {
                modelBuilder.Entity(indexable.Type)
                    .HasIndex(indexable.PropertyInfo.Name)
                        .ForSqlServerIsClustered(indexable.IndexAttribute.IsClustered)
                        .IsUnique(indexable.IndexAttribute.IsUnique);
            }
        }
    }
}
