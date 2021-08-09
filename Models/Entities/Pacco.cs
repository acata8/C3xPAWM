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


    }

}