using System;
using build9ja.core.Entities;

namespace build9ja.core.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
		Task<int> Complete();
	}
}

