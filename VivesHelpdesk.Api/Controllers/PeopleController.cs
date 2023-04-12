using Microsoft.AspNetCore.Mvc;
using VivesHelpdesk.Services;
using VivesHelpdesk.Services.Model.Request;

namespace VivesHelpdesk.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly PersonService _personService;

        public PeopleController(PersonService personService)
        {
            _personService = personService;
        }

        //Find: /api/people
        [HttpGet]
        public IActionResult Find()
        {
            var people = _personService.Find();
            return Ok(people);
        }

        //Get: /api/people/1
        [HttpGet("{id:int}")]
        public IActionResult Get([FromRoute]int id)
        {
            var people = _personService.Get(id);
            return Ok(people);
        }

        //Create
        [HttpPost]
        public IActionResult Create([FromBody]PersonRequest request)
        {
            var createdPerson = _personService.Create(request);
            return Ok(createdPerson);
        }

        //Edit
        [HttpPut("{id:int}")]
        public IActionResult Update([FromRoute]int id, [FromBody] PersonRequest request)
        {
            var createdPerson = _personService.Update(id, request);
            return Ok(createdPerson);
        }

        //Delete
        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _personService.Delete(id);
            return Ok();
        }
    }
}
