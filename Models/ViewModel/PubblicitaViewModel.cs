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

        
    }
}