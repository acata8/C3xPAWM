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
            string luogo = bindingContext.ValueProvider.GetValue("Luogo").FirstValue;
            int page = Convert.ToInt32(bindingContext.ValueProvider.GetValue("Page").FirstValue);
            var ascending = Convert.ToBoolean(bindingContext.ValueProvider.GetValue("Ascending").FirstValue);
            string orderby = bindingContext.ValueProvider.GetValue("OrderBy").FirstValue;
            var tipologia = Convert.ToBoolean(bindingContext.ValueProvider.GetValue("Tipologia").FirstValue);
            var nome = Convert.ToBoolean(bindingContext.ValueProvider.GetValue("Nome").FirstValue);
            var ordCitta = Convert.ToBoolean(bindingContext.ValueProvider.GetValue("Citta").FirstValue);
            var paginare = true;
            var inputModel = new ElencoListInputModel(search, luogo, page, orderby, ascending, elencoOptions.CurrentValue.PerPage, elencoOptions.CurrentValue.Order, tipologia, ordCitta, nome, paginare);
            
            bindingContext.Result = ModelBindingResult.Success(inputModel);

            return Task.CompletedTask;
        }
    }
}