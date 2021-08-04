using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using C3xPAWM.Models.Enums;



namespace C3xPAWM.Models.Entities
{
    public partial class Negozio
    {
        
        public int NegozioId { get; private set; }
        public string Nome { get; private set; }
        public int Token { get; private set; }
        public string Telefono { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public Tipologia Tipologia { get; private set; }
        public Categoria Categoria { get; private set; }
        public string Regione { get; private set; }
        public string Provincia { get; private set; }
        public string Citta { get; private set; }
        public string Via { get; private set; }

        public virtual List<Pubblicita> Pubblicita {get; private set; }

        public void CambiaNome(string nuovoNome){
            if(string.IsNullOrWhiteSpace(nuovoNome)){
                throw new ArgumentException("Il negozio deve avere un nome valido");
            }
            Nome = nuovoNome;
        }

        public void CambiaTelefono(string nuovoNumero){
            //Ritorna vero se non contiene carattere
            bool controlloNumero = nuovoNumero.Any(x => !char.IsLetter(x));
            if(string.IsNullOrWhiteSpace(nuovoNumero) || nuovoNumero.Length !=10 || !controlloNumero){
                throw new ArgumentException("Numero telefonico non valido");
            }
            
            Telefono = nuovoNumero;
        }

    }

        
}
