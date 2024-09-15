using System.Diagnostics;

using Microsoft.Extensions.Logging;

namespace ThumbezaTech.TaxiManager.Core.Common.Behaviors;
internal sealed class PerformanceBehavior<TRequest, TResponse>
  : IPipelineBehavior<TRequest, TResponse>
  where TRequest : IMessage
{
  private readonly ILogger<TRequest> _logger;

  public PerformanceBehavior(ILogger<TRequest> logger) => _logger = logger;

  public ValueTask<TResponse> Handle(TRequest message, CancellationToken cancellationToken, MessageHandlerDelegate<TRequest, TResponse> next)
  {
    var start = Stopwatch.GetTimestamp();
    try
    {
      return next(message, cancellationToken);
    }
    finally
    {
      var delta = TimeSpan.FromTicks(Stopwatch.GetTimestamp() - start);
      if (delta.TotalMilliseconds > 500)
        _logger.LogWarning("{@Name} took {@ElapsedMs}ms to complete.", typeof(TRequest).Name, delta.TotalMilliseconds);
    }
  }
}
