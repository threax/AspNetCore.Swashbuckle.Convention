using Microsoft.EntityFrameworkCore;

namespace DevApp.Database
{
    public partial class AppDbContext
    {
        public DbSet<ValueEntity> Values { get; set; }
    }
}
