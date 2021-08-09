using System;
using C3xPAWM.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace C3xPAWM.Models.ViewComponents
{
    public class TipologieViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(){
            
            var tipologie = (Tipologia[])Enum.GetValues(typeof(Tipologia));

            return View(tipologie);
        }

    }
}

