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

        bool IPagination.Tipologia => throw new System.NotImplementedException();

        bool IPagination.Citta => throw new System.NotImplementedException();

        string IPagination.OrderBy => throw new System.NotImplementedException();

        bool IPagination.Ascending => throw new System.NotImplementedException();

        bool IPagination.Nome => throw new System.NotImplementedException();

        string IPagination.Luogo => throw new System.NotImplementedException();

        bool IPagination.Paginare => throw new System.NotImplementedException(); 
    }
}