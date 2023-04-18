using Ardalis.Specification;

namespace ThumbezaTech.Leads.SharedKernel.Interfaces;

// from Ardalis.Specification
public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot { }
