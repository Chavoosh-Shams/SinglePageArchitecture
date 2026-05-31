using MvcSinglePage.ApplicationServices.Dtos.ProductDtos;
using MvcSinglePage.ApplicationServices.Services.Contracts;
using MvcSinglePage.Frameworks.ResponseFrameworks;
using MvcSinglePage.Frameworks.ResponseFrameworks.Contracts;
using MvcSinglePage.Models.DomainModels.ProductAggregates;
using MvcSinglePage.Models.Services.Contracts;
using System.Net;

namespace MvcSinglePage.ApplicationServices.Services
{
    public class ProductApplicationService : IProductApplicationService
    {

        #region [- Private Fields -]
        private readonly IProductRepository _productRepository;
        #endregion

        #region [- Ctor() -]
        public ProductApplicationService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        #endregion

        #region [- PostAsync() -]
        public async Task<IResponse<PostProductDto>> PostAsync(PostProductDto postProductDto)
        {
            if (postProductDto == null)
            {
                return new Response<PostProductDto>(
                    false,
                    HttpStatusCode.BadRequest,
                    ResponseMessages.NullInput,
                    null
                    );
            }
            try
            {
                var prdouct = new Product()
                {
                    Title = postProductDto.Title,
                    DescriptionRecord = postProductDto.DescriptionRecord,
                    UnitPrice = postProductDto.UnitPrice,
                };
                var result = await _productRepository.InsertAsync(prdouct);
                if (!result.IsSuccessful)
                {
                    return new Response<PostProductDto>(
                        false,
                        HttpStatusCode.InternalServerError,
                        ResponseMessages.Error,
                        null);
                }
                return new Response<PostProductDto>(
                    true,
                    HttpStatusCode.Created,
                    ResponseMessages.SuccessfullOperation,
                    postProductDto);
            }
            catch (Exception)
            {
                return new Response<PostProductDto>(
                    false,
                    HttpStatusCode.InternalServerError,
                    ResponseMessages.Error,
                    null);
            }

        }
        #endregion

        #region [- PutAsync() -]
        public async Task<IResponse<PutProductDto>> PutAsync(PutProductDto putProductDto)
        {
            if (putProductDto == null)
            {
                return new Response<PutProductDto>(
                    false,
                    HttpStatusCode.BadRequest,
                    ResponseMessages.NullInput,
                    null
                    );
            }
            try
            {
                var product = new Product()
                {
                    Id = putProductDto.Id,
                    Title = putProductDto.Title,
                    DescriptionRecord = putProductDto.DescriptionRecord,
                    UnitPrice = putProductDto.UnitPrice,
                };
                var result = await _productRepository.UpdateAsync(product);
                if (!result.IsSuccessful)
                {
                    return new Response<PutProductDto>(
                        false,
                        HttpStatusCode.InternalServerError,
                        ResponseMessages.Error,
                        null);
                }
                return new Response<PutProductDto>(
                    true,
                    HttpStatusCode.OK,
                    ResponseMessages.SuccessfullOperation,
                    putProductDto);
            }
            catch (Exception)
            {
                return new Response<PutProductDto>(
                    false,
                    HttpStatusCode.InternalServerError,
                    ResponseMessages.Error,
                    null);
            }

        }
        #endregion

        #region [- DeleteAsync() -]
        public async Task<IResponse<DeleteProductDto>> DeleteAsync(DeleteProductDto deleteProductDto)
        {
            if (deleteProductDto == null)
            {
                return new Response<DeleteProductDto>(
                    false,
                    HttpStatusCode.BadRequest,
                    ResponseMessages.NullInput,
                    null
                    );
            }
            try
            {
                var product = new Product()
                {
                    Id = deleteProductDto.Id,
                    Title = deleteProductDto.Title,
                    DescriptionRecord = deleteProductDto.DescriptionRecord,
                    UnitPrice = deleteProductDto.UnitPrice,
                };
                var result = await _productRepository.DeleteAsync(product);
                if (!result.IsSuccessful)
                {
                    return new Response<DeleteProductDto>(
                        false,
                        HttpStatusCode.InternalServerError,
                        ResponseMessages.Error,
                        null);
                }
                return new Response<DeleteProductDto>(
                    true,
                    HttpStatusCode.OK,
                    ResponseMessages.SuccessfullOperation,
                    deleteProductDto);
            }
            catch (Exception)
            {
                return new Response<DeleteProductDto>(
                    false,
                    HttpStatusCode.InternalServerError,
                    ResponseMessages.Error,
                    null);
            }
        }
        #endregion

        #region [- GetByIdAsync() -]
        public async Task<IResponse<GetProductDto>> GetByIdAsync(GetProductDto getProductDto)
        {
            if (getProductDto == null)
            {
                return new Response<GetProductDto>(
                    false,
                    HttpStatusCode.BadRequest,
                    ResponseMessages.Error,
                    null
                    );
            }
            var product = new Product()
            {
                Id = getProductDto.Id,
            };
            var productDto = await _productRepository.SelectById(product);
            if (!productDto.IsSuccessful || productDto.Value == null)
            {
                return new Response<GetProductDto>(
                    false,
                    HttpStatusCode.NotFound,
                    ResponseMessages.NullInput,
                    null);
            }
            var responseDto = new GetProductDto
            {
                Id = productDto.Value.Id,
                Title = productDto.Value.Title,
                DescriptionRecord = productDto.Value.DescriptionRecord,
                UnitPrice = productDto.Value.UnitPrice,
            };
            return new Response<GetProductDto>(
               true,
               HttpStatusCode.OK,
               ResponseMessages.SuccessfullOperation,
               responseDto);
        }
        #endregion

        #region [- GetAllAsync() -]
        public async Task<IResponse<List<GetAllProductDto>>> GetAllAsync()
        {
            var products = await _productRepository.SelectAllAsync();
            if (!products.IsSuccessful || products.Value == null)
            {
                return new Response<List<GetAllProductDto>>(
                    false,
                    HttpStatusCode.NotFound,
                    ResponseMessages.NullInput,
                    null);
            }
            var result = products.Value.Select(product => new GetAllProductDto()
            {
                Id = product.Id,
                Title = product.Title,
                DescriptionRecord = product.DescriptionRecord,
                UnitPrice = product.UnitPrice,
            }).ToList();
            return new Response<List<GetAllProductDto>>(
                true,
                HttpStatusCode.OK,
                ResponseMessages.SuccessfullOperation,
                result);
        }
        #endregion

    }
}
