using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Common;
using Common.Dto;
using lesson6.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.IdentityModel.Tokens;
using Repository.Entities;
using Repository.Interfaces;
using Service.Interfaces;


namespace lesson6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NurseController : ControllerBase
    {
		private IConfiguration configuration;
		private readonly IService<NurseDto> servise;
        private readonly IsExist<NurseDto> isExist;
        public NurseController(IService<NurseDto> service, IConfiguration configuration, IsExist<NurseDto> isExist)
        {
            this.configuration= configuration;
            this.servise= service;
            this.isExist= isExist;
        }
		[HttpPost("login")]
		public string Login([FromBody] Login login)
		{
			NurseDto n = isExist.Exist(login);
			if (n!= null)
				return GenerateToken(n);
			return "User does not exist";
		}


        // GET: api/<NurseController>
        [HttpGet]
		[Authorize(Roles = "nurse")]
		public async Task<List<NurseDto>> Get()
		{
			return await servise.GetAll();
		}


		// GET api/<NurseController>/5
		[HttpGet("{id}")]
		public async Task<NurseDto> Get(int id)
		{
			return await servise.GetById(id);
		}

		// POST api/<NurseController>
		[HttpPost]
		public Task<NurseDto> Post([FromForm] NurseDto nurse)
		{
			return servise.AddItem(nurse);
		}

		// PUT api/<NurseController>/5
		[HttpPut("{id}")]
		public async Task<NurseDto> Put(int id, [FromBody] NurseDto nurse)
		{
			return await servise.UpdateItem(id, nurse);
		}

		// DELETE api/<NurseController>/5
		[HttpDelete("{id}")]
		public async Task Delete(int id)
		{
			await servise.DeleteItem(id);
		}

		private string GenerateToken(NurseDto n)
		{
			var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
			var credentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
			var claims = new[] {
			new Claim(ClaimTypes.Name,n.Name),
			new Claim(ClaimTypes.Role,"nurse"),
			 };
			var token = new JwtSecurityToken(configuration["Jwt:Issuer"], configuration["Jwt:Audience"],
				claims,
				expires: DateTime.Now.AddMinutes(15),
				signingCredentials: credentials);
			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
