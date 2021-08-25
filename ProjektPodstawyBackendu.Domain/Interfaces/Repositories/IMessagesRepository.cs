using System.Collections.Generic;

namespace ProjektPodstawyBackendu.Domain
{
    public interface IMessagesRepository
    {
        List<MessageEntity> GetAll();
        bool Add(MessageEntity message);
        bool Delete(MessageEntity message);
        
    }
}