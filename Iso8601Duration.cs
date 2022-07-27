using System;
using System.Text.RegularExpressions;

namespace Iso8601
{
    /// <summary>
    /// Represents a time interval written in ISO 8601 string like "P1Y2M3DT4H5M6S".
    /// </summary>
    public class Iso8601Duration
    {
        internal struct DurationStruct
        {
            public int Years;
            public int Months;
            public int Days;
            public int Hours;
            public int Minutes;
            public int Seconds;
        }

        internal DurationStruct Duration;

        /// <summary>
        /// Initializes a new instance of the Iso8601Duration class using a time interval written in ISO 8601 string like "P1Y2M3DT4H5M6S".
        /// </summary>
        /// <param name="iso8601Duration">A time interval written in ISO 8601 string like "P1Y2M3DT4H5M6S".</param>
        public Iso8601Duration(string iso8601Duration)
        {
            this._CheckInput(iso8601Duration);
            var groups = Regex.Matches(iso8601Duration, Constants.GROUP_PATTERN);
            DurationStruct durStr = new DurationStruct();
            foreach (var group in groups)
            {
                var elements = Regex.Matches(group.ToString(), Constants.ELEMENT_PATTERN);
                if (elements[0].Value == Constants.TAG_PERIOD)
                {
                    for (int i = 1; i < elements.Count; i++)
                    {
                        var intVal = 0;
                        int.TryParse(elements[i].Value.Substring(0, elements[i].Value.Length - 1), out intVal);
                        switch (elements[i].Value.Substring(elements[i].Value.Length - 1))
                        {
                            case Constants.TAG_YEARS: durStr.Years += intVal; break;
                            case Constants.TAG_MONTHS: durStr.Months += intVal; break;
                            case Constants.TAG_DAYS: durStr.Days += intVal; break;
                            default: throw new Exception(Constants.ERROR_EXTRASYMB);
                        }
                    }
                }
                if (elements[0].Value == Constants.TAG_TIME)
                {
                    for (int i = 1; i < elements.Count; i++)
                    {
                        var intVal = 0;
                        int.TryParse(elements[i].Value.Substring(0, elements[i].Value.Length - 1), out intVal);
                        switch (elements[i].Value.Substring(elements[i].Value.Length - 1))
                        {
                            case Constants.TAG_HRS: durStr.Hours += intVal; break;
                            case Constants.TAG_MINS: durStr.Minutes += intVal; break;
                            case Constants.TAG_SECS: durStr.Seconds += intVal; break;
                            default: throw new Exception(Constants.ERROR_EXTRASYMB);
                        }
                    }
                }
            }
            this.Duration = durStr;
        }

        /// <summary>
        /// Initializes a new instance of the Iso8601Duration class using a time interval represented by TimeSpan.
        /// </summary>
        /// <param name="timeSpan">A time interval.</param>
        public Iso8601Duration(TimeSpan timeSpan)
        {
            this.Duration = new DurationStruct
            {
                Days = timeSpan.Days,
                Hours = timeSpan.Hours,
                Minutes = timeSpan.Minutes,
                Seconds = timeSpan.Seconds
            };
        }

        #region OperatorsExtensions
        /// <summary>
        /// Compares 2 time intervals represented by instances of Iso8601Duration.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>Whether intervals are equal or not.</returns>
        public static bool operator ==(Iso8601Duration left, Iso8601Duration right) => _ConvertToSeconds(left) == _ConvertToSeconds(right);

        /// <summary>
        /// Compares 2 time intervals represented by instances of Iso8601Duration.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>Whether intervals are NOT equal.</returns>
        public static bool operator !=(Iso8601Duration left, Iso8601Duration right) => !(left == right);

        /// <summary>
        /// Compares this time interval to another represented by an instance of Iso8601Duration.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>Whether intervals are equal.</returns>
        public bool Equals(Iso8601Duration isoDuration) => this == isoDuration;

        /// <summary>
        /// Compares 2 time intervals represented by instances of Iso8601Duration.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>Whether the left interval is longer than the right one.</returns>
        public static bool operator >(Iso8601Duration left, Iso8601Duration right) => _ConvertToSeconds(left) > _ConvertToSeconds(right);

        /// <summary>
        /// Compares 2 time intervals represented by instances of Iso8601Duration.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>Whether the left interval is shorter than the right one.</returns>
        public static bool operator <(Iso8601Duration left, Iso8601Duration right) => _ConvertToSeconds(left) < _ConvertToSeconds(right);

        /// <summary>
        /// Compares 2 time intervals represented by instances of Iso8601Duration.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>Whether the left interval is longer or equal to the right one.</returns>
        public static bool operator >=(Iso8601Duration left, Iso8601Duration right) => !(left < right);

        /// <summary>
        /// Compares 2 time intervals represented by instances of Iso8601Duration.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>Whether the left interval is shorter or equal to the right one.</returns>
        public static bool operator <=(Iso8601Duration left, Iso8601Duration right) => !(left > right);
        #endregion

        private void _CheckInput(string iso8601Duration)
        {
            if (!iso8601Duration.StartsWith(Constants.TAG_PERIOD)) throw new Exception(Constants.ERROR_FIRSTSYMB);
            if (Regex.Replace(iso8601Duration, Constants.ELEMENT_PATTERN, "").Length > 0) throw new Exception(Constants.ERROR_EXTRASYMB);
        }

        private static int _ConvertToSeconds(Iso8601Duration isoDuration)
        {
            return isoDuration.Duration.Years * 365 * 24 * 60 * 60 +
                isoDuration.Duration.Months * 30 * 24 * 60 * 60 +
                isoDuration.Duration.Days * 24 * 60 * 60 +
                isoDuration.Duration.Hours * 60 * 60 +
                isoDuration.Duration.Minutes * 60 +
                isoDuration.Duration.Seconds;
        }
    }
}
