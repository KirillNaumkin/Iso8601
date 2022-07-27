# Iso8601
Iso8601-helper. Adds extensions for DateTime to work with time periods represented by strings like "P2Y4DT20M" easier.

See How2Use.cs

Briefly:
`var nextBirthday = DateTime.Now.Add(new Iso8601Duration("P1Y"));`

Use:

- Constants.cs
- Extensions.cs
- Iso8601Duration.cs

The reasons of creating this helper are:

- The only one native .NET-converter I know for such strings is XmlConvert. But it counts time intervals in an errative way because of a month consisting of exactly 30 days and a year consisting of exactly 365 days. It leads to a misconception like ```DateTime.Parse("2020-01-01").Add(XmlConvert.ToTimeSpan("P1M")); // returns 2020-01-30```
