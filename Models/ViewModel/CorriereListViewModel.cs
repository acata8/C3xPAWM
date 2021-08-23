using C3xPAWM.Models.InputModel;

namespace C3xPAWM.Models.ViewModel
{
    public class CorriereListViewModel : IPagination
    {
         public ListViewModel<CorriereViewModel> Corrieri { get; set; }
        public ElencoListInputModel Input { get; set; }

        public string OrderBy => Input.OrderBy;

        public bool Ascending => Input.Ascending;

        string IPagination.Search => Input.Search;

        int IPagination.TotalResults => Corrieri.TotaleElenco;

        int IPagination.Page => Input.Page;

        int IPagination.Limit => Input.Limit;
    }
}