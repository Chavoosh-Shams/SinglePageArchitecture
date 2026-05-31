using MvcSinglePage.Frameworks.ResponseFrameworks.Contracts;

namespace MvcSinglePage.Models.Services.Contracts
{
    public interface IRepository<T> where T : class
    {
        Task<IResponse<T>> InsertAsync(T obj);

        Task<IResponse<T>> UpdateAsync(T obj);

        Task<IResponse<T>> DeleteAsync(T obj);

        Task<IResponse<T>> SelectById(T obj);

        Task<IResponse<IEnumerable<T>>> SelectAllAsync();

    }
}
