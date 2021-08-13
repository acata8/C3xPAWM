using System.ComponentModel.DataAnnotations;

namespace C3xPAWM.Models.InputModel
{
    public class CorriereInputModel
    {
        public int CorriereId {get; set; }

        [Required(ErrorMessage = "Il Nominativo è obbligatorio")]
        public string Nominativo { get; set; }

        [Required(ErrorMessage = "Il numero è obbligatorio"),
        MaxLength(10, ErrorMessage = "Numero telefonico non valido"),
        MinLength(10, ErrorMessage = "Numero telefonico non valido" )]
        public string Telefono { get; set; }


    }
}