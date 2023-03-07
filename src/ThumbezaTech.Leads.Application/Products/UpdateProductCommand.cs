using ThumbezaTech.Leads.Domain.ProductAggregate;

namespace ThumbezaTech.Leads.Application.Products;

public sealed record UpdateProductCommand(Product Product) : ICommand<Result>;

internal sealed class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, Result>
{
  private readonly IProductService _service;

  public UpdateProductCommandHandler(IProductService service) => _service = service;

  public ValueTask<Result> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
  {
    var parameter = new Dictionary<string, object>
        {
            { "product", Guard.Against.Null(command.Product, nameof(command.Product)) },
        };
    return _service.UpdateProduct(parameter, cancellationToken);
  }
}
