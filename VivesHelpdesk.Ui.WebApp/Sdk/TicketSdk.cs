using VivesHelpdesk.Services.Model.Request;
using VivesHelpdesk.Services.Model.Result;

namespace VivesHelpdesk.Ui.WebApp.Sdk
{
    public class TicketSdk
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TicketSdk(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IList<TicketResult>> Find(int? assignedToId = null)
        {
            var client = _httpClientFactory.CreateClient("HelpdeskApi");
            var route = "/api/tickets";
            if (assignedToId is not null)
            {
                route = $"{route}/{assignedToId}";
            }
            var response = await client.GetAsync(route);

            response.EnsureSuccessStatusCode();

            var tickets = await response.Content.ReadFromJsonAsync<IList<TicketResult>>();

            if (tickets is null)
            {
                return new List<TicketResult>();
            }

            return tickets;
        }

        public async Task<TicketResult?> Get(int id)
        {
            var client = _httpClientFactory.CreateClient("HelpdeskApi");
            var route = $"/api/tickets/{id}";
            var response = await client.GetAsync(route);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<TicketResult>();
        }

        public async Task<TicketResult?> Create(TicketRequest request)
        {
            var client = _httpClientFactory.CreateClient("HelpdeskApi");
            var route = "/api/tickets";
            var response = await client.PostAsJsonAsync(route, request);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<TicketResult>();
        }

        public async Task<TicketResult?> Update(int id, TicketRequest request)
        {
            var client = _httpClientFactory.CreateClient("HelpdeskApi");
            var route = $"/api/tickets/{id}";
            var response = await client.PutAsJsonAsync(route, request);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<TicketResult>();
        }

        public async Task Delete(int id)
        {
            var client = _httpClientFactory.CreateClient("HelpdeskApi");
            var route = $"/api/tickets/{id}";
            var response = await client.DeleteAsync(route);

            response.EnsureSuccessStatusCode();
        }
    }
}
