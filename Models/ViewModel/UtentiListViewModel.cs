using System.Collections.Generic;
using C3xPAWM.Models.InputModel;

namespace C3xPAWM.Models.ViewModel
{
    public class UtenteListViewModel : IPagination
    {
        public ListViewModel<UtenteViewModel> Utenti { get; set; }
        public ElencoListInputModel Input { get; set; }

        public string OrderBy => Input.OrderBy;

        public bool Ascending => Input.Ascending;

        string IPagination.Search => Input.Search;

        int IPagination.TotalResults => Utenti.TotaleElenco;

        int IPagination.Page => Input.Page;

        int IPagination.Limit => Input.Limit;

        bool IPagination.Tipologia => Input.Tipologia;

         bool IPagination.Citta => Input.Citta ;


        string IPagination.OrderBy => Input.OrderBy;

        bool IPagination.Ascending => Input.Ascending;

         bool IPagination.Nome => Input.Nome;


        string IPagination.Luogo => Input.Luogo;


        bool IPagination.Paginare => Input.Paginare;
    }
}