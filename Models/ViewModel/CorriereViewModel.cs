using System;
using C3xPAWM.Models.Entities;
using C3xPAWM.Models.Enums;

namespace C3xPAWM.Models.ViewModel
{
    public class CorriereViewModel
    {
        public int CorriereId { get; set; }
        public string Nominativo { get; set; }
        public Categoria Categoria {get; set;}
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public static CorriereViewModel FromEntity(Corriere corriere)
        {
           return new CorriereViewModel{
               CorriereId = corriere.CorriereId,
                Nominativo = corriere.Nominativo,
                Categoria = Categoria.Corriere,
                Telefono = corriere.Telefono,
           };
        }
    }
}