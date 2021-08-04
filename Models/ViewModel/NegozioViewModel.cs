
using System.Collections.Generic;
using C3xPAWM.Models.Entities;
using C3xPAWM.Models.Enums;

namespace C3xPAWM.Models.ViewModel
{
    public class NegozioViewModel
    {
        public int NegozioId { get; set; }
        public Categoria Categoria { get; set; } 
        public string Nome { get; set; }
        public string Telefono { get; set; }
        public Tipologia Tipologia { get; set; }
        public string Regione { get; set; }
        public string Provincia { get; set; }
        public string Citta { get; set; }
        public string Via { get; set; }
        public List<Pubblicita> Pubblicita {get; set;}
    }
}