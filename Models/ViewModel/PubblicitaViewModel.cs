using System;
using C3xPAWM.Models.Entities;

namespace C3xPAWM.Models.ViewModel
{
    public class PubblicitaViewModel
    {
        public int PubblicitaId { get; set; }
        public string NomeEvento { get; set; }
        public int Durata { get; set; }
        public int Attiva {get; set;}
        public Negozio Negozio;

      public static PubblicitaViewModel FromEntity(Pubblicita pubblicita){
            return new PubblicitaViewModel{
                PubblicitaId = pubblicita.PubblicitaId,
                NomeEvento = pubblicita.NomeEvento,
                Durata = pubblicita.Durata,
                Attiva = pubblicita.Attiva,
                Negozio = pubblicita.Negozio
            };
        }
        
    }
}