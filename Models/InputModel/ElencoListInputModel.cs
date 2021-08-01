using System;
using System.Linq;
using C3xPAWM.Customizations.ModelBinder;
using C3xPAWM.Models.Options;
using Microsoft.AspNetCore.Mvc;

namespace C3xPAWM.Models.InputModel
{
    [ModelBinder(BinderType = typeof(ElencoListInputModelBinder))]
    public class ElencoListInputModel
    {
       
        public string Search { get; }
        public int Page { get; }
        public string OrderBy { get; }
        public bool Ascending { get; }
        public int Limit { get; }

        public int Offset { get; }


        public ElencoListInputModel(string search, int page, string orderby, bool ascending, int limit, ElencoOrderOptions elencoOptions)
        {
            //Sanitizzazione
            if (!elencoOptions.Allow.Contains(orderby))
            {
                orderby = elencoOptions.By;
                ascending = elencoOptions.Ascending;
            }

            Search = search ?? "";
            Page = Math.Max(1, page);;
            Limit = Math.Max(1, limit);
            Offset = (Page - 1) * Limit;
            OrderBy = orderby;
            Ascending = ascending;
        }

    }
}