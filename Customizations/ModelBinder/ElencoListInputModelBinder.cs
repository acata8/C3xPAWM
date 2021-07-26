using System;
using System.Threading.Tasks;
using C3xPAWM.Models.InputModel;
using C3xPAWM.Models.Options;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;

namespace C3xPAWM.Customizations.ModelBinder
{
    public class ElencoListInputModelBinder : IModelBinder
    {
        private readonly IOptionsMonitor<ElencoOptions> elencoOptions;

        public ElencoListInputModelBinder(IOptionsMonitor<ElencoOptions> elencoOptions)
        {
            this.elencoOptions = elencoOptions;

        }
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            string search = bindingContext.ValueProvider.GetValue("Search").FirstValue;
            int page = Convert.ToInt32(bindingContext.ValueProvider.GetValue("Page").FirstValue);
            var ascending = Convert.ToBoolean(bindingContext.ValueProvider.GetValue("Ascending").FirstValue);
            string orderby = bindingContext.ValueProvider.GetValue("OrderBy").FirstValue;
           
            var inputModel = new ElencoListInputModel(search, page, orderby, ascending, elencoOptions.CurrentValue.PerPage, elencoOptions.CurrentValue.Order);
            
            bindingContext.Result = ModelBindingResult.Success(inputModel);

            return Task.CompletedTask;
        }
    }
}