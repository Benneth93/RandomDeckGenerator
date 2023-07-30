using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RandomDeckGenerator.Services;

namespace RandomDeckGenerator.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    public bool AzureStub { private set;  get; }

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        AzureStub = AppSettingsService._stubs.AzureFileServiceStub;
    }
}