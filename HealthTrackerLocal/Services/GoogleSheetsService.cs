using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using HealthTrackerLocal.Models;

namespace HealthTrackerLocal.Services;

public class GoogleSheetsService
{
    private readonly SheetsService _sheetsService;
    private readonly string _spreadsheetId;
    private readonly string _sheetName;

    public GoogleSheetsService(IConfiguration configuration)
    {
        _spreadsheetId = configuration["GoogleSheets:SpreadsheetId"]
            ?? throw new InvalidOperationException("GoogleSheets:SpreadsheetId is missing.");

        _sheetName = configuration["GoogleSheets:SheetName"]
            ?? throw new InvalidOperationException("GoogleSheets:SheetName is missing.");

        var credentialsPath = configuration["GoogleSheets:CredentialsPath"]
            ?? throw new InvalidOperationException("GoogleSheets:CredentialsPath is missing.");

        GoogleCredential credential;

        using (var stream = new FileStream(credentialsPath, FileMode.Open, FileAccess.Read))
        {
            credential = GoogleCredential
                .FromStream(stream)
                .CreateScoped(SheetsService.Scope.Spreadsheets);
        }

        _sheetsService = new SheetsService(new BaseClientService.Initializer
        {
            HttpClientInitializer = credential,
            ApplicationName = "Local Health Tracker"
        });
    }

    public async Task AppendHealthEntryAsync(HealthEntry entry)
    {
        var range = $"{_sheetName}!A:AK";

        var row = new List<object?>
        {
            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            entry.Date.ToString("yyyy-MM-dd"),
            entry.CycleDay,
            entry.CyclePhase,
            entry.Bleeding,
            entry.Cramps,
            entry.Pms,
            entry.OvulationSigns,
            entry.SleepStart?.ToString(@"hh\:mm"),
            entry.WakeTime?.ToString(@"hh\:mm"),
            entry.SleepHours,
            entry.NightAwakenings,
            entry.SleepQuality,
            entry.DeepSleepPercent,
            entry.Mood,
            entry.Energy,
            entry.Anxiety,
            entry.Irritability,
            entry.Focus,
            entry.Motivation,
            BoolToText(entry.Headache),
            BoolToText(entry.Migraine),
            BoolToText(entry.Ibuprofen),
            entry.BodyPain,
            entry.Bloating,
            entry.Nausea,
            entry.Appetite,
            entry.Cravings,
            entry.CaffeineCups,
            entry.LastCaffeineTime?.ToString(@"hh\:mm"),
            BoolToText(entry.Alcohol),
            BoolToText(entry.PhysicalActivity),
            entry.Stress,
            entry.UnusualEvents,
            BoolToText(entry.DailyMedsTaken),
            entry.Supplements,
            entry.Notes
        };

        var valueRange = new ValueRange
        {
            Values = new List<IList<object>>
            {
                row.Cast<object>().ToList()
            }
        };

        var appendRequest = _sheetsService.Spreadsheets.Values.Append(
            valueRange,
            _spreadsheetId,
            range
        );

        appendRequest.ValueInputOption =
            SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;

        await appendRequest.ExecuteAsync();
    }
    private static string BoolToText(bool? value)
    {
        return value switch
        {
            true => "Yes",
            false => "No",
            null => ""
        };
    }
}