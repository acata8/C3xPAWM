using System.Collections.Generic;
using C3xPAWM.Models.InputModel;

namespace C3xPAWM.Models.ViewModel
{
    public class OrdiniUtenteViewModel : IPagination
    {
        
        public ListViewModel<PaccoViewModel> Ordini { get; set; }

        public ElencoListInputModel Input { get; set; }

        public string Search => Input.Search;        
        int IPagination.TotalResults => Ordini.TotaleElenco;

        int IPagination.Page => Input.Page;

        int IPagination.Limit => Input.Limit;

        public string OrderBy => throw new System.NotImplementedException();
        public bool Ascending => throw new System.NotImplementedException();

    
        bool IPagination.Tipologia => throw new System.NotImplementedException();

        string IPagination.Search => throw new System.NotImplementedException();

        string IPagination.OrderBy => throw new System.NotImplementedException();

        bool IPagination.Ascending => throw new System.NotImplementedException();

        bool IPagination.Citta => throw new System.NotImplementedException();

        bool IPagination.Nome => throw new System.NotImplementedException();

        string IPagination.Luogo => throw new System.NotImplementedException();

        bool IPagination.Paginare  => throw new System.NotImplementedException(); 
    }
}