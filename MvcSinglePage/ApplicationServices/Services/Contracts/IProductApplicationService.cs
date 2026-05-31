using MvcSinglePage.ApplicationServices.Dtos.ProductDtos;

namespace MvcSinglePage.ApplicationServices.Services.Contracts
{
    public interface IProductApplicationService :
        IApplicationService
         <PostProductDto, PutProductDto, DeleteProductDto, GetProductDto, GetAllProductDto>
    {

    }
}
