namespace Iso8601
{
    public static class Constants
    {
        public const string TAG_PERIOD = "P";
        public const string TAG_YEARS = "Y";
        public const string TAG_MONTHS = "M";
        public const string TAG_DAYS = "D";
        public const string TAG_TIME = "T";
        public const string TAG_HRS = "H";
        public const string TAG_MINS = "M";
        public const string TAG_SECS = "S";

        public const string GROUP_PATTERN = "P[0-9YMD-]+|T[0-9HMS-]+";
        public const string ELEMENT_PATTERN = "((-{0,1}\\d{0,})[PYMDTHMS])";

        public const string ERROR_EXTRASYMB = "String contains extraneous symbols.";
        public const string ERROR_FIRSTSYMB = "String should start with 'P'.";
    }
}
