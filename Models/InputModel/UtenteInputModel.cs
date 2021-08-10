using System.ComponentModel.DataAnnotations;

namespace C3xPAWM.Models.InputModel
{
    public class UtenteInputModel
    {
        public int UtenteId {get; set; }

        [Required(ErrorMessage = "Il Nominativo è obbligatorio")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Il numero è obbligatorio"),
        MaxLength(10, ErrorMessage = "Numero telefonico non valido"),
        MinLength(10, ErrorMessage = "Numero telefonico non valido" )]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "La mail è obbligatoria")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La password è obbligatoria"),
        MinLength(5, ErrorMessage = "Minimi 5 caratteri")]
        public string Password { get; set; }
    }

}
