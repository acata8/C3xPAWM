using System.Collections.Generic;
using C3xPAWM.Models.InputModel;

namespace C3xPAWM.Models.ViewModel
{
    public class ElencoListViewModel
    {
        public List<NegozioViewModel> Negozi { get; set; }
        public ElencoListInputModel Input { get; set; }
    }
}