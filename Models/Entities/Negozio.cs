using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using C3xPAWM.Models.Enums;

#nullable disable

namespace C3xPAWM.Models.Entities
{
    public partial class Negozio
    {
        public Negozio(string nome, Tipologia tipologia)
        {
            if(string.IsNullOrWhiteSpace(nome)){
                throw new ArgumentException("negozio deve avere un nome");
            }
            if(Tipologia.IsDefined(tipologia)){
                throw new ArgumentException("negozio deve avere una tipologia valida");
            }
            Nome = nome;
            Categoria = Categoria.NEGOZIO;
            Tipologia = tipologia;
            Token = 5;
            Indirizzi = new HashSet<Indirizzo>();
        }

        public int NegozioId { get; private set; }
        public string Nome { get; private set; }
        public int Token { get; private set; }
        public string Telefono { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public Tipologia Tipologia { get; private set; }
        public Categoria Categoria { get; private set; }

        public virtual ICollection<Indirizzo> Indirizzi { get; set; }

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

        public Indirizzo PrimoIndirizzo(){
            return Indirizzi.First();
        }

    }

        
}
