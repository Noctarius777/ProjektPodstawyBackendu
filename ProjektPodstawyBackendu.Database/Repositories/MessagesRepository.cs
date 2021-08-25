using Microsoft.EntityFrameworkCore;
using ProjektPodstawyBackendu.Domain;
using System.Collections.Generic;
using System.Linq;

namespace ProjektPodstawyBackendu.Database.Repositories
{
    public class MessagesRepository : IMessagesRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private DbSet<MessageEntity> Messages { get; set; }

        public MessagesRepository(ApplicationDbContext dbContext) // poprosimy o ApplicationDbContext
        {
            _dbContext = dbContext;  
            Messages = dbContext.Message;
        }
        public List<MessageEntity> GetAll()
        {
            return Messages.ToList();
        }
        public bool Add(MessageEntity message)
        {
            Messages.Add(message);
            return _dbContext.SaveChanges() > 0;
        }
        public bool Delete(MessageEntity message)
        {
            Messages.Remove(message);
            return _dbContext.SaveChanges() > 0;

        }
    }
}
