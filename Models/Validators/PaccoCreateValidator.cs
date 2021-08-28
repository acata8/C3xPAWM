using C3xPAWM.Models.InputModel;
using FluentValidation;

namespace C3xPAWM.Models.Validators
{
    public class PaccoCreateValidator : AbstractValidator<PaccoCreateInputModel>
    {
        

        public PaccoCreateValidator()
        {
            RuleFor(x => x.ProvinciaD).NotEmpty().WithMessage("Provincia di destinazione obbligatoria");
            RuleFor(x => x.ProvinciaP).NotEmpty().WithMessage("Provincia di partenza obbligatoria");

            RuleFor(x => x.CittaD).NotEmpty().WithMessage("Città di destinazione obbligatoria");
            RuleFor(x => x.CittaP).NotEmpty().WithMessage("Città di partenza obbligatoria");

            RuleFor(x => x.RegioneD).NotEmpty().WithMessage("Regione di destinazione obbligatoria");
            RuleFor(x => x.RegioneP).NotEmpty().WithMessage("Regione di partenza obbligatoria");

            RuleFor(x => x.ViaD).NotEmpty().WithMessage("Via di destinazione obbligatoria");
            RuleFor(x => x.ViaP).NotEmpty().WithMessage("Via di partenza obbligatoria");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email utente obbligatoria").EmailAddress().WithMessage("Formato non valido!");
        }
    }
}
