using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace apisiase.Controllers
{
    public class BicicletaController : BaseController
    {
        private readonly IGenericRepository<Bicicleta> _repository;
        public BicicletaController(IGenericRepository<Bicicleta> repository)
        {
            _repository = repository;
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _repository.getAllAsync();

            return Ok(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(Bicicleta bicicleta){
            var result = await _repository.add(bicicleta);            
            if(result == null){
                return BadRequest();
            }

            return Ok(bicicleta);            
        }
        


        [HttpPost("Update/{id}")]
        public async Task<IActionResult> Update(int id,Bicicleta bicicleta){
            bicicleta.Id = id;
            var result = await _repository.update(bicicleta);            
            if(result == null){
                return BadRequest();
            }

            return Ok(bicicleta);            
        }

        [HttpDelete("DeleteByID/{id}")]    
        public async Task<IActionResult> DeleteByID(int id){
            var record = await _repository.getByID(id);
            if(record == null){
                return NotFound();
            }

            var result = await _repository.DeleteEntity(record);

            return Ok(result);
        }


    }
}
