using BookingRoomAPI.Application.Service.Interfaces;
using BookingRoomAPI.Application.Service.Services;
using BookingRoomAPI.Domain.Interfaces;
using BookingRoomAPI.Domain.Interfaces.Base;
using BookingRoomAPI.Infrastructure;
using BookingRoomAPI.Infrastructure.Repositories;
using BookingRoomAPI.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingRoomAPI.Application.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure DbContext with Scoped lifetime
            #region InMemory
            // InMemory
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("Booking");
            }, ServiceLifetime.Transient);
            #endregion

            #region SQL Server
            //SQL Server
            //services.AddDbContext<AppDbContext>(options =>
            //{
            //    options.UseSqlServer(configuration.GetConnectionString("Default"),
            //        x => x.MigrationsAssembly("BookingRoomAPI"))
            //    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            //}, ServiceLifetime.Transient
            //);

            //services.AddScoped<Func<AppDbContext>>((provider) => () => provider.GetService<AppDbContext>());
            #endregion

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped(typeof(IRepository<>), typeof(Repository<>))
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IBookingRepository, BookingRepository>()
                .AddScoped<IRoomRepository, RoomRepository>();
        }

        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddScoped<IUserService, UserService>()
                .AddScoped<IBookingService, BookingService>()
                .AddScoped<IRoomService, RoomService>();
        }
    }
}
