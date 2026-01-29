using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lesson6.Interfaces;
using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class AppointmentRepository : IRepository<Appointment>
    {
        private readonly IContext ctx;
        public AppointmentRepository(IContext context)
        {
            ctx = context;
        }
        public async Task <Appointment> AddItem(Appointment appointment)
        {
            ctx.Appointments.AddAsync(appointment);
            ctx.Save();
            return appointment;
        }

        public async Task DeleteItem(int id)
        {
            var a = ctx.Appointments.FirstOrDefault(x => x.Id == id);
            ctx.Appointments.Remove(a);
            await ctx.Save();
        }

        public async Task<List<Appointment>> GetAll()
        {
            return await ctx.Appointments.ToListAsync();
        }

        public async Task<Appointment> GetById(int id)
        {
            return await ctx.Appointments.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Appointment> UpdateItem(int id, Appointment appointment)
        {
            var a = ctx.Appointments.FirstOrDefault(x => x.Id == id);
            a.NurseId = appointment.NurseId;
            a.BabyId = appointment.BabyId;
            a.Date = appointment.Date;
            a.Description = appointment.Description;
            await ctx.Save();
            return a;
        }
    }
}
