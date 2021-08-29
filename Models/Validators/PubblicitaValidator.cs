using System;
using C3xPAWM.Models.InputModel;
using FluentValidation;

namespace C3xPAWM.Models.Validators
{
    public class PubblicitaValidator : AbstractValidator<PubblicitaInputModel>
    {
        public PubblicitaValidator()
        {
            RuleFor(x => x.NomeEvento)
                    .NotEmpty().WithMessage("Nome dell'evento obbligatorio")
                    .MinimumLength(2).WithMessage("Nome evento: caratteri minimi {MinLength}")
                    .MaximumLength(30).WithMessage("Nome evento: caratteri massimi {MaxLength}");

            RuleFor(x => x.Durata)
                    .NotEmpty().WithMessage("Durata non valida")
                    .InclusiveBetween(1, 30).WithMessage("La durata deve essere compresa tra 1 e il numero di token disponibili, ma ad un massimo di 30 giorni");
        }
    }
}