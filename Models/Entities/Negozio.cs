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

        public virtual ICollection<Pubblicita> Pubblicita {get; private set; }

        public virtual ICollection<Pacco> Pacchi {get; private set; }

        public Negozio()
        {
            
        }

        public Negozio(string nome, string telefono, string provincia, 
        string regione, string citta, string via, string tipologia,
        string email, string password)
        {
            #region ControlliEmpty
            if(string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome non valido");
            if (string.IsNullOrWhiteSpace(telefono))
            {
                throw new ArgumentException($"'{nameof(telefono)}' non può essere Null o uno spazio vuoto.", nameof(telefono));
            }

            if (string.IsNullOrWhiteSpace(provincia))
            {
                throw new ArgumentException($"'{nameof(provincia)}' non può essere Null o uno spazio vuoto.", nameof(provincia));
            }

            if (string.IsNullOrWhiteSpace(regione))
            {
                throw new ArgumentException($"'{nameof(regione)}' non può essere Null o uno spazio vuoto.", nameof(regione));
            }

            if (string.IsNullOrWhiteSpace(citta))
            {
                throw new ArgumentException($"'{nameof(citta)}' non può essere Null o uno spazio vuoto.", nameof(citta));
            }

            if (string.IsNullOrWhiteSpace(via))
            {
                throw new ArgumentException($"'{nameof(via)}' non può essere Null o uno spazio vuoto.", nameof(via));
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException($"'{nameof(email)}' non può essere Null o uno spazio vuoto.", nameof(email));
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException($"'{nameof(password)}' non può essere Null o uno spazio vuoto.", nameof(password));
            }
            #endregion
            Nome = nome;
            Telefono = telefono;
            Provincia = provincia;
            Regione = regione;
            Citta = citta;
            Via = via;
            settaTipologia(tipologia);
            Categoria = Categoria.NEGOZIO;
            Email = email;
            Password = password;
            Token = 5;
            Pubblicita = new HashSet<Pubblicita>();
            Pacchi = new HashSet<Pacco>();
        }

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

        public void CambiaVia(string via){
            if (string.IsNullOrWhiteSpace(via))
            {
                throw new ArgumentException($"'{nameof(via)}' non può essere Null o uno spazio vuoto.", nameof(via));
            }
            Via = via;
        }

         public void CambiaRegione(string regione){
            if (string.IsNullOrWhiteSpace(regione))
            {
                throw new ArgumentException($"'{nameof(regione)}' non può essere Null o uno spazio vuoto.", nameof(regione));
            }
            Regione = regione;
        }

        public void CambiaEmail(string email){
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException($"'{nameof(email)}' non può essere Null o uno spazio vuoto.", nameof(email));
            }
            Email = email;
        }

        public void CambiaPassword(string password){
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException($"'{nameof(password)}' non può essere Null o uno spazio vuoto.", nameof(password));
            }
            Password = password;
        }

         public void CambiaProvincia(string provincia){
            if (string.IsNullOrWhiteSpace(provincia))
            {
                throw new ArgumentException($"'{nameof(provincia)}' non può essere Null o uno spazio vuoto.", nameof(provincia));
            }
            Provincia = provincia;
        }
        public void CambiaCitta(string citta){
            if (string.IsNullOrWhiteSpace(citta))
            {
                throw new ArgumentException($"'{nameof(citta)}' non può essere Null o uno spazio vuoto.", nameof(citta));
            }
            Citta = citta;
        }
        public void settaTipologia(string tipologia){
            Tipologia tipoInput;
            
            if(Enum.TryParse(tipologia, true, out tipoInput)){
                Tipologia = tipoInput;
                
            }else
                Tipologia = Tipologia.INDEFINITA;
        }

        public void DecrementaToken(int durata){
            if(durata > 0){
                Token -= durata;
            }
        }

    }

        
}
