
using lesson6.Interfaces;
using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class BabyRepository : IRepository<Baby>
    {
        private readonly IContext ctx;
        public BabyRepository(IContext context)
        {
            ctx = context;
        }
        public async Task <Baby> AddItem(Baby item)
        {
            ctx.Babies.AddAsync(item);
            await ctx.Save();
            return item;

        }

        public async Task DeleteItem(int id)
        {
            var b = ctx.Babies.FirstOrDefault(x => x.Id == id);
            ctx.Babies.Remove(b);
            await ctx.Save();
        }

        public async Task <List<Baby>> GetAll()
        {
            return await ctx.Babies.ToListAsync();
        }

        public async Task<Baby> GetById(int id)
        {
            return await ctx.Babies.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task <Baby> UpdateItem(int id, Baby baby)
        {
            var b =await ctx.Babies.FirstOrDefaultAsync(x => x.Id == id);
            b.Name = baby.Name;
            b.Weight = baby.Weight;
            b.BornDate = baby.BornDate;
            await ctx.Save();
            return b;
        }
    }
}
