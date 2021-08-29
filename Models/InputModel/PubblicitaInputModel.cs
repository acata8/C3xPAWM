using System;
using System.ComponentModel.DataAnnotations;
using C3xPAWM.Controllers;
using C3xPAWM.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace C3xPAWM.Models.InputModel
{
    public class PubblicitaInputModel
    {

        public int PubblicitaId { get; set; }
        public int NegozioId { get; set; }
        public Negozio Negozio {get; set;}

        public string NomeEvento { get; set; }
        public int Durata { get; set; }
        //Riferita in termini di giorni
        public int Attiva { get; set; }

        public DateTime DataInizio {get; set;}
        public DateTime DataFine {get; set;}

    }
}