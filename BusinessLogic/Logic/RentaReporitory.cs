using System;
using BusinessLogic.Persistence;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Logic;

public class RentaReporitory : IRentaRepository
{  
    private readonly SiaseDbContext _context;
    public RentaReporitory(SiaseDbContext context){
        _context = context;
    }
    public Task<IReadOnlyList<Renta>> GetAllwithSpec(ISpecification<Renta> spec)
    {
        throw new NotImplementedException();
    }

    public async Task<Renta> GetRentaByID(int id)
    {
        return await _context.Renta.FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IReadOnlyList<Renta>> GetRentaByUsuarioID(int userID)
    {
        return await _context.Renta.Where(r=>r.UsuarioID == userID).ToListAsync();
    }

    public async Task<IReadOnlyList<Renta>> GetRentas()
    {
     return await _context.Renta.ToListAsync();
    }
}
