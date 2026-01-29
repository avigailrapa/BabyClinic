using Microsoft.AspNetCore.Http;

namespace Common.Dto
{
    public class BabyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BornDate { get; set; }
        public int Weight { get; set; }
		public IFormFile? ImageFile { get; set; }

		public byte[]? ArrImage { get; set; }
       

      

    }
}
