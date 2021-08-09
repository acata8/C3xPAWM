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
    }
}