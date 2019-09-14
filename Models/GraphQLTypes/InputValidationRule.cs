using GraphQL.Validation;

namespace Models.GraphQLTypes
{
    public class InputValidationRule : IValidationRule
    {
        public INodeVisitor Validate(ValidationContext context)
        {
            return new DebugNodeVisitor();
        }
    }
}