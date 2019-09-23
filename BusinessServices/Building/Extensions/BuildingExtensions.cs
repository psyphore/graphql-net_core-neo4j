using BusinessServices.Person.Extensions;
using System;
using System.Linq;

namespace BusinessServices.Building.Extensions
{
    public static class BuildingExtensions
    {
        public static DataAccess.Building.Building ToEnitity(this Models.DTOs.BuildingModel model)
        {
            if (model == null)
                return null;

            return new DataAccess.Building.Building
            {
                Id = model.Id,
                Name = model.Name,
                Address = model.Address,
                Avatar = model.Avatar,
                Deactivated = model.Deactivated,
                HeadCount = model.HeadCount,
                People = model.People?.Select(p => p.ToEnitity())
            };
        }

        public static Models.DTOs.BuildingModel ToModel(this DataAccess.Building.Building entity)
        {
            if (entity == null)
                return null;

            return new Models.DTOs.BuildingModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Address = entity.Address,
                Avatar = entity.Avatar,
                Deactivated = entity.Deactivated,
                HeadCount = entity.HeadCount,
                People = entity.People?.Select(p => p.ToModel())
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