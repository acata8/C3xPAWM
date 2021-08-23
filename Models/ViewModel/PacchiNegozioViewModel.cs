using System.Collections.Generic;
using C3xPAWM.Models.Entities;
using C3xPAWM.Models.InputModel;

namespace C3xPAWM.Models.ViewModel
{
    public class PacchiNegozioViewModel
    {

         public Negozio Negozio;
        public int NegozioId;
        public List<PaccoViewModel> Pacchi {get; set;}
    }
}