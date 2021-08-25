using System.ComponentModel.DataAnnotations;
using C3xPAWM.Controllers;
using C3xPAWM.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace C3xPAWM.Models.InputModel
{
    public class NegozioInputModel
    {

        public int NegozioId {get; set;}
        public string Nome {get; set; }
        public string Telefono { get; set; }
        public string Regione { get; set; }
        public string Provincia { get; set; }
        public string Citta { get; set; }
        public string Via { get; set; }

        [Required(ErrorMessage = "Tipologia obbligatoria")]
        [Remote(action: nameof(NegozioController.TipologiaNonEsistente), controller:"Negozio",ErrorMessage = "Tipologia non esistente")]
        public Tipologia Tipologia { get;  set; }


    }
}