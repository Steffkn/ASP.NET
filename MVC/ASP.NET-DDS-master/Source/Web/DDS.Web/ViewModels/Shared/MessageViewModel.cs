namespace DDS.Web.ViewModels.Shared
{
    using System;
    using AutoMapper;
    using DDS.Data.Models;
    using DDS.Web.Infrastructure.Mapping;

    public class MessageViewModel : IMapFrom<Message>
    {
        public string SenderUserId { get; set; }

        public string ResieverUserId { get; set; }

        public ApplicationUser SenderUser { get; set; }

        public ApplicationUser ResieverUser { get; set; }

        public Diploma SelectedDiploma { get; set; }

        public string MessageSend { get; set; }

        public bool IsRead { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
