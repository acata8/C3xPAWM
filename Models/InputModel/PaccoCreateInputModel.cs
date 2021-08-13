using C3xPAWM.Controllers;
using C3xPAWM.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace C3xPAWM.Models.InputModel
{
    public class PaccoCreateInputModel
    {
         
        public int PaccoId { get; set; }

        public string Destinazione {get; set;}
        public string Partenza {get; set;}

        public int NegozioId { get; set; }

        [Remote(action: nameof(NegozioController.emailTrovata), controller:"Negozio",ErrorMessage = "Email non esistente")]
        public string Email {get; set;}
        public ApplicationUser Utente;
        
        public string UtenteId { get; set; }

    }
}