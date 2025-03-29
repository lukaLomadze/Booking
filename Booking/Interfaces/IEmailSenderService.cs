namespace Booking.Interfaces
{
    public interface IEmailSenderService
    {
        public Task SendEmail(string mail, string subject, string body);
    }
}
