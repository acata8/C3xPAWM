using C3xPAWM.Models.Entities;

namespace C3xPAWM.Models.ViewModel
{
    public class PacchiListViewModel
    {
        public ListViewModel<PaccoViewModel> PacchiNonAssegnati {get; set;}
         public ListViewModel<PaccoViewModel> PacchiCorriere {get; set;}

         public int CorriereId {get; set;}
    }
}