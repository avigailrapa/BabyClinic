
using Microsoft.Extensions.DependencyInjection;
using Repository.Entities;
using Repository.Interfaces;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public static class ExtensionRepository
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Baby>, BabyRepository>();
			services.AddScoped<IRepository<Nurse>, NurseRepository>();
			services.AddScoped<IRepository<Appointment>, AppointmentRepository>();

			return services;
        }
    }
}
