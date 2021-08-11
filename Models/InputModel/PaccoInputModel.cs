using System.ComponentModel.DataAnnotations;
using C3xPAWM.Controllers;
using C3xPAWM.Models.Entities;
using C3xPAWM.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace C3xPAWM.Models.InputModel
{
    public class PaccoInputModel
    {
        public int PaccoId { get; set; }
        public StatoPacco StatoPacco { get;  set; }

        [Required(ErrorMessage = "La provincia è obbligatoria"),
        MaxLength(2, ErrorMessage = "Lunghezza massima 2 caratteri"),
        MinLength(2, ErrorMessage = "Lunghezza minima 2 caratteri" )]
        public string Provincia { get; set; }

        [Required(ErrorMessage = "La città è obbligatoria")]
        public string Citta { get; set; }
        [Required(ErrorMessage = "La via è obbligatoria"),]
        public string Via { get; set; }
        public int NegozioId { get; set; }
        public int UtenteId { get; set; }
        public  Negozio Negozio { get; set; }
        public  Corriere Corriere { get; set; }
        public  Utente Utente { get; set; }

       //[Required(ErrorMessage = "Mail obbligatoria"),
        //Remote(action: nameof(NegozioController.emailTrovata), controller: "Negozio", ErrorMessage = "Email non trovata")]
        public string Email { get; set; }
    }
}