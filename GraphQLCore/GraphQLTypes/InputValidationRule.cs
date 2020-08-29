using GraphQL.Validation;

namespace GraphQLCore.GraphQLTypes
{
    public class InputValidationRule : IValidationRule
    {
        public INodeVisitor Validate(ValidationContext context)
        {
            return new DebugNodeVisitor();
        }
    }
}