namespace VivesHelpdesk.Services.Model.Result
{
    public class TicketResult
    {
        public int Id { get; set; }

        public required string Title { get; set; }

        public required string Description { get; set; }

        public required string Author { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? AssignedToId { get; set; }

        public string? AssignedToFirstName { get; set; }

        public string? AssignedToLastName { get; set;}
    }
}
