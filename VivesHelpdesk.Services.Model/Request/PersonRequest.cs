using System.ComponentModel.DataAnnotations;

namespace VivesHelpdesk.Services.Model.Request
{
    public class PersonRequest
    {
        [Required]
        public required string FirstName { get; set; }
       
        [Required] 
        public required string LastName { get; set; }
    }
}
