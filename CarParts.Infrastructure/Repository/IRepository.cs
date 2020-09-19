using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarParts.Infrastructure.Repository
{
    public interface IRepository<T>
    {
        Task Create(T t);
        Task Delete(int id);
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task Update(T t , int id);
    }
}
