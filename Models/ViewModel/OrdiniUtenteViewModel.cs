using System.Collections.Generic;
using C3xPAWM.Models.InputModel;

namespace C3xPAWM.Models.ViewModel
{
    public class OrdiniUtenteViewModel : IPagination
    {
        
        public ListViewModel<PaccoViewModel> Ordini { get; set; }

        public ElencoListInputModel Input { get; set; }

              
        int IPagination.TotalResults => Ordini.TotaleElenco;

        int IPagination.Page => Input.Page;

        int IPagination.Limit => Input.Limit;

        bool IPagination.Tipologia =>  Input.Tipologia;

        string IPagination.Search =>  Input.Search;

        string IPagination.OrderBy => Input.OrderBy;

        bool IPagination.Ascending => Input.Ascending;

         bool IPagination.Citta => Input.Citta ;


         bool IPagination.Nome => Input.Nome;


        string IPagination.Luogo => Input.Luogo;


        bool IPagination.Paginare  => Input.Paginare;
    }
}