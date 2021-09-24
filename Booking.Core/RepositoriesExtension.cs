using Booking.Core.Data.Repository.Abstractions;
using Booking.Core.Data.Repository.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Booking.Core.Data
{
    public static class RepositoriesExtension
    {
        public static void RegisterRepos(this IServiceCollection services)
        {
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IRoomRepository, RoomRepository>();
            services.AddTransient<IBookingReservationRepository, BookingReservationRepository>();
        }
    }
}
