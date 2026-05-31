using MvcSinglePage.ApplicationServices.Dtos.PersonDtos;

namespace MvcSinglePage.ApplicationServices.Services.Contracts
{
    public interface IPersonApplicationService :
        IApplicationService
        <PostPersonDto, PutPersonDto, DeletePersonDto, GetPersonDto, GetAllPeronDto>
    {

    }
}
