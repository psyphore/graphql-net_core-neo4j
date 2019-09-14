namespace BusinessServices.Person.Extensions
{
    public static class PersonExtensions
    {
        public static DataAccess.Person.Person ToEnitity(this Models.DTOs.PersonModel model)
        {
            if (model == null)
                return null;

            return new DataAccess.Person.Person
            {
                Id = model.Id,
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                Buildings = model.Buildings,
                Line = model.Line,
                Manager = model.Manager.ToEnitity(),
                Products = model.Products,
                Team = model.Team,
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
                Buildings = entity.Buildings,
                Line = entity.Line,
                Manager = entity.Manager.ToModel(),
                Products = entity.Products,
                Team = entity.Team,
            };
        }
    }
}