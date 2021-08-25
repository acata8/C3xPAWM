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

        bool IPagination.Tipologia => Input.Tipologia;
        
        bool IPagination.Citta => Input.Citta;

        bool IPagination.Nome => Input.Nome;

        string IPagination.Luogo => Input.Luogo;

        bool IPagination.Paginare => Input.Paginare;
    }
}