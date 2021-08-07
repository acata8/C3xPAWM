using C3xPAWM.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace C3xPAWM.Models.ViewComponents
{
    public class PaginationViewComponent : ViewComponent
    {
        
        public IViewComponentResult Invoke(IPagination model){
            
            return View(model);
        }

    }
}