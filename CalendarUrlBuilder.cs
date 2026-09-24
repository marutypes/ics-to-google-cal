using Ical.Net;
using Ical.Net.CalendarComponents;

public static class CalendarUrlBuilder
{
    public static string BuildGoogleCalendarUrl(CalendarEvent calendarEvent)
    {
        var query = new Dictionary<string, string?>
        {
            ["action"] = "TEMPLATE",
            ["text"] = calendarEvent.Summary,
            ["dates"] = BuildDateRange(calendarEvent),
            ["location"] = calendarEvent.Location,
            ["details"] = calendarEvent.Description,
        };

        var queryString = string.Join(
            "&",
            query
                .Where(x => !string.IsNullOrWhiteSpace(x.Value))
                .Select(x => $"{Uri.EscapeDataString(x.Key)}={Uri.EscapeDataString(x.Value!)}")
        );

        return $"https://calendar.google.com/calendar/render?{queryString}";
    }

    public static string BuildDateRange(CalendarEvent calendarEvent)
    {
        var start = calendarEvent.DtStart;
        var end = calendarEvent.DtEnd;

        if (start is null)
            throw new InvalidOperationException("Event has no start time.");

        if (!start.HasTime)
        {
            var startText = $"{start.Year:D4}{start.Month:D2}{start.Day:D2}";
            var effectiveEnd = end ?? start.AddDays(1);
            var endText = $"{effectiveEnd.Year:D4}{effectiveEnd.Month:D2}{effectiveEnd.Day:D2}";

            return $"{startText}/{endText}";
        }

        var startUtc = start.AsUtc;
        var endUtc = end?.AsUtc ?? start.AddHours(1).AsUtc;

        return $"{startUtc:yyyyMMdd'T'HHmmss'Z'}/{endUtc:yyyyMMdd'T'HHmmss'Z'}";
    }
}
