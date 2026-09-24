using System.Diagnostics;
using Ical.Net;

if (args.Length == 0)
{
    Console.Error.WriteLine("Usage: ics-to-google-calendar <file.ics>");
    return 1;
}

var filePath = args[0];

if (!File.Exists(filePath))
{
    Console.Error.WriteLine($"File not found: {filePath}");
    return 1;
}

try
{
    var ics = await File.ReadAllTextAsync(filePath);

    var calendar = Calendar.Load(ics);

    if (calendar is null || calendar.Events.Count == 0)
    {
        Console.Error.WriteLine("No calendar events found.");
        return 1;
    }

    foreach (var calendarEvent in calendar.Events)
    {
        var url = CalendarUrlBuilder.BuildGoogleCalendarUrl(calendarEvent);

        Process.Start(new ProcessStartInfo { FileName = url, UseShellExecute = true });
    }

    return 0;
}
catch (Exception ex)
{
    Console.Error.WriteLine($"Couldn't open calendar event: {ex.Message}");
    return 1;
}
