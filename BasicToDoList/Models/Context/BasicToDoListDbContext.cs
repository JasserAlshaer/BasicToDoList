using BasicToDoList.Models.Entity;
using BasicToDoList.Models.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace BasicToDoList.Models.Context
{
    public class BasicToDoListDbContext : DbContext
    {
        public BasicToDoListDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MissionEntityTypeConfiguration());
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Mission> Missions { get; set; }
        public virtual DbSet<MissionStatus> MissionStatus { get; set; }
    }
}
