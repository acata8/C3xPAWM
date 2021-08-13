using System.ComponentModel.DataAnnotations;

namespace C3xPAWM.Models.Enums
{
    public enum Categoria
    {
        Commerciante,
        Corriere,
        
        [Display(Name = "Amministratore")]
        Administrator,
        Utente
    }
}