using Common.Dto;
using Microsoft.Extensions.DependencyInjection;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public static class ExtensionService
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddRepository();
            services.AddScoped<IService<BabyDto>, BabyService>();
			services.AddScoped<IsExist<BabyDto>, BabyService>();
            services.AddScoped<IService<NurseDto>, NurseService>();
			services.AddScoped<IsExist<NurseDto>, NurseService>();
            services.AddScoped<IService<AppointmentDto>, AppointmentService>();


            return services;
        }
    }
}
