using Dy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dy.Core.DataAccess.NHibernate
{
    public class NhQueryableRepository<T> : IQueryableRepository<T> where T : class, IEntity, new()
    {
        private NHibernateHelper _nHibernateHelper;

        public NhQueryableRepository(NHibernateHelper nHibernateHelper)
        {
            _nHibernateHelper = nHibernateHelper;
        }

        IQueryable<T> _entities;
        public virtual IQueryable<T> Entities =>  _entities ?? (_entities = _nHibernateHelper.OpenSession().Query<T>());
        public IQueryable<T> Table => this.Entities;

    }
}
