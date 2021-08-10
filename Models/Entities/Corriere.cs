using System;
using System.Collections.Generic;
using C3xPAWM.Models.Enums;

namespace C3xPAWM.Models.Entities
{
    public class Corriere
    {
        public int CorriereId {get; private set; }
        public string Nominativo { get; private set; }
        public string Telefono { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public virtual ICollection<Pacco> Pacchi {get; private set; }
        public Categoria Categoria { get; private set; }

         public Corriere(string email, string password, string nominativo, string telefono)
         {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new System.ArgumentException($"'{nameof(email)}' non può essere Null o uno spazio vuoto.", nameof(email));
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new System.ArgumentException($"'{nameof(password)}' non può essere Null o uno spazio vuoto.", nameof(password));
            }

            if (string.IsNullOrWhiteSpace(nominativo))
            {
                throw new System.ArgumentException($"'{nameof(nominativo)}' non può essere Null o uno spazio vuoto.", nameof(nominativo));
            }

            if (string.IsNullOrWhiteSpace(telefono))
            {
                throw new System.ArgumentException($"'{nameof(telefono)}' non può essere Null o uno spazio vuoto.", nameof(telefono));
            }

            this.Nominativo = nominativo;
            this.Password = password;
            this.Email = email;
            this.Telefono = telefono;
            this.Pacchi = new HashSet<Pacco>();
            this.Categoria = Categoria.CORRIERE;
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

            Nominativo = nominativo;
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
        public Corriere()
         {
             
         }
    }
}