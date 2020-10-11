using BusinessServices.Helpers;

namespace BusinessServices.User.Extensions
{
    public static class UserExtensions
    {
        public static DataAccess.User.User ToEnitity(this Models.DTOs.UserModel model)
        {
            if (model == null)
                return null;

            return new DataAccess.User.User
            {
                Id = model.Id,
                Firstname = model.Firstname,
                Email = model.Email,
                Deactivated = model.Deactivated.DateToTimeStamp(),
                Created = model.Created.DateToTimeStamp()
            };
        }

        public static Models.DTOs.UserModel ToModel(this DataAccess.User.User entity)
        {
            if (entity == null)
                return null;

            return new Models.DTOs.UserModel
            {
                Id = entity.Id,
                Firstname = entity.Firstname,
                Email = entity.Email,
                Created = entity.Created.TimeStampToDate(),
                Deactivated = entity.Deactivated.TimeStampToDate()
            };
        }
    }
}