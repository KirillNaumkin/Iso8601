# Iso8601
Iso8601-helper. Adds extensions for DateTime to work with time periods represented by strings like "P2Y4DT20M" easier.

See How2Use.cs

Briefly:
`var nextBirthday = DateTime.Now.Add(new Iso8601Duration("P1Y"));`

Use:

- Constants.cs
- Extensions.cs
- Iso8601Duration.cs

# TLDR
The reason for creating this helper is that the only one native .NET-converter I know for such strings is XmlConvert. But it counts time intervals in an errative way because of a month consisting of exactly 30 days and a year consisting of exactly 365 days. It leads to a misconception like:

- ```DateTime.Parse("2020-01-01").Add(XmlConvert.ToTimeSpan("P1M")); // returns 2020-01-31``` and
- ```DateTime.Parse("2020-01-01").Add(XmlConvert.ToTimeSpan("P2M")); // returns 2020-03-02```.

I also didn't implement converting ISO 8601 into a TimeSpan because:

- "P1Y" is not `TimeSpan.Days = 365` and
- "P1M" is not `TimeSpan.Days = 30`

... — the truth of these expressions depends on a specific year and month, so you cannot just convert these ISO 8601 intervals into a TimeSpan for adding to a custom DateTime, because specific initial DateTime leads to a specific resulting DateTime for the same ISO 8601 period value.
