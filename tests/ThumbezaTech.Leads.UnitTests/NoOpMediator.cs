using Mediator;

namespace ThumbezaTech.Leads.UnitTests;

public class NoOpMediator : IMediator
{
  public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamRequest<TResponse> request, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public IAsyncEnumerable<object?> CreateStream(object request, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamQuery<TResponse> query, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamCommand<TResponse> command, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task Publish(object notification, CancellationToken cancellationToken = default)
  {
    return Task.CompletedTask;
  }

  public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
  {
    return Task.CompletedTask;
  }

  public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
  {
    return Task.FromResult<TResponse>(default);
  }

  public Task<object?> Send(object request, CancellationToken cancellationToken = default)
  {
    return Task.FromResult<object?>(default);
  }

  public ValueTask<TResponse> Send<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default)
  {
    return ValueTask.FromResult<TResponse>(default!);
  }

  public ValueTask<TResponse> Send<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = default)
  {
    return ValueTask.FromResult<TResponse>(default!);
  }

  ValueTask IPublisher.Publish<TNotification>(TNotification notification, CancellationToken cancellationToken)
  {
    return ValueTask.CompletedTask;
  }

  ValueTask IPublisher.Publish(object notification, CancellationToken cancellationToken)
  {
    return ValueTask.CompletedTask;
  }

  ValueTask<TResponse> ISender.Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken)
  {
    return ValueTask.FromResult<TResponse>(default!);
  }

  ValueTask<object?> ISender.Send(object message, CancellationToken cancellationToken)
  {
    return ValueTask.FromResult<object?>(default!);
  }
}
