using System.Collections.Generic;
using C3xPAWM.Migrations;
using C3xPAWM.Models.Entities;

namespace C3xPAWM.Models.ViewModel
{
    public class PacchiListViewModel
    {

        public Corriere Corriere;
        public int CorriereId;
        public List<PaccoViewModel> Pacchi {get; set;}
        
    }
}