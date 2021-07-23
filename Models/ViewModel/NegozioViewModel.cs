using System;
using System.Collections.Generic;
using System.Data;
using C3xPAWM.Models.Entities;
using C3xPAWM.Models.Enums;

namespace C3xPAWM.Models.ViewModel
{
    public class NegozioViewModel
    {

        public Categoria Categoria { get; set; } 
        public string Nome { get; set; }
        public string Telefono { get; set; }
        public Tipologia Tipologia { get; set; }
        public List<IndirizzoViewModel> Indirizzo {get; set;}
        
    }
}