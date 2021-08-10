using System.ComponentModel.DataAnnotations;
using C3xPAWM.Models.Entities;

namespace C3xPAWM.Models.InputModel
{
    public class PubblicitaInputModel
    {
        
        public int PubblicitaId { get; set; }
        public int NegozioId {get; set;}
        public Negozio Negozio;
        
        [Required(ErrorMessage = "Il nome dell'evento è obbligatorio")]
        public string NomeEvento { get; set; }

        [Range(1, 30, ErrorMessage = "La durata deve essere di almeno 1 giorno e massimo 30 giorni")]
        public int Durata { get; set; }
        //Riferita in termini di giorni
        public int Attiva {get; set;}
    
        

    }
}