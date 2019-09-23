using BusinessServices.Person.Extensions;
using System;
using System.Linq;

namespace BusinessServices.Product.Extensions
{
    public static class ProductExtensions
    {
        public static DataAccess.Product.Product ToEnitity(this Models.DTOs.ProductModel model)
        {
            if (model == null)
                return null;

            return new DataAccess.Product.Product
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Status = model.Status,
                Avatar = model.Avatar,
                Deactivated = model.Deactivated,
                ChampionCount = model.ChampionCount,
                Champions = model.Champions?.Select(p => p.ToEnitity())
            };
        }

        public static Models.DTOs.ProductModel ToModel(this DataAccess.Product.Product entity)
        {
            if (entity == null)
                return null;

            return new Models.DTOs.ProductModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Status = entity.Status,
                Avatar = entity.Avatar,
                Deactivated = entity.Deactivated,
                ChampionCount = entity.ChampionCount,
                Champions = entity.Champions?.Select(p => p.ToModel())
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