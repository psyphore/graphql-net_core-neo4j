using System;

namespace BusinessServices.Helpers
{
    public static class TimeStampExtensions
    {
        public static long? DateToTimeStamp(this DateTime? date)
        {
            if (date == null)
                return null;

            return date.Value.Ticks;
        }

        public static DateTime? TimeStampToDate(this long? timestamp)
        {
            if (timestamp == null)
                return null;

            return new DateTime(1970, 1, 1).AddTicks(timestamp.Value);
        }

        public static long DateToTimeStamp(this DateTime date)
        {
            if (date == null)
                return 0;

            return date.Ticks;
        }

        public static DateTime TimeStampToDate(this long timestamp)
        {
            return new DateTime(1970, 1, 1).AddTicks(timestamp);
        }
    }
}
