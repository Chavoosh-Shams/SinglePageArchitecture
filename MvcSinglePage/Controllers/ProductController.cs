using Microsoft.AspNetCore.Mvc;
using MvcSinglePage.ApplicationServices.Dtos.ProductDtos;
using MvcSinglePage.ApplicationServices.Services.Contracts;

namespace MvcSinglePage.Controllers
{
    public class ProductController : Controller
    {

        #region [- Private Fiedls -]
        private readonly IProductApplicationService _productApplicationService;
        #endregion

        #region [- Ctor() -]
        public ProductController(IProductApplicationService productApplicationService)
        {
            _productApplicationService = productApplicationService;
        }
        #endregion

        #region [- Index() -]
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region [- PostProduct() -]
        [HttpPost]
        public async Task<IActionResult> PostProduct([FromBody] PostProductDto postProductDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _productApplicationService.PostAsync(postProductDto);

            if (!result.IsSuccessful)
                return BadRequest(result.Message);

            return Ok(result.Value);
        }
        #endregion

        #region [- PutProduct() -]
        [HttpPut]
        public async Task<IActionResult> PutProduct([FromBody] PutProductDto putProductDto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _productApplicationService.PutAsync(putProductDto);

            if (!result.IsSuccessful)
                return BadRequest(result.Message);

            return Ok(result.Value);
        }
        #endregion

        #region [- DeleteProduct() -]
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(DeleteProductDto deleteProductDto)
        {
            var result = await _productApplicationService.DeleteAsync(deleteProductDto);

            if (!result.IsSuccessful)
                return NotFound();

            return Ok(result.Value);
        }
        #endregion

        #region [- GetProductById() -]
        [HttpGet]
        public async Task<IActionResult> GetProductById(GetProductDto getProductDto)
        {
            var product = await _productApplicationService.GetByIdAsync(getProductDto);
            var response = product.Value;
            return Ok(response);
        }
        #endregion

        #region [- GetAll() -]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productApplicationService.GetAllAsync();
            return Ok(products.Value);
        }
        #endregion
    }
}
