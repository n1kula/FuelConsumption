using System.Data.Entity;

namespace XML
{
    internal class CarDB : DbContext
    {
        public DbSet<Car> Cars { get; set; }
    }
}
