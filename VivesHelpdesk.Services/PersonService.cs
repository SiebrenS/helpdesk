using VivesHelpdesk.Data;
using VivesHelpdesk.Model;
using VivesHelpdesk.Services.Model.Request;
using VivesHelpdesk.Services.Model.Result;

namespace VivesHelpdesk.Services
{
    public class PersonService
    {
        private readonly VivesHelpdeskDbContext _dbContext;

        public PersonService(VivesHelpdeskDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Find
        public IList<PersonResult> Find()
        {
            return  _dbContext.People
                .Select(p => new PersonResult
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    NumberOfAssignedTickets = p.AssignedTickets.Count
                })
                .ToList();
        }

        //Get
        public PersonResult? Get(int id)
        {
            return _dbContext.People
                .Select(p => new PersonResult
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    NumberOfAssignedTickets = p.AssignedTickets.Count
                })
                .SingleOrDefault(p => p.Id == id);
        }

        //Create
        public PersonResult? Create(PersonRequest request)
        {
            var person = new Person
            {
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            _dbContext.Add(person);
            _dbContext.SaveChanges();

            return Get(person.Id);
        }

        //Update
        public PersonResult? Update(int id, PersonRequest request)
        {
            var person = _dbContext.People
                .SingleOrDefault(p => p.Id == id);
            if (person == null)
            {
                return null;
            }

            person.FirstName = request.FirstName;
            person.LastName = request.LastName;

            _dbContext.SaveChanges();

            return Get(id);
        }

        //Delete
        public void Delete(int id)
        {
            var person = _dbContext.People
                .SingleOrDefault(p => p.Id == id);
            if (person == null)
            {
                return;
            }

            _dbContext.People.Remove(person);
            _dbContext.SaveChanges();
        }

    }
}
