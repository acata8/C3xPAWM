using System;
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


        public Pubblicita()
        {
        }

        public Pubblicita(Negozio negozio, string nomeEvento, int durata)
        {
            if (negozio is null)
            {
                throw new ArgumentNullException(nameof(negozio));
            }
            if (string.IsNullOrWhiteSpace(nomeEvento))
            {
                throw new ArgumentException($"'{nameof(nomeEvento)}' non può essere Null o uno spazio vuoto.", nameof(nomeEvento));
            }
            if (durata < 0)
            {
                throw new ArgumentNullException(nameof(durata));
            }

            this.NegozioId = negozio.NegozioId;
            this.NomeEvento = nomeEvento;
            this.Durata = durata;
            this.Attiva = 1;
        }

        
    }
}