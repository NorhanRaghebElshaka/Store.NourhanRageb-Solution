using Store.NourhanRageb.Domain.Entities;
using Store.NourhanRageb.Domain.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.NourhanRageb.Domain
{
    public interface IUnitOfWork
    {
        Task<int> CompleteAsync(); //Save Channge هتعمل 

        // Create Repository<T> And Return
        IGenericRepositroy<TEntity , TKey> Repositroy<TEntity , TKey>() where TEntity : BaseEntity<TKey>;
    }
}
