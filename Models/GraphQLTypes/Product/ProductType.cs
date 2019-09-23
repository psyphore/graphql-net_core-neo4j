using GraphQL.Types;
using Models.DTOs;
using Models.GraphQLTypes.Person;

namespace Models.GraphQLTypes.Product
{ 
    public class ProductType : ObjectGraphType<ProductModel>
    {
        public ProductType()
        {
            Field(x => x.Id);
            Field(x => x.Name, true);
            Field(x => x.Description, true);
            Field(x => x.ChampionCount, true);
            Field(x => x.Avatar, true);
            Field(x => x.Deactivated, true);

            Field<ListGraphType<PersonType>>("Champions", resolve: ctx => new ListGraphType<PersonType>());
            Field<PersonType>("Owner", resolve: ctx => new PersonType());
        }
    }
}