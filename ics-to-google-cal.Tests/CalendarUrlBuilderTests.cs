using Ical.Net;
using Xunit;

namespace ics_to_google_cal.Tests;

public class CalendarUrlBuilderTests
{
    [Fact]
    public void BuildGoogleCalendarUrl_ForTimedEvent_UsesGoogleTemplateFormat()
    {
        var calendar = Calendar.Load(
            "BEGIN:VCALENDAR\nVERSION:2.0\nBEGIN:VEVENT\nUID:test-1\nDTSTAMP:20240101T000000Z\nDTSTART:20240102T090000Z\nDTEND:20240102T100000Z\nSUMMARY:Team sync\nLOCATION:Room 1\nDESCRIPTION:Discuss roadmap\nEND:VEVENT\nEND:VCALENDAR"
        );
        Assert.NotNull(calendar);

        var evt = Assert.Single(calendar!.Events)!;

        var url = CalendarUrlBuilder.BuildGoogleCalendarUrl(evt);

        Assert.StartsWith("https://calendar.google.com/calendar/render?", url);
        Assert.Contains("action=TEMPLATE", url);
        Assert.Contains("text=Team%20sync", url);
        Assert.Contains("dates=20240102T090000Z%2F20240102T100000Z", url);
        Assert.Contains("location=Room%201", url);
        Assert.Contains("details=Discuss%20roadmap", url);
    }

    [Fact]
    public void BuildDateRange_ForAllDayEvent_UsesGoogleAllDayFormat()
    {
        var calendar = Calendar.Load(
            "BEGIN:VCALENDAR\nVERSION:2.0\nBEGIN:VEVENT\nUID:test-2\nDTSTAMP:20240101T000000Z\nDTSTART;VALUE=DATE:20240102\nDTEND;VALUE=DATE:20240104\nSUMMARY:Conference\nEND:VEVENT\nEND:VCALENDAR"
        );
        Assert.NotNull(calendar);

        var evt = Assert.Single(calendar!.Events)!;

        var range = CalendarUrlBuilder.BuildDateRange(evt);

        Assert.Equal("20240102/20240104", range);
    }
}
