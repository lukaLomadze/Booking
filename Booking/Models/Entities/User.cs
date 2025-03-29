using Booking.Enums;
using System.Data;

namespace Booking.Models.Entities
{
    public class User : BaseClass
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpirationDate { get; set; }
        public Status Status { get; set; } = Status.Active;
        public Role Role { get; set; }
        public string email { get; set; }

    }
}
