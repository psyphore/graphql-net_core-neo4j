namespace ThumbezaTech.Leads.Application.Common.Extensions;

public static class TimeStampExtensions
{
    public static long? DateToTimeStamp(this DateTime? date) => date?.Ticks;

    public static DateTime? TimeStampToDate(this long? timestamp) =>
      timestamp == null
      ? null :
      new DateTime(1970, 1, 1).AddMilliseconds(timestamp.Value);

    public static long DateToTimeStamp(this DateTime date) => date.Ticks;

    public static DateTime TimeStampToDate(this long timestamp) => new DateTime(1970, 1, 1).AddMilliseconds(timestamp);
}
