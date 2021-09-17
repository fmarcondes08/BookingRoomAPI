using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using BookingRoomAPI.Application.Extensions;
using BookingRoomAPI.Application.Mappers;
using BookingRoomAPI.Domain.Models;
using BookingRoomAPI.Domain.Models.Enums;
using BookingRoomAPI.Domain.Interfaces;

namespace BookingRoomAPI
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Booking Room API",
                    Version = "v1",
                    Description = "A simple example in how Developing an API using DDD and the Microsoft technologies.",
                    Contact = new OpenApiContact
                    {
                        Name = "Fabricio Marcondes Santos",
                        Email = "marcondes.fabricio@gmailcom",
                        Url = new Uri("https://www.linkedin.com/in/fabriciomarcondes/"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri("https://choosealicense.com/licenses/mit/")
                    }
                });


                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services
                .AddDatabase(_configuration)
                .AddRepositories()
                .AddServices(_configuration);

            services.AddAutoMapper(x => x.AddProfile(new ModelMapper()));
            services.AddAutoMapper(x => x.AddProfile(new DtoMapper()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IRoomRepository repository)
        {
            #region Room Test InMemory Data
            var room = new Room
            {
                Id = new Guid("a58e6fd0-b5a5-4677-bba2-ed57acbed9f8"),
                Active = true,
                Created_At = new DateTime(2021, 9, 13, 12, 5, 40, 870, DateTimeKind.Local).AddTicks(1636),
                Deleted_At = null,
                Description = "Room for Rent",
                Number = 1,
                Type = RoomType.Standard,
                Updated_At = new DateTime(2021, 9, 13, 12, 5, 40, 871, DateTimeKind.Local).AddTicks(2762)
            };

            repository.Add(room);

            #endregion

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Booking Room API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
