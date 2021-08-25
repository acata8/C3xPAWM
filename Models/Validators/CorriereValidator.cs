using C3xPAWM.Models.InputModel;
using FluentValidation;

namespace C3xPAWM.Models.Validators
{
    public class CorriereValidator : AbstractValidator<CorriereInputModel>
    {
        
        public CorriereValidator()
        {
            RuleFor(x => x.Nominativo)
                    .NotEmpty()
                    .WithMessage("Il nominativo è obbligatorio")
                    .MinimumLength(3).WithMessage("Nome corriere: caratteri minimi {MinLength}")
                    .MaximumLength(50).WithMessage("Nome corriere: caratteri massimi {MaxLength}")
                    .Matches(@"^[a-zA-Z]*$").WithMessage("Formato non valido, il nominativo può contenere solo lettere!");;

            RuleFor(x => x.Telefono)
                    .NotEmpty()
                    .MinimumLength(10).WithMessage("Il numero telefonico non è valido")
                    .MaximumLength(10).WithMessage("Il numero telefonico non è valido")
                    .Matches(@"^\d{10}$").WithMessage("Formato non valido, Non può contenere caratteri");
            
            
        }

       
    }
}

       