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
	internal class AppointmentService : IService<AppointmentDto>
	{
		private readonly IRepository<Appointment> repository;
		private readonly IMapper mapper;

		public AppointmentService(IRepository<Appointment> repository, IMapper mapper)
		{
			this.repository = repository;
			this.mapper = mapper;
		}

		public async Task<List<AppointmentDto>> GetAll()
		{
			var appointments = await repository.GetAll();
			return mapper.Map<List<AppointmentDto>>(appointments);
		}

		public async Task<AppointmentDto> GetById(int id)
		{
			var appointment = await repository.GetById(id);
			return mapper.Map<AppointmentDto>(appointment);
		}

		public async Task<AppointmentDto> AddItem(AppointmentDto item)
		{
			var entity = await repository.AddItem(mapper.Map<Appointment>(item));
			return mapper.Map<AppointmentDto>(entity);
		}

		public async Task<AppointmentDto> UpdateItem(int id, AppointmentDto item)
		{
			var updated = await repository.UpdateItem(id, mapper.Map<Appointment>(item));
			return mapper.Map<AppointmentDto>(updated);
		}

		public async Task DeleteItem(int id)
		{
			await repository.DeleteItem(id);
		}
	}
}