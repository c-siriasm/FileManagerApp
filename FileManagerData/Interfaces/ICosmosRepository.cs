using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagerData.Interfaces
{
    public interface ICosmosRepository<T>
    {
        Task<bool> CreateItem(T model);
        Task<List<T>> GetItems();
    }
}
