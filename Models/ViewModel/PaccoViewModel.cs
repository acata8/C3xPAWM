using System;
using C3xPAWM.Models.Entities;
using C3xPAWM.Models.Enums;

namespace C3xPAWM.Models.ViewModel
{
    public class PaccoViewModel
    {
        public int PaccoId { get; set; }
        public string Destinazione {get; set;}
        public string Regione { get; set; }
        public string Provincia { get; set; }
        public string Citta { get; set; }
        public string Via { get; set; }
        public string Partenza {get; set;}

        public int NegozioId { get; set; }
        public Negozio Negozio {get;set;}

        public int CorriereId { get; set; }
        public Corriere Corriere {get;set;}

        public string UtenteId {get; set;}
        public ApplicationUser Utente {get; set;}
        public StatoPacco StatoPacco {get; set;}
        public DateTime Data {get; set;}
    }
}