# ICS to Google Calendar


https://github.com/user-attachments/assets/1aa1c55b-79a3-439b-bd8a-2824ec5ac76d


This small Windows utility lets you double-click an `.ics` file on your desktop or in Explorer and have it open the event in Google Calendar so you can create it with a single click.

The idea is simple: when Windows sees an `.ics` file, it can open it using a custom command rather than a desktop calendar app. This tool reads the event data from the `.ics` file and launches the Google Calendar event-creation URL for each event in the file.

## Why this exists

A lot of calendar invites and export files arrive as `.ics` files. Opening them in a browser or calendar app is often clunky, especially if you don't want to use Outlook. This project helps by turning that flow into:

- find the `.ics` file
- parse the event(s)
- build the matching Google Calendar event URL(s)
- open them in the default browser

That means the experience becomes: "double-click the file, and Google Calendar opens with the event ready to save."

## How to use it

### 1. Build the app

From the repository root:

```bash
dotnet build
```

This produces a runnable executable in `bin/Debug/net9.0/`.

### 2. Associate `.ics` files with the app on Windows

You want Windows to open `.ics` files with this program instead of a default calendar app.

One way to do it is:

1. Open File Explorer.
2. Right-click any `.ics` file.
3. Choose Open with > Choose another app.
4. Select More apps.
5. If the app is not listed, choose Look for another app on this PC.
6. Browse to the compiled executable for this project.
7. Check "Always use this app to open .ics files".

After that, double-clicking an `.ics` file should launch the application and open the event in Google Calendar.

### 3. Run it manually

You can also invoke it from the command line:

```bash
ics-to-google-cal.exe "C:\path\to\event.ics"
```

If the file is valid, it will open Google Calendar event creation pages for the event(s) inside.

## Local build

From the project directory:

```bash
dotnet build
```

To run it directly after building:

```bash
dotnet run -- "C:\path\to\event.ics"
```

## Running tests locally

From the repository root:

```bash
dotnet test
```

This runs the xUnit test project and checks the date and URL-building logic used by the app.

## Notes

- The app reads the calendar data using `Ical.Net`.
- Timed events are converted to UTC for the Google Calendar `dates` parameter.
- All-day events are formatted as Google expects for all-day entries.
- Each event in the `.ics` file is opened as its own Google Calendar creation page.

## Example

If you have a file like `meeting.ics` and double-click it, the program will open something like:

```text
https://calendar.google.com/calendar/render?action=TEMPLATE&...
```

which pre-populates a Google Calendar event form for that event.
