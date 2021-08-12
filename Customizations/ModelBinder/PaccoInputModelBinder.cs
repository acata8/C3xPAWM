using System;
using System.Threading.Tasks;
using C3xPAWM.Models.ViewModel;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace C3xPAWM.Customizations.ModelBinder
{
    public class PaccoInputModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            int corriereId = Convert.ToInt32(bindingContext.ValueProvider.GetValue("IdCorriere").FirstValue);
            int paccoId = Convert.ToInt32(bindingContext.ValueProvider.GetValue("IdPacco").FirstValue);
            var inputModel = new PaccoAssegnatoViewModel(paccoId, corriereId);
            
            bindingContext.Result = ModelBindingResult.Success(inputModel);

            return Task.CompletedTask;
        }
    }
}