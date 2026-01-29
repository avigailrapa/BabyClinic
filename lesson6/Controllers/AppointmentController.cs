using Common.Dto;
using lesson6.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Repository.Interfaces;
using Service.Interfaces;



namespace lesson6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {

		private readonly IService<AppointmentDto> service;
		public AppointmentController(IService<AppointmentDto>service)
        {
            this.service = service;
        }
        // GET: api/<AppointmentController>
        [HttpGet]
		public async Task<List<AppointmentDto>> Get()
		{
			return await service.GetAll();
		}


		// GET api/<AppointmentController>/5
		[HttpGet("{id}")]
		public async Task<AppointmentDto> Get(int id)
		{
			return await service.GetById(id);
		}

		// POST api/<AppointmentController>
		[HttpPost]
        public Task<AppointmentDto> Post([FromForm] AppointmentDto appointment)
        {
          return  service.AddItem(appointment);
        }

        // PUT api/<AppointmentController>/5
        [HttpPut("{id}")]
        public async Task<AppointmentDto> Put(int id, [FromBody] AppointmentDto appointment)
        {

            return await service.UpdateItem(id, appointment);
        }

        // DELETE api/<AppointmentController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
           await service.DeleteItem(id);

        }
    }
}

