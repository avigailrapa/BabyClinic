using AutoMapper;
using Common;
using Common.Dto;
using Repository.Entities;
using Repository.Interfaces;
using Service.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Services
{
	public class BabyService : IService<BabyDto>, IsExist<BabyDto>
	{
		private readonly IRepository<Baby> repository;
		private readonly IMapper mapper;

		public BabyService(IRepository<Baby> repository, IMapper map)
		{
			this.repository = repository;
			this.mapper = map;
		}

		public async Task<List<BabyDto>> GetAll()
		{
			var babies = await repository.GetAll();       // מחכים קודם
			return mapper.Map<List<BabyDto>>(babies);     // ממפים את הרשימה עצמה
		}

		public async Task<BabyDto> GetById(int id)
		{
			var baby = await repository.GetById(id);
			return mapper.Map<BabyDto>(baby);
		}

		public async Task<BabyDto> AddItem(BabyDto item)
		{
			var entity = await repository.AddItem(mapper.Map<Baby>(item));
			return mapper.Map<BabyDto>(entity);
		}

		public async Task<BabyDto> UpdateItem(int id, BabyDto item)
		{
			var updated = await repository.UpdateItem(id, mapper.Map<Baby>(item));
			return mapper.Map<BabyDto>(updated);
		}

		public async Task DeleteItem(int id)
		{
			await repository.DeleteItem(id);
		}

		public BabyDto Exist(Login login)
		{ 
			var baby = GetAll().Result.FirstOrDefault(x => x.Name == login.UserName && x.Id == login.Password); 
			if (baby != null) return baby;
			return null;
		}

        
    }
}
