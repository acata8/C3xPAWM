using System.ComponentModel.DataAnnotations;
using C3xPAWM.Controllers;
using C3xPAWM.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace C3xPAWM.Models.InputModel
{
    public class PaccoCreateInputModel
    {
        
        
        [Remote(action: nameof(NegozioController.emailTrovata), controller:"Negozio",ErrorMessage = "Email non esistente")]
        public string Email {get; set;}
        public int PaccoId { get; set; }
        public string Destinazione {get; set;}
        public string Partenza {get; set;}
        public int NegozioId { get; set; }
        public string RegioneP { get; set; }
        public string ProvinciaP { get; set; }
        public string CittaP { get; set; }
        public string ViaP { get; set; }

        public string RegioneD { get; set; }
        public string ProvinciaD { get; set; }
        public string CittaD { get; set; }
        public string ViaD { get; set; }
        
        public Negozio Negozio { get; set; }
        public ApplicationUser Utente;
        public string UtenteId { get; set; }

    }
}