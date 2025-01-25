using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IMateriasRepository
    {
        Task<Materia> getMateriaByIdAsnc(int id);
        Task<IReadOnlyList<Materia>> getMateriasAsync();
        Task<IReadOnlyList<string>> getMateriasListAsync();
        Task<IReadOnlyList<Profesor>> getProfesoresByMateriaID(int id);

    }
}
