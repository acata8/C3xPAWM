
using System.Collections.Generic;
using System.Linq;
using C3xPAWM.Models.Entities;
using C3xPAWM.Models.Enums;

namespace C3xPAWM.Models.ViewModel
{
    public class NegozioViewModel
    {
    public NegozioViewModel()
    {
        Pubblicita = new List<PubblicitaViewModel>();
    }

        public virtual ApplicationUser ProprietarioUser {get; set;}

        public int NegozioId { get; set; }
        public Categoria Categoria { get; set; } 
        public string Nome { get; set; }
        public string Telefono { get; set; }
        public Tipologia Tipologia { get; set; }
        public string Proprietario { get;  set; }
        public string Regione { get; set; }
        public string Provincia { get; set; }
        public string Citta { get; set; }
        public string Via { get; set; }
        public List<PubblicitaViewModel> Pubblicita {get; set;}
        public int Token { get;  set; }
        
        public int Revocato {get; set;}

        public static NegozioViewModel FromEntity(Negozio negozio){
            return new NegozioViewModel{
                NegozioId = negozio.NegozioId,
                Nome = negozio.Nome,
                Telefono = negozio.Telefono,
                Tipologia = negozio.Tipologia,
                Categoria = negozio.Categoria,
                Via = negozio.Via,
                Revocato = negozio.Revocato,
                Citta = negozio.Citta,
                Proprietario = negozio.Proprietario,
                Provincia = negozio.Provincia,
                Regione = negozio.Regione,
                Token = negozio.Token,
                Pubblicita = negozio.Pubblicita
                                    .Select(p => PubblicitaViewModel.FromEntity(p))
                                    .ToList()
            };
        }
    }
}