using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using C3xPAWM.Models.Entities;
using C3xPAWM.Models.Enums;

namespace C3xPAWM.Models.InputModel
{
    public class UserRoleInputModel
    {
        public string Email { get; set; }
        public Categoria Ruolo {get; set;}
  
    }
}