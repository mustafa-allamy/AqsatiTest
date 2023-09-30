using Application.CQRS.User.Dtos;
using Common.Forms;
using Common.Responses;
using Mediator;
using OneOf;

namespace Application.CQRS.User.Forms
{
    public class UpdateUserForm : BaseForm<UpdateUserForm, Domain.Entities.UserAndPermissions.User>, ICommand<OneOf<SuccessServiceResponse<UserDto>, FailServiceResponse>>
    {
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }


    }
}