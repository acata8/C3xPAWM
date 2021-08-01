using System.Collections.Generic;

namespace C3xPAWM.Models.ViewModel
{
    public class ListViewModel<T>
    {
        public List<T> Elenco {get; set;}

        public int TotaleElenco {get; set;}
    }
}