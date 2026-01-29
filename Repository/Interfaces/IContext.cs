using Microsoft.EntityFrameworkCore;
using Repository.Entities;

namespace lesson6.Interfaces
{
    public interface IContext
    {
        public DbSet<Nurse> Nurses { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Baby> Babies { get; set; }
        public Task Save();

    }
}
