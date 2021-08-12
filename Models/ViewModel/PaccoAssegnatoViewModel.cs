using System.Collections.Generic;
using C3xPAWM.Customizations.ModelBinder;
using C3xPAWM.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace C3xPAWM.Models.ViewModel
{
    
    [ModelBinder(BinderType = typeof(PaccoInputModelBinder))]
    public class PaccoAssegnatoViewModel
    {
        public int PaccoId { get; set; }
        
        public Pacco Pacco {get; set;}
        public int CorriereId { get; set; }

        public PaccoAssegnatoViewModel(int id, int corriereid)
        {
            CorriereId = corriereid;
            PaccoId = id;
        }
    }
}