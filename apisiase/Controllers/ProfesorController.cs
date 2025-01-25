using System.ComponentModel.DataAnnotations;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace apisiase.Controllers
{

    public class ProfesorController : BaseController
    {
        private readonly IGenericRepository<Profesor> _repository;
        public ProfesorController(IGenericRepository<Profesor> repository)
        {
            _repository = repository;
        }

        [HttpPost("Add")]
        public async Task<ActionResult> Create(Profesor profesor)
        {

            var validationProblems = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(
                profesor, new ValidationContext(profesor), validationProblems, true
            );

            if (!isValid)
            {
                return BadRequest(validationProblems.ToValidationProblemDetails());
            }
            var result = await _repository.add(profesor);

            if (result == 0)
            {
                throw new Exception("Error al insertar");
            }
            return Ok(profesor);

        }

        [HttpPost("Vote/{id}/Dislike")]
        public async Task<ActionResult> DisLike(int id ){
            var profesor = await _repository.getByID(id);

            if(profesor == null){
                return NotFound();
            }
            profesor.Dislikes++;

            var result = await _repository.update(profesor);

            if(result == 0 ){
                throw new Exception("Error al actualizar");
            }

            return Ok(profesor);
            
        }
        [HttpPost("Vote/{id}/like")]
        public async Task<ActionResult> Like(int id ){
            var profesor = await _repository.getByID(id);

            if(profesor == null){
                return NotFound();
            }

            profesor.Likes++;

            var result = await _repository.update(profesor);

            if(result ==0 ){
                throw new Exception("Error al actualizar");
            }

            return Ok(profesor);
            
        }


        [HttpPut("Update/{id}")]
        public async Task<ActionResult> Update(int id, Profesor profesor)
        {
            profesor.Id = id;

            var result = await _repository.update(profesor);
            if (result == 0)
            {
                throw new Exception("Error al actualizar");
            }

            return Ok(profesor);
        }

        [HttpGet("GetNew")]
        public ActionResult GetNew()
        {
            var ds = new Profesor();

            return Ok(ds);
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            var result = await _repository.getAllAsync();

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



    }
}
