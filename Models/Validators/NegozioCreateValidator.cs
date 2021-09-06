using C3xPAWM.Models.InputModel;
using FluentValidation;

namespace C3xPAWM.Models.Validators
{
    public class NegozioCreateValidator : AbstractValidator<NegozioInputModel>
    {

        public NegozioCreateValidator()
        {
            RuleFor(x => x.Nome)
                    .NotEmpty().WithMessage("Il nome è obbligatorio")
                    .MinimumLength(3).WithMessage("Nome negozio: caratteri minimi {MinLength}")
                    .MaximumLength(50).WithMessage("Nome negozio: caratteri massimi {MaxLength}");
                    

            RuleFor(x => x.Telefono)
                    .NotEmpty().WithMessage("Il numero è obbligatorio")
                    .MinimumLength(10).WithMessage("Telefono: Il numero telefonico non è valido")
                    .MaximumLength(10).WithMessage("Telefono: Il numero telefonico non è valido")
                    .Matches(@"^\d+$").WithMessage("Formato non valido, il numero telefonico non è valido");

            RuleFor(x => x.Regione)
                    .NotEmpty().WithMessage("La regione è obbligatoria")
                    .Matches(@"^[a-zA-Z]*$").WithMessage("Formato non valido, la regione può contenere solo lettere!");

            RuleFor(x => x.Provincia)
                    .NotEmpty().WithMessage("La provincia è obbligatoria")
                    .MinimumLength(2).WithMessage("Provincia: Errore nell'inserimento")
                    .MaximumLength(2).WithMessage("Provincia: Errore nell'inserimento")
                    .Matches(@"[A-Z]{2}$").WithMessage("Formato non valido, la provincia può contenere solo 2 caratteri maiuscoli");
;
            RuleFor(x => x.Citta)
                    .NotEmpty().WithMessage("La città è obbligatoria")
                    .Matches(@"^[a-zA-Z]*$").WithMessage("Formato non valido, la città può contenere solo lettere!");

            RuleFor(x => x.Via)
                    .NotEmpty().WithMessage("La via è obbligatoria");

        }
    }
}