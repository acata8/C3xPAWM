using C3xPAWM.Models.Enums;

namespace C3xPAWM.Models.ViewModel
{
    public class NegozioViewModel
    {

        private Categoria Categoria { get; } 
        public string Nome { get; set; }
        public string Citta { get; set; }
        public string Via { get; set; }
        public string Telefono { get; set; }
        public Tipologia Tipologia { get; set; }

        public NegozioViewModel(string nome, string citta, string via, string telefono, Tipologia tipologia){
            this.Categoria = Categoria.COMMERCIANTE;
            this.Nome = nome;
            this.Citta = citta;
            this.Via = via;
            this.Telefono = telefono;
            this.Tipologia = tipologia;
        }   

    }
}