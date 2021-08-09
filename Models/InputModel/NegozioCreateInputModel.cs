using System.ComponentModel.DataAnnotations;

namespace C3xPAWM.Models.InputModel
{
    public class NegozioCreateInputModel
    {
        [Required(ErrorMessage = "Il nome è obbligatorio")]
        public string Nome {get; set; }

        [Required(ErrorMessage = "Il numero è obbligatorio"),
        MaxLength(10, ErrorMessage = "Numero telefonico non valido"),
        MinLength(10, ErrorMessage = "Numero telefonico non valido" )]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "La regione è obbligatoria")]
        public string Regione { get; set; }

        [Required(ErrorMessage = "La provincia è obbligatoria"),
        MaxLength(2, ErrorMessage = "Lunghezza massima 2 caratteri"),
        MinLength(2, ErrorMessage = "Lunghezza minima 2 caratteri" )]
        public string Provincia { get; set; }

        [Required(ErrorMessage = "La città è obbligatoria")]
        public string Citta { get; set; }
        [Required(ErrorMessage = "La via è obbligatoria"),]
        public string Via { get; set; }
        
        [Required(ErrorMessage = "La tipologia è obbligatoria"),]
        public string Tipologia { get;  set; }

        //TODO
        
        /*
        Vedere lezione 116 validazione client per verificare se l'email e' corretta
        */        
        public string Email { get;  set; }
        public string Password { get;  set; }

    }
}