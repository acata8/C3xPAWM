using C3xPAWM.Models.Enums;
using C3xPAWM.Models.InputModel;
using FluentValidation;

namespace C3xPAWM.Models.Validators
{
    public class UserRoleValidator : AbstractValidator<UserRoleInputModel>
    {
        public UserRoleValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email utente obbligatoria").EmailAddress().WithMessage("Formato non valido!");

            
        }
    }
}