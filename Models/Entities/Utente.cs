using System;
using System.Collections.Generic;
using C3xPAWM.Models.Enums;

namespace C3xPAWM.Models.Entities
{
    public class Utente
    {
      
        public int UtenteId { get; private set; }
        public string Nome { get; private set; }
        public Categoria Categoria { get; private set; }
        public string Telefono { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public virtual ICollection<Pacco> Pacchi {get; private set; }
    

     public Utente(string email, string password, string nome, string telefono)
         {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new System.ArgumentException($"'{nameof(email)}' non può essere Null o uno spazio vuoto.", nameof(email));
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new System.ArgumentException($"'{nameof(password)}' non può essere Null o uno spazio vuoto.", nameof(password));
            }

            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new System.ArgumentException($"'{nameof(nome)}' non può essere Null o uno spazio vuoto.", nameof(nome));
            }
            
            this.Nome = nome;
            this.Password = password;
            this.Email = email;
            this.Telefono = telefono;
            this.Pacchi = new HashSet<Pacco>();
            this.Categoria = Categoria.UTENTE;
         }

        public Utente()
        {
            
        }
          public void CambiaTelefono(string telefono)
        {
            if (string.IsNullOrWhiteSpace(telefono))
            {
                throw new ArgumentException($"'{nameof(telefono)}' non può essere Null o uno spazio vuoto.", nameof(telefono));
            }

            Telefono = telefono;
        }

        public void CambiaNome(string nominativo)
        {
            if (string.IsNullOrWhiteSpace(nominativo))
            {
                throw new ArgumentException($"'{nameof(nominativo)}' non può essere Null o uno spazio vuoto.", nameof(nominativo));
            }

            Nome = nominativo;
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
    }
}