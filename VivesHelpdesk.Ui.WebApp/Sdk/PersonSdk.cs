using VivesHelpdesk.Services.Model.Request;
using VivesHelpdesk.Services.Model.Result;

namespace VivesHelpdesk.Ui.WebApp.Sdk
{
    public class PersonSdk
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PersonSdk(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IList<PersonResult>> Find()
        {
            var client = _httpClientFactory.CreateClient("HelpdeskApi");
            var route = "/api/people";
            var response = await client.GetAsync(route);

            response.EnsureSuccessStatusCode();

            var people = await response.Content.ReadFromJsonAsync<IList<PersonResult>>();
            
            if (people is null)
            {
                return new List<PersonResult>();
            }

            return people;
        }

        public async Task<PersonResult?> Get(int id)
        {
            var client = _httpClientFactory.CreateClient("HelpdeskApi");
            var route = $"/api/people/{id}";
            var response = await client.GetAsync(route);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<PersonResult>();
        }

        public async Task<PersonResult?> Create(PersonRequest person)
        {
            var client = _httpClientFactory.CreateClient("HelpdeskApi");
            var route = "/api/people";
            var response = await client.PostAsJsonAsync(route, person);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<PersonResult>();
        }

        public async Task<PersonResult?> Update(int id, PersonRequest person)
        {
            var client = _httpClientFactory.CreateClient("HelpdeskApi");
            var route = $"/api/people/{id}";
            var response = await client.PutAsJsonAsync(route, person);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<PersonResult>();
        }

        public async Task Delete(int id)
        {
            var client = _httpClientFactory.CreateClient("HelpdeskApi");
            var route = $"/api/people/{id}";
            var response = await client.DeleteAsync(route);

            response.EnsureSuccessStatusCode();
        }
    }
}
