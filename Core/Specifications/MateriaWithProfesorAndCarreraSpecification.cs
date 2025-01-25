using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class MateriaWithProfesorAndCarreraSpecification : BaseSpecification<Materia>
    {

        public MateriaWithProfesorAndCarreraSpecification(MateriaSpecificationParams materiaParams)

        : base(x => 
                    (string.IsNullOrEmpty(materiaParams.Search) || x.Nombre.Contains(materiaParams.Search)) &&
                    // (!materiaParams.Carrera.HasValue || x.Carrera.Id == materiaParams.Carrera) &&
                    (!materiaParams.Profesor.HasValue || x.Profesor.Id == materiaParams.Profesor)) 
        {
            AddInclude(p => p.Profesor);
            // AddInclude(p=>p.Carrera);
            ApplyPaging(materiaParams.PageSize * (materiaParams.PageIndex - 1), materiaParams.PageSize);


        } 

        public MateriaWithProfesorAndCarreraSpecification(int id) : base(x => x.Id == id)
        {
            // AddInclude(p => p.Carrera);
            AddInclude(p => p.Profesor);
        }
        
        
        
            
   }
}

