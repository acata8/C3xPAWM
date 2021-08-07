using System.Collections.Generic;
using C3xPAWM.Models.InputModel;

namespace C3xPAWM.Models.ViewModel
{
    public class ElencoListViewModel : IPagination
    {
        public ListViewModel<NegozioViewModel> Negozi { get; set; }
        public ElencoListInputModel Input { get; set; }

        string IPagination.Search => Input.Search;

        int IPagination.TotalResults => Negozi.TotaleElenco;

        int IPagination.Page => Input.Page;

        string IPagination.OrderBy => Input.OrderBy;

        bool IPagination.Ascending => Input.Ascending;

        int IPagination.Limit => Input.Limit;
    }
}