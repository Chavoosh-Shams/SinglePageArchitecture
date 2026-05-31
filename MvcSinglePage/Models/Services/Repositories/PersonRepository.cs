using Microsoft.EntityFrameworkCore;
using MvcSinglePage.Frameworks.ResponseFrameworks;
using MvcSinglePage.Frameworks.ResponseFrameworks.Contracts;
using MvcSinglePage.Models.DomainModels.PersonAggregates;
using MvcSinglePage.Models.Services.Contracts;
using System.Net;

namespace MvcSinglePage.Models.Services.Repositories
{
    public class PersonRepository : IPersonRepository
    {

        #region [- Private Fields() -]
        private readonly ProjectDbContext _context;
        #endregion

        #region [- Ctor() -]
        public PersonRepository(ProjectDbContext context)
        {
            _context = context;
        }
        #endregion

        #region [- InsertAsync() -]
        public async Task<IResponse<Person>> InsertAsync(Person person)
        {
            if (person == null)
            {
                return new Response<Person>(
                    false,
                    HttpStatusCode.BadRequest,
                    ResponseMessages.NullInput,
                    null
                    );
            }
            await _context.Person.AddAsync(person);
            await _context.SaveChangesAsync();
            return new Response<Person>(
                true,
                HttpStatusCode.Created,
                ResponseMessages.SuccessfullOperation,
                person);
        }
        #endregion

        #region [- UpdateAsync() -]
        public async Task<IResponse<Person>> UpdateAsync(Person person)
        {
            if (person == null)
            {
                return new Response<Person>(
                    false,
                    HttpStatusCode.BadRequest,
                    ResponseMessages.NullInput,
                    null
                    );
            }
            var exsitingPerson = await _context.Person.FindAsync(person.Id);
            if (exsitingPerson == null)
            {
                return new Response<Person>(
                    false,
                    HttpStatusCode.NotFound,
                    ResponseMessages.NotFound,
                    null
                    );
            }
            else
            {
                _context.Entry(exsitingPerson).CurrentValues.SetValues(person);
                await _context.SaveChangesAsync();
                return new Response<Person>(
                    true,
                    HttpStatusCode.OK,
                    ResponseMessages.SuccessfullOperation,
                    exsitingPerson
                    );
            }

        }
        #endregion

        #region [- DeleteAsync() -]
        public async Task<IResponse<Person>> DeleteAsync(Person person)
        {
            if (person == null)
            {
                return new Response<Person>(
                    false,
                    HttpStatusCode.BadRequest,
                    ResponseMessages.NullInput,
                    null);
            }
            var exsitingPerson = await _context.Person.FindAsync(person.Id);
            if (exsitingPerson == null)
            {
                return new Response<Person>(
                    false,
                    HttpStatusCode.NotFound,
                    ResponseMessages.NotFound,
                    null
                    );
            }
            else
            {
                _context.Person.Remove(exsitingPerson);
                await _context.SaveChangesAsync();
                return new Response<Person>(
                    true,
                    HttpStatusCode.OK,
                    ResponseMessages.SuccessfullOperation,
                    exsitingPerson
                    );
            }
        }
        #endregion

        #region [- SelectPersonById() -]
        public async Task<IResponse<Person>> SelectById(Person person)
        {
            if (person == null)
            {
                return new Response<Person>(
                    false,
                    HttpStatusCode.BadRequest,
                    ResponseMessages.NullInput,
                    null);
            }
            else
            {
                var exsitingPeroson = await _context.Person.FindAsync(person.Id);
                if (exsitingPeroson == null)
                {
                    return new Response<Person>(
                    false,
                    HttpStatusCode.NotFound,
                    ResponseMessages.NotFound,
                    null
                    );
                }
                else
                {
                    return new Response<Person>(
                        true,
                        HttpStatusCode.OK,
                        ResponseMessages.SuccessfullOperation,
                        exsitingPeroson);
                }
            }
        }
        #endregion

        #region [- SelectAllAsync() -]
        public async Task<IResponse<IEnumerable<Person>>> SelectAllAsync()
        {
            var persons = await _context.Person.AsNoTracking().ToListAsync();
            return new Response<IEnumerable<Person>>(
                true,
                HttpStatusCode.OK,
                ResponseMessages.SuccessfullOperation,
                persons);

        }
        #endregion

    }
}
