using BusinessServices.Building.Extensions;
using BusinessServices.Product.Extensions;
using System;
using System.Linq;

namespace BusinessServices.Person.Extensions
{
    public static class PersonExtensions
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

        public static DataAccess.Person.Person ToEnitity(this Models.DTOs.PersonModel model)
        {
            if (model == null)
                return null;

            return new DataAccess.Person.Person
            {
                Id = model.Id,
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                Line = model.Line?.Select(l => l.ToEnitity()).ToList(),
                Manager = model.Manager.ToEnitity(),
                Team = model.Team?.Select(t => t.ToEnitity()).ToList(),
                Avatar = model.Avatar,
                Bio = model.Bio,
                Email = model.Email,
                KnownAs = model.KnownAs,
                Mobile = model.Mobile,
                Title = model.Title,
                Deactivated2 = model.Deactivated.DateToTimeStamp(),
                Deactivated = model.Deactivated,

                Buildings = model.Buildings?.Select(b => b.ToEnitity()),
                Products = model.Products?.Select(p => p.ToEnitity())
            };
        }

        public static Models.DTOs.PersonModel ToModel(this DataAccess.Person.Person entity)
        {
            if (entity == null)
                return null;

            return new Models.DTOs.PersonModel
            {
                Id = entity.Id,
                Firstname = entity.Firstname,
                Lastname = entity.Lastname,
                Line = entity.Line?.Select(l => l.ToModel()),
                Manager = entity.Manager.ToModel(),
                Team = entity.Team?.Select(t => t.ToModel()),
                Avatar = entity.Avatar,
                Bio = entity.Bio,
                Email = entity.Email,
                KnownAs = entity.KnownAs,
                Mobile = entity.Mobile,
                Title = entity.Title,
                Deactivated = entity.Deactivated2.TimeStampToDate(),
                Buildings = entity.Buildings?.Select(b => b.ToModel()),
                Products = entity.Products?.Select(p => p.ToModel()),
            };
        }
    }
}