using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VivesHelpdesk.Model;

[Table(nameof(Ticket))]
public class Ticket
{
    public int Id { get; set; }

    public required string Title { get; set; }

    public required string Description { get; set; }

    public required string Author { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? AssignedToId { get; set; }
    public Person AssignedTo { get; set; } = null!;
}