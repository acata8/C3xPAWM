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
        public string Luogo { get; }
        public int Page { get; }
        public string OrderBy { get; }
        public bool Ascending { get; }
        public int Limit { get; }

        public int Offset { get; }

       public bool Tipologia { get; set; }

        public bool Citta { get; set; }
        public bool Nome { get; set; }
        public bool Paginare { get; set; }
        public ElencoListInputModel(string search, string luogo, int page, string orderby, bool ascending, int limit, ElencoOrderOptions elencoOptions, bool tipologia = false, bool citta = false, bool nome = false, bool paginare = true)
        {
            //Sanitizzazione
            if (!elencoOptions.Allow.Contains(orderby))
            {
                orderby = elencoOptions.By;
                ascending = elencoOptions.Ascending;
            }

            Search = search ?? "";
            Luogo = luogo ?? "";
            Page = Math.Max(1, page);
            Limit = Math.Max(1, limit);
            Offset = (Page - 1) * Limit;
            OrderBy = orderby;
            Tipologia = tipologia;
            Citta = citta;
            Nome = nome;
            Paginare = paginare;
            Ascending = ascending;
        }
    }
}