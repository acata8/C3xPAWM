using System.Collections.Generic;
using C3xPAWM.Models.ViewModel;

namespace C3xPAWM.Models.Entities
{
    public class Pubblicita
    {
        public int PubblicitaId { get; set; }

        public int NegozioId { get; set; }
        public string NomeEvento { get; set; }
        public int Durata { get; set; }
        //Riferita in termini di giorni
        public int Attiva {get; set;}
        public virtual Negozio Negozio { get; set; }


    }
}