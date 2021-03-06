using System.Net.Http;
using System.Threading.Tasks;
using stiebel_eltron_apiserver.Web;
using Xunit;

namespace stiebel_eltron_apiserver.FunctionalTests
{
    public class HomeControllerIndex : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public HomeControllerIndex(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task ReturnsViewWithCorrectMessage()
        {
            HttpResponseMessage response = await _client.GetAsync("/");
            response.EnsureSuccessStatusCode();
            string stringResponse = await response.Content.ReadAsStringAsync();

            Assert.Contains("stiebel_eltron_apiserver.Web", stringResponse);
        }
    }
}
