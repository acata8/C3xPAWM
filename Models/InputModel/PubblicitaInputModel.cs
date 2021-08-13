using System.ComponentModel.DataAnnotations;
using C3xPAWM.Controllers;
using C3xPAWM.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace C3xPAWM.Models.InputModel
{
    public class PubblicitaInputModel
    {

        public int PubblicitaId { get; set; }
        public int NegozioId { get; set; }
        public Negozio Negozio {get; set;}

        [Required(ErrorMessage = "Il nome dell'evento è obbligatorio")]
        public string NomeEvento { get; set; }

        [Range(1, 30, ErrorMessage = "La durata deve essere di almeno {1} giorno e massimo {2} giorni oppure il numero di TOKEN disponibili")]
        public int Durata { get; set; }
        //Riferita in termini di giorni
        public int Attiva { get; set; }

    }
}