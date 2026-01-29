using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Common;
using Common.Dto;
using lesson6.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repository.Entities;
using Service.Interfaces;


namespace lesson6.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class BabyController : ControllerBase
    {
        private IConfiguration configuration;
        private readonly IService<BabyDto> service;
        private readonly IsExist<BabyDto> isExist;
        public BabyController(IService<BabyDto> service,IConfiguration configuration, IsExist<BabyDto> isExist)
        {
            this.service = service;
            this.configuration = configuration;
            this.isExist = isExist;
        }
        [HttpPost("login")]
        public string Login([FromBody]Login login)
        {
            BabyDto b=isExist.Exist(login);
            if(b!=null)
                return GenerateToken(b);
            return "User does not exist";
        }

		// GET: api/<BabyController>
		[HttpGet]
		[Authorize(Roles = "user")]
		public async Task<List<BabyDto>> Get()
		{
			return await service.GetAll();
		}


		// GET api/<BabyController>/5
		[HttpGet("{id}")]
		public async Task<BabyDto> Get(int id)
		{
			return await service.GetById(id);
		}


		// POST api/<BabyController>
		[HttpPost]
		public Task<BabyDto> Post([FromForm] BabyDto baby)
		{
			var path = Path.Combine(Environment.CurrentDirectory, "images/", baby.ImageFile.FileName);
			using (FileStream fs = new FileStream(path, FileMode.Create))
			{
				baby.ImageFile.CopyTo(fs);
				fs.Close();
			}
			return service.AddItem(baby);
		}

		// PUT api/<BabyController>/5
		[HttpPut("{id}")]
		public async Task<BabyDto> Put(int id, [FromBody] BabyDto baby)
		{
			return await service.UpdateItem(id, baby);
		}


		// DELETE api/<BabyController>/5
		[HttpDelete("{id}")]
        public async Task Delete(int id)
        {
          
            await service.DeleteItem(id);
        }

		private string GenerateToken(BabyDto b)
		{
			var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
			var claims = new[] {
			new Claim(ClaimTypes.Name,b.Name),
			new Claim("weight",b.Weight.ToString()),
            new Claim(ClaimTypes.Role,"user"),
             };
			var token = new JwtSecurityToken(configuration["Jwt:Issuer"], configuration["Jwt:Audience"],
				claims,
				expires: DateTime.Now.AddMinutes(15),
				signingCredentials: credentials);
			return new JwtSecurityTokenHandler().WriteToken(token);
		}

	}
}
