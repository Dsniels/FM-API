using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Profesor : Base
    {

        [Required]
        public string Nombre { get; set; }
        [Required]
        public string PrimerApellido {get; set;}
        [Required]
        public string SegundoApellido {get; set;}
        public int Likes { get; set; } = 0;
        public int Dislikes {get; set;} = 0;

    }
}
