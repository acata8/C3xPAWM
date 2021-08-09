using System.Collections.Generic;
using C3xPAWM.Models.Enums;

namespace C3xPAWM.Models.Entities
{
    public class Utente
    {
        public int UtenteId { get; private set; }
        public string Nome { get; private set; }
        public Categoria Categoria { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public virtual ICollection<Pacco> Pacchi {get; private set; }
    

     public Utente(string email, string password, string nome)
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
            this.Pacchi = new HashSet<Pacco>();
            this.Categoria = Categoria.NEGOZIO;
         }
    }
}