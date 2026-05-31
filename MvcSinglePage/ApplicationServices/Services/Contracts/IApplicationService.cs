using MvcSinglePage.Frameworks.ResponseFrameworks.Contracts;

namespace MvcSinglePage.ApplicationServices.Services.Contracts
{
    public interface IApplicationService<TPost, TPut, TDelete, TGet, TGetAll>
    {
        Task<IResponse<TPost>> PostAsync(TPost obj);

        Task<IResponse<TPut>> PutAsync(TPut obj);

        Task<IResponse<TDelete>> DeleteAsync(TDelete obj);

        Task<IResponse<TGet>> GetByIdAsync(TGet obj);

        Task<IResponse<List<TGetAll>>> GetAllAsync();

    }
}
