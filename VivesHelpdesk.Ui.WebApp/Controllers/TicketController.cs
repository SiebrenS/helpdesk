using Microsoft.AspNetCore.Mvc;
using VivesHelpdesk.Services.Model;
using VivesHelpdesk.Services.Model.Request;
using VivesHelpdesk.Ui.WebApp.Sdk;

namespace VivesHelpdesk.Ui.WebApp.Controllers
{
    public class TicketController : Controller
    {
        private readonly TicketSdk _ticketSdk;
        private readonly PersonSdk _personSdk;

        public TicketController(
            PersonSdk personSdk,
            TicketSdk ticketSdk)
        {
            _ticketSdk = ticketSdk;
            _personSdk = personSdk;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? assignedToId)
        {
            if (assignedToId.HasValue)
            {
                var assignedToPerson = await _personSdk.Get(assignedToId.Value);

                if (assignedToPerson is not null)
                {
                    //ViewBag.AssignedToPerson = assignedToPerson;
                    ViewData["AssignedToPerson"] = assignedToPerson;
                }
            }
            
            var tickets = await _ticketSdk.Find(assignedToId);

            return View(tickets);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return await GetCreateEditView("Create");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]TicketRequest request)
        {
            //Validate
            if (!ModelState.IsValid)
            {
                return await GetCreateEditView("Create", request);
            }

            await _ticketSdk.Create(request);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute]int id)
        {
            var ticket = await _ticketSdk.Get(id);

            if(ticket is null)
            {
                return RedirectToAction("Index");
            }

            var request = new TicketRequest
            {
                Title = ticket.Title,
                Author = ticket.Author,
                AssignedToId = ticket.AssignedToId,
                Description = ticket.Description
            };

            return await GetCreateEditView("Edit", request);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute]int id, [FromForm]TicketRequest request)
        {
            //Validate
            if (!ModelState.IsValid)
            {
                return await GetCreateEditView(nameof(Edit), request);
            }

            await _ticketSdk.Update(id, request);

            return RedirectToAction("Index");

        }

        private async Task<IActionResult> GetCreateEditView(string viewName, TicketRequest? request = null)
        {
            var people = await _personSdk.Find();
            ViewBag.People = people;
            return View(viewName, request);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var ticket = await _ticketSdk.Get(id);

            return View(ticket);
        }

        [HttpPost]
        [Route("[controller]/Delete/{id:int?}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _ticketSdk.Get(id);

            if(ticket is null)
            {
                return RedirectToAction("Index");
            }

            await _ticketSdk.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
