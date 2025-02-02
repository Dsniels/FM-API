using System;
using System.ComponentModel.DataAnnotations;
using Azure.Storage.Blobs.Models;

namespace Core.Entities;

public class Renta : Base
{
    [Required]
    public bool Entregado {get; set;} = false;
    public DateTimeOffset FechaRenta {get;set;}
    public DateTimeOffset FechaEntrega {get; set;}
    public int UsuarioID {get; set;}
    public int BicicletaID {get; set;}
    public Usuario Usuario {get; set;}


}
