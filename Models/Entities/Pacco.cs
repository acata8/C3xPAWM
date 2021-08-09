using System;
using C3xPAWM.Models.Enums;

namespace C3xPAWM.Models.Entities
{
    public class Pacco
    {
        public int PaccoId { get; private set; }
        public StatoPacco StatoPacco { get; private set; }
        public string Provincia { get; private set; }
        public string Citta { get; private set; }
        public string Via { get; private set; }

        public int NegozioId { get; set; }
        public virtual Negozio Negozio { get; set; }

        public int CorriereId { get; set; }
        public virtual Corriere Corrieri { get; set; }

        public int UtenteId { get; private set; }
        public virtual Utente Utenti { get; set; }

        public Pacco(string provincia, string via, string citta, Utente utente, Negozio negozio)
        {
            if (string.IsNullOrWhiteSpace(provincia))
            {
                throw new System.ArgumentException($"'{nameof(provincia)}' non può essere Null o uno spazio vuoto.", nameof(provincia));
            }

            if (string.IsNullOrWhiteSpace(via))
            {
                throw new System.ArgumentException($"'{nameof(via)}' non può essere Null o uno spazio vuoto.", nameof(via));
            }

            if (string.IsNullOrWhiteSpace(citta))
            {
                throw new System.ArgumentException($"'{nameof(citta)}' non può essere Null o uno spazio vuoto.", nameof(citta));
            }

            if (utente is null)
            {
                throw new ArgumentNullException(nameof(utente));
            }

            if (negozio is null)
            {
                throw new ArgumentNullException(nameof(negozio));
            }

            this.Citta = citta;
            this.Via = via;
            this.Provincia = provincia;
            this.NegozioId = negozio.NegozioId;
            this.UtenteId = utente.UtenteId;
            this.StatoPacco = StatoPacco.NON_ASSEGNATO;
            this.CorriereId = 0;
        }

        public Pacco()
        {
            
        }

        public void SetCorriere(Corriere corriere){
            if (corriere is null)
            {
                throw new ArgumentNullException(nameof(corriere));
            }
            this.CorriereId = corriere.CorriereId;
        }

        
    }

}