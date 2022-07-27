using System;

namespace Iso8601
{
    public static class Extensions
    {
        /// <summary>
        /// Returns a new DateTime that adds the value of the specified Iso8601Duration to the value of this instance.
        /// </summary>
        /// <param name="dateTime">Current instance of DateTime.</param>
        /// <param name="isoDuration">Instance of Iso8601Duration with specified value.</param>
        /// <returns>An object whose value is the sum of the date and time represented by this instance and the time interval represented by isoDuration.</returns>
        public static DateTime Add(this DateTime dateTime, Iso8601Duration isoDuration)
        {
            dateTime = dateTime.Add(new TimeSpan(
                isoDuration.Duration.Days,
                isoDuration.Duration.Hours,
                isoDuration.Duration.Minutes,
                isoDuration.Duration.Seconds
                ));
            dateTime = dateTime.AddMonths(isoDuration.Duration.Months);
            dateTime = dateTime.AddYears(isoDuration.Duration.Years);
            return dateTime;
        }

        /// <summary>
        /// Returns a new DateTime that subtracts the the specified Iso8601Duration from the value of this instance.
        /// </summary>
        /// <param name="dateTime">Current instance of DateTime.</param>
        /// <param name="isoDuration">Instance of Iso8601Duration with specified value.</param>
        /// <returns>An object that is equal the date and time represented by this instance minus the time interval represented by isoDuration.</returns>
        public static DateTime Subtract(this DateTime dateTime, Iso8601Duration isoDuration)
        {
            dateTime = dateTime.Subtract(new TimeSpan(
                isoDuration.Duration.Days,
                isoDuration.Duration.Hours,
                isoDuration.Duration.Minutes,
                isoDuration.Duration.Seconds
                ));
            dateTime = dateTime.AddMonths(-isoDuration.Duration.Months);
            dateTime = dateTime.AddYears(-isoDuration.Duration.Years);
            return dateTime;
        }
    }
}
