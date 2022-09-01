namespace Booking_app.Services.Interfaces
{
    public interface IServiceBase<T> where T : class
    {
        ICollection<T> FindAll();
        T FindById(int id);
        bool Create(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        bool isExis(int id);
    }
}