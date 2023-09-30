using Application.CQRS.User.Dtos;
using Common.Forms;
using Common.Responses;
using Mediator;
using OneOf;
using System.ComponentModel.DataAnnotations;

namespace Application.CQRS.User.Forms
{
    public class RegisterForm : BaseForm<RegisterForm, Domain.Entities.UserAndPermissions.User>,
        ICommand<OneOf<SuccessServiceResponse<UserDto>, FailServiceResponse>>
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }

        public string LastName { get; set; }
        public string Email { get; set; }

        public string PhoneNumber { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public int DepartmentId { get; set; }
    }
}