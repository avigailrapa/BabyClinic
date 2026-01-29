using lesson6.Interfaces;
using Microsoft.EntityFrameworkCore;
using Repository.Entities;

namespace lesson6.model
{
    public class BabyClinicContext:DbContext,IContext
    {
        private readonly string connection;
        public BabyClinicContext(string connection)
        {
            this.connection=connection;
        }
        public virtual DbSet<Baby> Babies { get; set; }
        public virtual DbSet<Nurse> Nurses { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
      

        public async Task Save()
        {
           await SaveChangesAsync();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           optionsBuilder.UseSqlServer(connection);
        }
           
        }
}

