

using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class ComentariosMaterias : Base
{
    [Required]
     public int MateriaID {get; set;}

     [Required]
     public string Descripcion {get; set; }

}
