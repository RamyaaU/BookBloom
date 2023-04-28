using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookBloom.DataAccess.Repository.IRepository
{
    //when working with generic class type will not be known so T is given and T is a class
    public interface IRepository<T> where T : class
    {
        //T- category
        IEnumerable<T> GetAll();
                
        //result will be a function, and out result will be boolean  
        T Get(Expression<Func<T, bool>> filter);

        void Add(T entity);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entity);
    }
}
