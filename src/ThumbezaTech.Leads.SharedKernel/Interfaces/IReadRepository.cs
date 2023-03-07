using Ardalis.Specification;

namespace ThumbezaTech.Leads.SharedKernel.Interfaces;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
{
}
