namespace DDS.Services.Data
{
    using DDS.Data.Common;
    using DDS.Data.Models;
    using Interfaces;

    public class MessagesService : BaseService<Message>, IMessagesService
    {
        public MessagesService(IDbRepository<Message> messages)
            : base(messages)
        {
        }
    }
}
