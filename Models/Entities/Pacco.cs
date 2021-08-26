using System;
using C3xPAWM.Models.Enums;

namespace C3xPAWM.Models.Entities
{
    public class Pacco
    {
        public int PaccoId { get; set; }

        public string Destinazione {get; set;}
        public string Partenza {get; set;}

        public int NegozioId { get; set; }
        public virtual Negozio Negozio {get;set;}

        public int CorriereId { get; set; }
        public virtual Corriere Corriere {get;set;}

        public string UtenteId {get; set;}
        public virtual ApplicationUser Utente {get; set;}

        public StatoPacco StatoPacco {get; set;}
        public DateTime dataConsegna {get; set;}

        public Pacco(string dest, string part, int negozioId, string utenteId)
        {
            this.Destinazione = dest;
            this.Partenza = part;
            this.NegozioId = negozioId;
            this.UtenteId = utenteId;
            this.CorriereId = 6;
            StatoPacco = StatoPacco.NON_ASSEGNATO;
        }
        public Pacco()
        {
        }
        
        public void SettaCorriere(int id){
            this.CorriereId = id;
            StatoPacco = StatoPacco.ASSEGNATO;
        }

        public void RevocaCorriere(){
            this.CorriereId = 6;
            StatoPacco = StatoPacco.NON_ASSEGNATO;
        }

        public void SettaConsegnato(){
            StatoPacco = StatoPacco.CONSEGNATO;
        }
    }
}