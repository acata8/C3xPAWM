using System.Collections.Generic;
using C3xPAWM.Models.Enums;

namespace C3xPAWM.Models.Entities
{
    public class Corriere
    {
        public int CorriereId {get; private set; }
        public string Nominativo { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public virtual ICollection<Pacco> Pacchi {get; private set; }
         public Categoria Categoria { get; private set; }

         public Corriere(string email, string password, string nominativo)
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
            
            this.Nominativo = nominativo;
            this.Password = password;
            this.Email = email;
            this.Pacchi = new HashSet<Pacco>();
            this.Categoria = Categoria.CORRIERE;
         }

         public Corriere()
         {
             
         }
    }
}