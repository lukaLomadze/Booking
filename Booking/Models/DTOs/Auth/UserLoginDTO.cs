﻿namespace Booking.Models.DTOs
{
    public class UserLoginDTO
    {
        public string UserName { get; set; }
        public string  Password { get; set; }
        public bool StaySignedIn { get; set; }



    }
}
