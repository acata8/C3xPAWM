using System.ComponentModel.DataAnnotations;

namespace C3xPAWM.Models.InputModel
{
    public class CorriereInputModel
    {
        public int CorriereId {get; set; }
        public string Nominativo { get; set; }
        public string Telefono { get; set; }
    }
}