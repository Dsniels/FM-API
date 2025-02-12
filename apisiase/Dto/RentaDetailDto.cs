using System;
using Core.Entities;

namespace apisiase.Dto;

public class RentaDetailDto : Renta
{
    public Bicicleta Bicicleta {get; set;}
    public Usuario Usuario {get; set;}

}
