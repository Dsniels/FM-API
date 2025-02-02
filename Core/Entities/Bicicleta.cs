using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Microsoft.Extensions.Primitives;


namespace Core.Entities;

public class Bicicleta : Base
{

    public bool Disponible { get; set;} = true;
    [Required]
    public string Modelo {get; set;}




}
