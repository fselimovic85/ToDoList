using TO_DO_LIST.Models;

namespace TO_DO_LIST.Service.MailSenderService
{
    public interface IEmailSender
    {
        void SendEmail(Message message);
    }
}
