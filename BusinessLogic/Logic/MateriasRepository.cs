using BusinessLogic.Persistence;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Logic
{
    public class MateriasRepository : IMateriasRepository
    {
        private readonly SiaseDbContext _context;

        public MateriasRepository(SiaseDbContext context)
        {
            _context = context;
        }

        public async Task<Materia> getMateriaByIdAsnc(int id)
        {
            return await _context.Materia.FirstOrDefaultAsync(m => m.Id == id);

        }

        public async Task<IReadOnlyList<Materia>> getMateriasAsync()
        {
            return await _context.Materia
                .Include(m => m.Profesor).ToListAsync();

        }

        public async Task<IReadOnlyList<string>> getMateriasListAsync()
        {
            return await _context.Materia.Select(m=>m.Nombre).Distinct().ToListAsync();
        }

 

        // public async Task<IReadOnlyList<Materia>> getMateriasWhereProfesor(int id)
        // {
        //     return await _context.Materia
        //         .Include(m => m.Carrera)
        //         .Include(m => m.Profesor)
        //         .Where(m => m.Profesor.Id == id).ToListAsync();
        // }


        public async Task<IReadOnlyList<Profesor>> getProfesoresByMateriaID(int id)
        {
            return await _context.Materia.Where(m=>m.Id==id).Select(x=>x.Profesor).ToListAsync();
        }
    }
}
