using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Azure.Storage.Blobs.Models;

namespace Core.Entities;

public class Renta : Base
{
    [Required]
    public bool Entregado {get; set;} = false;
    public DateTimeOffset FechaRenta {get;set;}
    public DateTimeOffset FechaEntrega {get; set;}
    public string UsuarioID {get; set;}
    public int BicicletaID {get; set;}
    
    public Usuario Usuario {get; set;}


}
