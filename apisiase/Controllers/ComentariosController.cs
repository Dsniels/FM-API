using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apisiase.Controllers
{
    public class ComentariosController : BaseController
    {
        private readonly IGenericRepository<ComentariosProfesores> _comentariosProfesor;
        private readonly IGenericRepository<ComentariosMaterias> _comentariosMaterias;

        public ComentariosController(
            IGenericRepository<ComentariosProfesores> comentariosProfesor,
            IGenericRepository<ComentariosMaterias> comentariosMaterias
        )
        {
            _comentariosMaterias = comentariosMaterias;
            _comentariosProfesor = comentariosProfesor;

        }

        [HttpGet("ComentariosMateria/{id}")]
        public async Task<ActionResult> ComentariosMateria(int id){
            var spec = new BaseSpecification<ComentariosMaterias>(x=>x.MateriaID == id);
            var result = await _comentariosMaterias.getAllWithSpec(spec);
            return Ok(result);
            
        }


        [HttpGet("ComentariosProfesor/{id}")]
        public async Task<ActionResult> getComentariosProfesor(int id){
            var spec = new BaseSpecification<ComentariosProfesores>(x=>x.ProfesorID == id);
            var result = await _comentariosProfesor.getAllWithSpec(spec);
            return Ok(result);
        }

        [HttpPost("AddComentarioMateria")]
        public async Task<ActionResult> AddComentarioMateria(ComentariosMaterias comentario){
            var validationProblems = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(comentario, new ValidationContext(comentario) ,validationProblems,true );

            if(!isValid){
                return BadRequest(validationProblems.ToValidationProblemDetails());
            }

            var result = await _comentariosMaterias.add(comentario);
            if(result == 0){
                throw new Exception("Error al agregar");
            }

            return Ok(comentario);


        }

        [HttpPost("AddComentarioProfesor")]
        public async Task<ActionResult> AddComentarioProfesor(ComentariosProfesores comentario){
            var validationProblems = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(comentario, new ValidationContext(comentario) ,validationProblems,true );

            if(!isValid){
                return BadRequest(validationProblems.ToValidationProblemDetails());
            }

            var result = await _comentariosProfesor.add(comentario);
            if(result == 0){
                throw new Exception("Error al agregar");
            }

            return Ok(comentario);

        }






    }
}
