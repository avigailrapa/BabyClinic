using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Common;
using Common.Dto;
using Repository.Entities;
using Repository.Interfaces;
using Service.Interfaces;

	namespace Service.Services
	{
		public class NurseService : IService<NurseDto>, IsExist<NurseDto>
		{
			private readonly IRepository<Nurse> repository;
			private readonly IMapper mapper;

			public NurseService(IRepository<Nurse> repository, IMapper mapper)
			{
				this.repository = repository;
				this.mapper = mapper;
			}

			public async Task<List<NurseDto>> GetAll()
			{
				var nurses = await repository.GetAll();
				return mapper.Map<List<NurseDto>>(nurses);
			}

			public async Task<NurseDto> GetById(int id)
			{
				var nurse = await repository.GetById(id);
				return mapper.Map<NurseDto>(nurse);
			}

			public async Task<NurseDto> AddItem(NurseDto item)
			{
				var entity = await repository.AddItem(mapper.Map<Nurse>(item));
				return mapper.Map<NurseDto>(entity);
			}

			public async Task<NurseDto> UpdateItem(int id, NurseDto item)
			{
				var updated = await repository.UpdateItem(id, mapper.Map<Nurse>(item));
				return mapper.Map<NurseDto>(updated);
			}

			public async Task DeleteItem(int id)
			{
				await repository.DeleteItem(id);
			}


			public NurseDto Exist(Login login)
		    {
			var nurse = GetAll().Result.FirstOrDefault(x => x.Name == login.UserName && x.Id == login.Password);
			if (nurse != null)
				return nurse;
			return null;
		    }
	}
}
