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
    }
}