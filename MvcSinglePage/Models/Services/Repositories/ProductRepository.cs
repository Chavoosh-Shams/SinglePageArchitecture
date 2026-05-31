using Microsoft.EntityFrameworkCore;
using MvcSinglePage.Frameworks.ResponseFrameworks;
using MvcSinglePage.Frameworks.ResponseFrameworks.Contracts;
using MvcSinglePage.Models.DomainModels.ProductAggregates;
using MvcSinglePage.Models.Services.Contracts;
using System.Net;

namespace MvcSinglePage.Models.Services.Repositories
{
    public class ProductRepository : IProductRepository
    {

        #region [- Private Fields -]
        private readonly ProjectDbContext _context;
        #endregion

        #region [- Ctor() -]
        public ProductRepository(ProjectDbContext context)
        {
            _context = context;
        }
        #endregion

        #region [- InsertAsync() -]
        public async Task<IResponse<Product>> InsertAsync(Product product)
        {
            if (product == null)
            {
                return new Response<Product>(
                    false,
                    HttpStatusCode.BadRequest,
                    ResponseMessages.NullInput,
                    null
                    );
            }
            else
            {
                await _context.Product.AddAsync(product);
                await _context.SaveChangesAsync();
                return new Response<Product>(
                    true,
                    HttpStatusCode.Created,
                    ResponseMessages.SuccessfullOperation,
                    product
                    );
            }
        }
        #endregion

        #region [- UpdateAsync() -]
        public async Task<IResponse<Product>> UpdateAsync(Product product)
        {
            if (product == null)
            {
                return new Response<Product>(
                    false,
                    HttpStatusCode.BadRequest,
                    ResponseMessages.NullInput,
                    null);
            }
            var exsitingProduct = await _context.Product.FindAsync(product.Id);
            if (exsitingProduct == null)
            {
                return new Response<Product>(
                    false,
                    HttpStatusCode.NotFound,
                    ResponseMessages.NotFound,
                    null);
            }
            else
            {
                _context.Entry(exsitingProduct).CurrentValues.SetValues(product);
                await _context.SaveChangesAsync();
                return new Response<Product>(
                    true,
                    HttpStatusCode.OK,
                    ResponseMessages.SuccessfullOperation,
                    exsitingProduct
                    );
            }
        }
        #endregion

        #region [- DeleteAsync() -]
        public async Task<IResponse<Product>> DeleteAsync(Product product)
        {
            if (product == null)
            {
                return new Response<Product>(
                    false,
                    HttpStatusCode.BadRequest,
                    ResponseMessages.NullInput,
                    null);
            }
            var exsitingProduct = await _context.Product.FindAsync(product.Id);
            if (exsitingProduct == null)
            {
                return new Response<Product>(
                    false,
                    HttpStatusCode.NotFound,
                    ResponseMessages.NotFound,
                    null);
            }
            else
            {
                _context.Product.Remove(exsitingProduct);
                await _context.SaveChangesAsync();
                return new Response<Product>(
                    true,
                    HttpStatusCode.OK,
                    ResponseMessages.SuccessfullOperation,
                    exsitingProduct);
            }
        }
        #endregion

        #region [- SelectById() -]
        public async Task<IResponse<Product>> SelectById(Product product)
        {
            if (product == null)
            {
                return new Response<Product>(
                    false,
                    HttpStatusCode.BadRequest,
                    ResponseMessages.NullInput,
                    null);
            }
            else
            {
                var exsitingProduct = await _context.Product.FindAsync(product.Id);
                if (exsitingProduct == null)
                {
                    return new Response<Product>(
                        false,
                        HttpStatusCode.NotFound,
                        ResponseMessages.NotFound,
                        null);
                }
                else
                {
                    return new Response<Product>(
                        true,
                        HttpStatusCode.OK,
                        ResponseMessages.SuccessfullOperation,
                        exsitingProduct);
                }
            }
        }
        #endregion

        #region [- SelectAllAsync() -]
        public async Task<IResponse<IEnumerable<Product>>> SelectAllAsync()
        {
            var products = await _context.Product.ToListAsync();
            return new Response<IEnumerable<Product>>(
                true,
                HttpStatusCode.OK,
                ResponseMessages.SuccessfullOperation,
                products
                );
        }
        #endregion
    }
}
