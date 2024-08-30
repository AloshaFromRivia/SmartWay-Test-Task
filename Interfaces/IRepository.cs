using SmartWay_Test_Task.Entities;

namespace SmartWay_Test_Task.Interfaces
{
    public interface IRepository<T> where T : IEntity
    {
        Task<T> Get(int id);
        Task<IQueryable<T>> GetAll();
        Task<IQueryable<T>> GetAll(Func<Employee, bool> predicate);
        Task<int> Add(T dto);
        Task Delete(int id);
        Task Update(int Id,T entity);
    }
}
