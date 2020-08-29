using GraphQL;
using GraphQL.Types;
using GraphQLCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace net_core_graphql.Controllers
{
    //[ApiController, Route("[controller]")]
    //[Produces("application/json")]
    public class GraphQLController : ControllerBase
    {
        private readonly IDocumentExecuter _documentExecuter;
        private readonly ISchema _schema;

        public GraphQLController(ISchema schema, IDocumentExecuter document)
        {
            _schema = schema;
            _documentExecuter = document;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GraphQLRequest query)
        {
            if (query == null) { throw new ArgumentNullException(nameof(query)); }

            var inputs = query.Variables.ToInputs();
            var executionOptions = new ExecutionOptions
            {
                Schema = _schema,
                Query = query.Query,
                Inputs = inputs
            };

            var result = await _documentExecuter.ExecuteAsync(executionOptions).ConfigureAwait(false);

            if (result.Errors?.Count != 0)
                return BadRequest(result);

            return Ok(result);
        }
    }
}