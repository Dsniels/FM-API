using System.ComponentModel.DataAnnotations;
using apisiase.Dto;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apisiase.Controllers
{

    public class MateriaController : BaseController
    {
        private readonly IGenericRepository<Profesor> _profesorRepostory;
        private readonly IGenericRepository<Carrera> _carreraRepostory;
        private readonly IGenericRepository<Materia> _repository;
        private readonly IMateriasRepository _materiasRepository;

        public MateriaController(IGenericRepository<Profesor> profesorRepository, IGenericRepository<Carrera> carreraRepostory, IGenericRepository<Materia> repository, IMateriasRepository materiasRepository)
        {
            _profesorRepostory = profesorRepository;
            _carreraRepostory = carreraRepostory;
            _repository = repository;
            _materiasRepository = materiasRepository;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll([FromQuery] MateriaSpecificationParams materiaParams)
        {
            var spec = new MateriaWithProfesorAndCarreraSpecification(materiaParams);

            var records = await _repository.getAllWithSpec(spec);


            return Ok(records);

        }
        [HttpGet("MateriasList")]
        public async Task<ActionResult> GetMateriasList()
        {
            var result = await _materiasRepository.getMateriasListAsync();
            return Ok(result);
        }


        [HttpDelete("DeleteByID/{id}")]
        public async Task<ActionResult> DeleteByID(int id)
        {
            var record = await _repository.getByID(id);
            if (record == null)
            {
                return NotFound();
            }

            var result = await _repository.DeleteEntity(record);

            return Ok(result);
        }


        [HttpGet("GetByID/{id}")]
        public async Task<IActionResult> GetByID(int id)
        {

            var materia = await _materiasRepository.getMateriaByIdAsnc(id);

            if (materia == null)
            {
                return NotFound();
            }


            return Ok(materia);

        }

        [HttpGet("GetNew")]
        public ActionResult getNew()
        {
            var ds = new MateriaDto();

            return Ok(ds);
        }
       [HttpPost("Vote/{id}/Dislike")]
        public async Task<ActionResult> DisLike(int id ){
            var materia = await _repository.getByID(id);

            if(materia == null){
                return NotFound();
            }
            materia.Dislikes++;

            var result = await _repository.update(materia);

            if(result == 0 ){
                throw new Exception("Error al actualizar");
            }

            return Ok(materia);
            
        }
        [HttpPost("Vote/{id}/like")]
        public async Task<ActionResult> Like(int id ){
            var materia = await _repository.getByID(id);

            if(materia == null){
                return NotFound();
            }

            materia.Likes++;

            var result = await _repository.update(materia);

            if(result ==0 ){
                throw new Exception("Error al actualizar");
            }

            return Ok(materia);
            
        }


        [HttpPut("Update/{id}")]
        public async Task<ActionResult> Update(int id, Materia materia)
        {
            materia.Id = id;
            var profesor = await _profesorRepostory.getByID(materia.ProfesorID);
            if(profesor == null){
                return BadRequest("No se encontro el profesor especificado");
            }
            materia.Profesor = profesor;
            var result = await _repository.update(materia);

            if (result == 0)
            {
                throw new Exception("Error al actualizar");
            }

            return Ok(materia);
        }


        [HttpPost("Add")]
        public async Task<ActionResult> Create(MateriaDto materia)
        {


            var validationProblems = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(
                materia, new ValidationContext(materia), validationProblems, true
            );

            if (!isValid)
            {
                return BadRequest(validationProblems.ToValidationProblemDetails());
            }
            var profesor = await _profesorRepostory.getByID(materia.ProfesorId);
            if (profesor == null )
                return BadRequest("Profesor  no encontrados");
            var newMateria = new Materia
            {
                Nombre = materia.Nombre,
                Profesor = profesor,
                ProfesorID = materia.ProfesorId
            };
            var result = await _repository.add(newMateria);
            if (result == 0)
            {
                throw new Exception("error al insertar");
            }

            return Ok(result);
        }


    }
}
