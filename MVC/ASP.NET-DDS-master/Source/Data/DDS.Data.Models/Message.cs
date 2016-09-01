namespace DDS.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using DDS.Data.Common.Models;

    public class Message : BaseModel<int>
    {
        public string SenderUserId { get; set; }

        public string ResieverUserId { get; set; }

        public Diploma SelectedDiploma { get; set; }

        [Required]
        public string MessageSend { get; set; }

        public bool IsRead { get; set; }
    }
}
