using VivesHelpdesk.Data;
using VivesHelpdesk.Model;
using VivesHelpdesk.Services.Model.Request;
using VivesHelpdesk.Services.Model.Result;

namespace VivesHelpdesk.Services
{
    public class TicketService
    {
        private readonly VivesHelpdeskDbContext _dbContext;

        public TicketService(VivesHelpdeskDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Find
        public IList<TicketResult> Find(int? id = null)
        {
            var query = _dbContext.Tickets.AsQueryable();

            if (id.HasValue)
            {
                query = query.Where(t => t.AssignedToId == id);
            }

            return query
                .Select(t => new TicketResult
                {
                    Id = t.Id,
                    Title = t.Title,
                    Author = t.Author,
                    Description = t.Description,
                    CreatedDate = t.CreatedDate,
                    AssignedToId = t.AssignedToId,
                    AssignedToFirstName = t.AssignedTo.FirstName,
                    AssignedToLastName = t.AssignedTo.LastName
                })
                .ToList();
        }

        //Get
        public TicketResult? Get(int id)
        {
            return _dbContext.Tickets
                .Select(t => new TicketResult
                {
                    Id = t.Id,
                    Title = t.Title,
                    Author = t.Author,
                    Description = t.Description,
                    CreatedDate = t.CreatedDate,
                    AssignedToId = t.AssignedToId,
                    AssignedToFirstName = t.AssignedTo.FirstName,
                    AssignedToLastName = t.AssignedTo.LastName
                })
                .SingleOrDefault(p => p.Id == id);
        }

        //Create
        public TicketResult? Create(TicketRequest request)
        {
            var ticket = new Ticket
            {
                Title = request.Title,
                Author = request.Author,
                Description = request.Description,
                AssignedToId = request.AssignedToId,
                CreatedDate = DateTime.UtcNow
            };
            

            _dbContext.Add(ticket);
            _dbContext.SaveChanges();

            return Get(ticket.Id);
        }

        //Update
        public TicketResult? Update(int id, TicketRequest request)
        {
            var ticket = _dbContext.Tickets
                .SingleOrDefault(p => p.Id == id);
            if (ticket == null)
            {
                return null;
            }

            ticket.Title = request.Title;
            ticket.Description = request.Description;
            ticket.Author = request.Author;
            ticket.AssignedToId = request.AssignedToId;

            _dbContext.SaveChanges();

            return Get(id);
        }

        //Delete
        public void Delete(int id)
        {
            var dbTicket = _dbContext.Tickets
                .SingleOrDefault(p => p.Id == id);
            if (dbTicket == null)
            {
                return;
            }

            _dbContext.Tickets.Remove(dbTicket);
            _dbContext.SaveChanges();
        }

    }
}
