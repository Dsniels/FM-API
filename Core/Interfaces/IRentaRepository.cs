using System;
using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces;

public interface IRentaRepository
{
    Task<IReadOnlyList<Renta>> GetRentas();
    Task<Renta> GetRentaByID(int id);
    Task<IReadOnlyList<Renta>> GetRentaByUsuarioID(int userID);

    

}
