using System.Linq;

namespace TicketingSystem.Data
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> All();

        T GetById(object id);

        void Add(T entity);

        //void Update(T entity);
        void Update(T entity, params string[] excludProp);

        void Delete(T entity);

        void Delete(object id);

        void Detach(T entity);
    }
}
