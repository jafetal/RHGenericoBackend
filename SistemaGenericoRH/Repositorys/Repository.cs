using Microsoft.EntityFrameworkCore;
using SistemaGenericoRH.Models;

namespace SistemaGenericoRH.Repositorys
{
    public abstract class Repository<T> : IRepository<T>, IDisposable where T : class
    {
        public ModelContext context;

        public Repository(ModelContext context)
        {
            this.context = context;
            this.context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public void Commit()
        {
            context.SaveChanges(); 
        }

        public T Create(T obj)
        {
            typeof(T).GetProperties().ToList().ForEach(propertyInfo =>
            {
                if ((propertyInfo.PropertyType.IsClass && !propertyInfo.PropertyType.FullName.StartsWith("System.")) ||
                     propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>))
                {
                    propertyInfo.SetValue(obj, null);
                }
            });

            context.Set<T>().Add(obj);
            context.SaveChanges();

            context.Entry(obj).State = EntityState.Detached;
            context.SaveChanges();
            return obj;
        }

        public void Update(T obj)
        {
            typeof(T).GetProperties().ToList().ForEach(propertyInfo =>
            {
                if ((propertyInfo.PropertyType.IsClass && !propertyInfo.PropertyType.FullName.StartsWith("System.")) ||
                     propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>))
                {
                    propertyInfo.SetValue(obj, null);
                }
            });

            context.Set<T>().Attach(obj);
            context.Entry(obj).State = EntityState.Modified;
            context.SaveChanges();

            context.Entry(obj).State = EntityState.Detached;
            context.SaveChanges();
        }

        public void Delete(T obj)
        {
            typeof(T).GetProperties().ToList().ForEach(propertyInfo =>
            {
                if ((propertyInfo.PropertyType.IsClass && !propertyInfo.PropertyType.FullName.StartsWith("System.")) ||
                     propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>))
                {
                    propertyInfo.SetValue(obj, null);
                }
            });

            context.Remove(obj);
            context.SaveChanges();
        }

        public void Delete(IEnumerable<T> objs)
        {
            foreach (T obj in objs)
            {
                Delete(obj);
            }
        }

        public void Update(IEnumerable<T> objs)
        {
            foreach (T obj in objs)
            {
                typeof(T).GetProperties().ToList().ForEach(propertyInfo =>
                {
                    if ((propertyInfo.PropertyType.IsClass && !propertyInfo.PropertyType.FullName.StartsWith("System.")) ||
                         propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>))
                    {
                        propertyInfo.SetValue(obj, null);
                    }
                });

                context.Set<T>().Attach(obj);
                context.Entry(obj).State = EntityState.Modified;
            }
            context.SaveChanges();
        }

        public void Create(IEnumerable<T> objs)
        {
            foreach (T obj in objs)
            {
                typeof(T).GetProperties().ToList().ForEach(propertyInfo =>
                {
                    if ((propertyInfo.PropertyType.IsClass && !propertyInfo.PropertyType.FullName.StartsWith("System.")) ||
                         propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>))
                    {
                        propertyInfo.SetValue(obj, null);
                    }
                });

                context.Set<T>().Add(obj);
                                  
            }

            context.SaveChanges();
        }

        public void Dispose()
        {
            if (context != null)
            {
                context.Dispose();
            }
            GC.SuppressFinalize(this);
        }

    }
}
