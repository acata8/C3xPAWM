using System;
using C3xPAWM.Models.Entities;
using C3xPAWM.Models.Enums;

namespace C3xPAWM.Models.ViewModel
{
    public class UtenteViewModel
    {
        public int UtenteId { get; set; }
        public string Nominativo { get; set; }
        public Categoria Categoria {get; set;}
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public static UtenteViewModel FromEntity(Utente Utente)
        {
           return new UtenteViewModel{
               UtenteId = Utente.UtenteId,
                Nominativo = Utente.Nome,
                Categoria = Categoria.UTENTE,
                Telefono = Utente.Telefono,
                Email =Utente.Email,
                Password = Utente.Password
           };
        }
    }
}