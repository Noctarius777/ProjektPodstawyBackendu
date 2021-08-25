namespace ProjektPodstawyBackendu.Domain
{
    public interface IEmailService
    {
        void SendMessageEmail(string email, string message);
    }
}
