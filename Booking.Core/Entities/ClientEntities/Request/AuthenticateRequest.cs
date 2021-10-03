using System.ComponentModel.DataAnnotations;

namespace Booking.Core.Entities.ClientEntities.Request
{
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}