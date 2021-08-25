using C3xPAWM.Models.InputModel;
using FluentValidation;

namespace C3xPAWM.Models.Validators
{
    public class PaccoCreateValidator : AbstractValidator<PaccoCreateInputModel>
    {
        

        public PaccoCreateValidator()
        {
            RuleFor(x => x.Destinazione).NotEmpty().WithMessage("Il luogo di destinazione è obbligatorio");

            RuleFor(x => x.Partenza).NotEmpty().WithMessage("Il luogo del ritiro è obbligatorio");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email utente obbligatoria").EmailAddress().WithMessage("Formato non valido!");
        }
    }
}
