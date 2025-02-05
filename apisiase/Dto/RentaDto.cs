using System;

namespace apisiase.Dto;

public class RentaDto
{


    public bool Entregado {get; set;} = false;
    public DateTimeOffset FechaRenta {get;set;}
    public DateTimeOffset FechaEntrega {get; set;}
    public string UsuarioID {get; set;}
    public int BicicletaID {get; set;}
    


}


