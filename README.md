# Iso8601
Iso8601-helper. Adds extensions for DateTime to work with time periods represented by strings like "P2Y4DT20M" easier.

See How2Use.cs

Briefly:
var nextBirthday = DateTime.Now.Add(new Iso8601Duration("P1Y"));

Use:
• Constants.cs
• Extensions.cs
• Iso8601Duration.cs
