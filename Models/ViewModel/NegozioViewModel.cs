using System;
using System.Data;
using C3xPAWM.Models.Enums;

namespace C3xPAWM.Models.ViewModel
{
    public class NegozioViewModel
    {

        private Categoria Categoria { get; set; } 
        public string Nome { get; set; }
        public string Citta { get; set; }
        public string Via { get; set; }
        public string Telefono { get; set; }
        public Tipologia Tipologia { get; set; }

        

        public static NegozioViewModel FromDataRow(DataRow negozioRow)
        {
            
            var NegozioViewModel = new NegozioViewModel{
                Categoria = Categoria.NEGOZIO,
                Nome = Convert.ToString(negozioRow["nome"]),
                Citta = Convert.ToString(negozioRow["citta"]),
                Via = Convert.ToString(negozioRow["via"]),
                Telefono = Convert.ToString(negozioRow["telefono"]),
                Tipologia = Enum.Parse<Tipologia>(Convert.ToString(negozioRow["tipologia"]))
            };

            return NegozioViewModel;     
        }
    }
}