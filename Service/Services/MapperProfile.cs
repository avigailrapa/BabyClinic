using AutoMapper;
using Common.Dto;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class MapperProfile:Profile
    {
        string path=Directory.GetCurrentDirectory()+"images/";
        public MapperProfile()
        {          //source  //dest
            CreateMap<Baby,BabyDto>().ForMember(dest => dest.ArrImage, src => src.MapFrom(s => Encoding.UTF8.GetBytes(path+s.ImageUrl)));
			CreateMap<Baby, BabyDto>().ReverseMap();
            CreateMap<Nurse, NurseDto>().ReverseMap();
			CreateMap<Appointment, AppointmentDto>().ReverseMap();



		}
	}
}
