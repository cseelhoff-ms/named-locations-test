using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Web;
using System.Net;
using Microsoft.Graph;

namespace named_locations_test.Pages
{
    [AuthorizeForScopes(ScopeKeySection = "MicrosoftGraph:Scopes")]
    public class IndexModel : PageModel
    {
        private readonly GraphServiceClient _graphServiceClient;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, GraphServiceClient graphServiceClient)
        {
            _logger = logger;
            _graphServiceClient = graphServiceClient;;
            Locations = [];
        }

        public List<NamedLocation> Locations { get; private set; }

        public async Task OnGet()
        {
            IConditionalAccessRootNamedLocationsCollectionPage result = await _graphServiceClient.Identity.ConditionalAccess.NamedLocations.Request().GetAsync();
            Locations = result.ToList();
        }
    }
}
