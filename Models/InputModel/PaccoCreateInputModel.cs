using System.ComponentModel.DataAnnotations;
using C3xPAWM.Controllers;
using C3xPAWM.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace C3xPAWM.Models.InputModel
{
    public class PaccoCreateInputModel
    {
         
        
        public int PaccoId { get; set; }

        [Required(ErrorMessage = " Destinazione obbligatoria")]
        public string Destinazione {get; set;}

        [Required(ErrorMessage = "Partenza obbligatoria")]
        public string Partenza {get; set;}


        public int NegozioId { get; set; }

        [Required(ErrorMessage = "Email obbligatoria")]
        [Remote(action: nameof(NegozioController.emailTrovata), controller:"Negozio",ErrorMessage = "Email non esistente")]
        public string Email {get; set;}
        public ApplicationUser Utente;
        
        public string UtenteId { get; set; }

    }
}