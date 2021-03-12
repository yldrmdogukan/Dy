using Dy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dy.Core.DataAccess
{
    public interface IQueryableRepository<T> where T : class, IEntity, new() 
    {
        IQueryable<T> Table { get; }
    }
}


/*Context kapanmasın diye Queryable, (Bll'de kulllanmak için), sadece Select işlemleri için
*class -> referans tip
*/