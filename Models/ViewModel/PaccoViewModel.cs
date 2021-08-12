using C3xPAWM.Models.Entities;
using C3xPAWM.Models.Enums;

namespace C3xPAWM.Models.ViewModel
{

    public class PaccoViewModel
    {
        public int PaccoId { get; set; }
        public StatoPacco StatoPacco { get; set; }
        public string Provincia { get; set; }
        public string Citta { get; set; }
        public string Via { get;  set; }
        public Utente Utente { get; set; }
        public Negozio Negozio {get; set; }
        
    }
}