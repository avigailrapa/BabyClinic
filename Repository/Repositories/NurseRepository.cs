using System;
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
    public class NurseRepository : IRepository<Nurse>
    {
        private readonly IContext ctx;
        public NurseRepository(IContext context)
        {
           ctx = context;

        }
		public async Task<Nurse> AddItem(Nurse item)
		{
			await ctx.Nurses.AddAsync(item);
			await ctx.Save();
			return item;

		}

		public async Task DeleteItem(int id)
		{
			var n = ctx.Nurses.FirstOrDefault(x => x.Id == id);
			ctx.Nurses.Remove(n);
			await ctx.Save();
		}

		public async Task<List<Nurse>> GetAll()
		{
			return await ctx.Nurses.ToListAsync();
		}

		public async Task<Nurse> GetById(int id)
		{
			return await ctx.Nurses.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task <Nurse> UpdateItem(int id, Nurse nurse)
        {
            var n = await ctx.Nurses.FirstOrDefaultAsync(x => x.Id == id);
            n.Id = nurse.Id;
            n.Name = nurse.Name;
            n.Appointment = nurse.Appointment;
            await ctx.Save();
            return n;
        }
    }
}
