using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using C3xPAWM.Controllers;
using C3xPAWM.Models.Entities;
using C3xPAWM.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace C3xPAWM.Models.InputModel
{
    public class UserRoleInputModel
    {
        
        public string Email { get; set; }
        public Categoria Ruolo {get; set;}
        public int Token {get; set;}
  
    }
}