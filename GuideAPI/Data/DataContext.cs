using GuideAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GuideAPI.Data
{
    public class DataContext:DbContext
    {
       
       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=A15-FA507R;Database=dbo;User Id=TestAdmin;Password=123456;TrustServerCertificate=true");
        }


        
        public DbSet<City> Cities { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
