using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyRepository.EntityFrameworkCore
{
    public interface IRepository<TEntity> : Abstractions.IRepository<TEntity>
        where TEntity : class
    {
    }
}
