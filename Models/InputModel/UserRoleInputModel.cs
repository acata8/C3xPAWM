using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using C3xPAWM.Models.Entities;
using C3xPAWM.Models.Enums;

namespace C3xPAWM.Models.InputModel
{
    public class UserRoleInputModel
    {
        [Required(ErrorMessage = "Email obbligatoria")]
        [EmailAddress(ErrorMessage = "Formato mail non valido")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ruolo obbligatorio")]
        [Display(Name = "Ruolo")]
        public Categoria Ruolo {get; set;}
  
    }
}