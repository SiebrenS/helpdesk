using System.ComponentModel.DataAnnotations;

namespace VivesHelpdesk.Services.Model.Request
{
    public class TicketRequest
    {
        [Required]
        public required string Title { get; set; }

        [Required]
        public required string Description { get; set; }

        [Required]
        public required string Author { get; set; }

        public int? AssignedToId { get; set; }
    }
}
