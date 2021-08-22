using System.Collections.Generic;
using C3xPAWM.Models.InputModel;

namespace C3xPAWM.Models.ViewModel
{
    public class OrdiniUtenteViewModel : IPagination
    {
        
        public ListViewModel<PaccoViewModel> Ordini { get; set; }

        public ElencoListInputModel Input { get; set; }

        public string Search => Input.Search;        
        public string OrderBy => throw new System.NotImplementedException();
        public bool Ascending => throw new System.NotImplementedException();

        int IPagination.TotalResults => Ordini.TotaleElenco;

        int IPagination.Page => Input.Page;

        int IPagination.Limit => Input.Limit;

    }
}