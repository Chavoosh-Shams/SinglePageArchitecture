using Microsoft.AspNetCore.Mvc;
using MvcSinglePage.ApplicationServices.Dtos.PersonDtos;
using MvcSinglePage.ApplicationServices.Services.Contracts;

namespace MvcSinglePage.Controllers
{
    public class PersonController : Controller
    {

        #region [- Private Fields -]
        private readonly IPersonApplicationService _personApplicationService;
        #endregion

        #region [- Ctor() -]
        public PersonController(IPersonApplicationService personApplicationService)
        {
            _personApplicationService = personApplicationService;
        }
        #endregion

        #region [- Index() -]
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region [- Post() -]
        [HttpPost]
        public async Task<IActionResult> PostPerson([FromBody] PostPersonDto postPersonDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _personApplicationService.PostAsync(postPersonDto);

            if (!result.IsSuccessful)
                return BadRequest(result.Message);

            return Ok(result.Value);
        }
        #endregion

        #region [- Put() -]
        [HttpPut]
        public async Task<IActionResult> PutPerson([FromBody] PutPersonDto putPersonDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _personApplicationService.PutAsync(putPersonDto);

            if (!result.IsSuccessful)
                return BadRequest(result.Message);

            return Ok(result.Value);
        }
        #endregion

        #region [- DeletePerson() -]
        [HttpDelete]
        public async Task<IActionResult> DeletePerson(DeletePersonDto deletePersonDto)
        {
            var result = await _personApplicationService.DeleteAsync(deletePersonDto);

            if (!result.IsSuccessful)
                return NotFound();

            return Ok(result.Value);
        }
        #endregion

        #region [- GetPersonById() -]
        [HttpGet]
        public async Task<IActionResult> GetPersonById(GetPersonDto getPersonDto)
        {
            var person = await _personApplicationService.GetByIdAsync(getPersonDto);
            var response = person.Value;
            return Ok(response);
        }
        #endregion

        #region [- GetAll() -]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var persons = await _personApplicationService.GetAllAsync();
            return Ok(persons.Value);
        }
        #endregion

    }
}
