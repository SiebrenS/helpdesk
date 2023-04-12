namespace VivesHelpdesk.Services.Model.Result
{
    public class PersonResult
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public int NumberOfAssignedTickets { get; set; }
    }
}
