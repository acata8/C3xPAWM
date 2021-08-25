using C3xPAWM.Models.InputModel;

namespace C3xPAWM.Models.ViewModel
{
    public class PubblicitaListViewModel : IPagination
    {
        public ListViewModel<PubblicitaViewModel> NegoziPubblicizzati { get; set; }
        public ElencoListInputModel Input { get; set; }

        string IPagination.Search => Input.Search;

        int IPagination.TotalResults => NegoziPubblicizzati.TotaleElenco;

        int IPagination.Page => Input.Page;

        string IPagination.OrderBy => Input.OrderBy;

        bool IPagination.Ascending => Input.Ascending;

        int IPagination.Limit => Input.Limit;

        bool IPagination.Tipologia => throw new System.NotImplementedException();

        bool IPagination.Citta => throw new System.NotImplementedException();

        bool IPagination.Nome => throw new System.NotImplementedException();

        string IPagination.Luogo => throw new System.NotImplementedException();

        bool IPagination.Paginare => throw new System.NotImplementedException(); 
    }
}