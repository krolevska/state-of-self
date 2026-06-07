using HealthTrackerLocal.Models;
using HealthTrackerLocal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HealthTrackerLocal.Pages;

public class IndexModel : PageModel
{
    private readonly GoogleSheetsService _googleSheetsService;

    public IndexModel(GoogleSheetsService googleSheetsService)
    {
        _googleSheetsService = googleSheetsService;
    }

    [BindProperty]
    public HealthEntry Entry { get; set; } = new();

    public bool Saved { get; set; }

    public void OnGet()
    {
        Entry.Date = DateTime.Today;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await _googleSheetsService.AppendHealthEntryAsync(Entry);

        Saved = true;
        Entry = new HealthEntry
        {
            Date = DateTime.Today
        };

        return Page();
    }
}