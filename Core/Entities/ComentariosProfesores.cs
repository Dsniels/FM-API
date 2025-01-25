using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class ComentariosProfesores : Base
{
    [Required]
    public int ProfesorID {get; set;}
    
    [Required]
    public string Descripcion {get; set;}


}
