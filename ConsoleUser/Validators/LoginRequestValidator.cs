using FluentValidation;
using ConsoleUser.DTO;

namespace ConsoleUser.Validators
{
    public class LoginRequestValidator: AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.UserEmail).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
