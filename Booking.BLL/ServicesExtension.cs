using Microsoft.Extensions.DependencyInjection;
using Booking.BLL.Business.Abstractions;
using Booking.BLL.Business.Implementations;

namespace Booking.BLL
{
    public static class ServicesExtension
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IRoomService, RoomService>();
            services.AddTransient<IBookingReservationService, BookingReservationService>();
            services.AddTransient<IEmailHelper, EmailHelper>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IUserService, UserService>();
        }
    }
}
