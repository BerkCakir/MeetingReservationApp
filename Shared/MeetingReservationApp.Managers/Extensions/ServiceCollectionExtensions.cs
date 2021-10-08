using MeetingReservationApp.Data.Abstract;
using MeetingReservationApp.Data.Concrete;
using MeetingReservationApp.Data.Concrete.EntityFramework.Contexts;
using MeetingReservationApp.Managers.Abstract;
using MeetingReservationApp.Managers.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingReservationApp.Managers.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadMyServices(this IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddDbContext<MeetingReservationAppContext>(opt => opt.UseSqlServer(connectionString));
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();

            return serviceCollection;
        }
    }
}
