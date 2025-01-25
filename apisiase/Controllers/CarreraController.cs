using System.ComponentModel.DataAnnotations;
using apisiase.Dto;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace apisiase.Controllers
{

    public class CarreraController : BaseController
    {

        private readonly IGenericRepository<Carrera> _repository;   

        public CarreraController(IGenericRepository<Carrera> repository)
        {
            _repository = repository;
        }

        [HttpPost("Add")]
        public async Task<ActionResult> Create(Carrera carrera)
        {
            
            var validationProblems = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(
                carrera, new ValidationContext(carrera), validationProblems, true
            );

            if(!isValid){
                return BadRequest(validationProblems.ToValidationProblemDetails());
            }
            var result = await  _repository.add(carrera);

            if(result == 0)
            {
                throw new Exception("Error al insertar");
            }
            return Ok(carrera);  

        }

        [HttpGet("GetNew")]
        public async Task<ActionResult> GetNew() {

            var ds = new CarreraDto();

            return Ok(ds);
            
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult> getAll()
        {
            var result = await _repository.getAllAsync();

            return Ok(result);
        }



        [HttpPut("Update/{id}")]
        public async Task<ActionResult> Update(int id, Carrera carrera) {
            carrera.Id = id;
            var result = await _repository.update(carrera);
            if (result == 0) {
                throw new Exception("Error al actualizar");
            }

            return Ok(carrera);

        }


        [HttpDelete("DeleteByID/{id}")]
        public async Task<ActionResult> DeleteByID(int id)
        {
            var record = await _repository.getByID(id);
            if(record == null)
            {
                return NotFound();
            }

             var result = await _repository.DeleteEntity(record);

            return Ok(result);
        }
    }
}
