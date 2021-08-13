using System.Collections.Generic;
using C3xPAWM.Models.Entities;

namespace C3xPAWM.Models.ViewModel
{
    public class CorriereDashboardViewModel
    {
        public int CorriereId { get; set; }

        public Corriere Corriere { get; set; }

        public List<Pacco> Pacchi {get; set;}
    }
}