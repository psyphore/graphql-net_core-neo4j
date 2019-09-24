using BusinessServices.Person.Extensions;
using System;
using System.Linq;

namespace BusinessServices.Search.Extensions
{
    public static class SearchExtensions
    {
        public static Models.DTOs.SearchModel ToModel(this DataAccess.Search.Search entity)
        {
            if (entity == null)
                return null;

            return new Models.DTOs.SearchModel
            {
                Id = entity.Id,
                People = entity.People?.Select(p => p.ToModel()),
                Count = entity.People != null ? entity.People.Count() : 0
            };
        }

        public static DateTime? TimeStampToDate(this long? timestamp)
        {
            if (timestamp == null)
                return null;

            return new DateTime(1970, 1, 1).AddTicks(timestamp.Value);
        }

        public static long? DateToTimeStamp(this DateTime? date)
        {
            if (date == null)
                return null;

            return date.Value.Ticks;
        }
    }
}