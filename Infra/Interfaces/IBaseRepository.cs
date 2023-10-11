using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infra.Interfaces
{
    public interface IBaseRepository<T> where T : Base{
        Task<T> Create(T obj);
        Task<T> Update(T obj);
        Task Remove(long id);
        Task<T> Get(long id);
        Task<List<T>> Get();
    }
}