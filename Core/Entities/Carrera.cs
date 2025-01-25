using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Carrera : Base
    {
        [Required]
        public string Nombre { get; set; }

        [Required]
        [StringLength(4)]
        public string Abreviatura { get; set;}
    }
}
