using System.Collections.Generic;
using C3xPAWM.Models.InputModel;

namespace C3xPAWM.Models.ViewModel
{
    public class ElencoListViewModel
    {
        public ListViewModel<NegozioViewModel> Negozi { get; set; }
        public ElencoListInputModel Input { get; set; }
    }
}