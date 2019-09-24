using GraphQL.Types;
using Models.DTOs;
using Models.GraphQLTypes.Person;

namespace Models.GraphQLTypes.Search
{ 
    public class SearchType : ObjectGraphType<SearchModel>
    {
        public SearchType()
        {
            Field(x => x.Id);
            Field(x => x.People);
            Field(x => x.Count);

            //Field<ListGraphType<PersonType>>("People", resolve: ctx => new ListGraphType<PersonType>());
        }
    }
}