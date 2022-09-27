
namespace SistemaGenericoRH.Repositorys
{
    public interface IRepository<T>
    {
        T Create(T obj);
        void Create(IEnumerable<T> objs);
        void Update(T obj);
        void Update(IEnumerable<T> objs);
        void Delete(T obj);
        void Delete(IEnumerable<T> objs);
    }
}
