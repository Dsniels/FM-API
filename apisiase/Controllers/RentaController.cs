using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apisiase.Controllers
{
    public class RentaController : BaseController
    {
        public readonly IRentaRepository _rentaRepository;
        public readonly IGenericRepository<Renta> _genericRepository;

        public RentaController(IRentaRepository rentaRepository, IGenericRepository<Renta> genericRepository)
        {
            _genericRepository = genericRepository;
            _rentaRepository = rentaRepository;
        }



        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] RentaSpecificationParams rentaParams){

            var RentaParams = new RentaWithUserAndBikeSpecification(rentaParams);

            var result = await _genericRepository.getAllWithSpec(RentaParams);

            return Ok(result);

        }

        [HttpGet("GetByID/{id}")]
        public async Task<IActionResult> GetByID(int id){
            var result = await _rentaRepository.GetRentaByID(id);
            if(result == null){
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddRenta(Renta renta){

            var result = await _genericRepository.add(renta);
            if(result == null){
                throw new Exception("Entity can not be added");
            }

            return Ok(renta);

        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateRenta(int id,Renta renta ){
            renta.Id = id;

            var result = await _genericRepository.update(renta);
            if(result== null){
                throw new Exception("Entity can not be Updated");
            }

            return Ok(renta);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteRenta(int id){
            var record = await _genericRepository.getByID(id);

            if(record == null){
                return NotFound();
            }

            await _genericRepository.DeleteEntity(record);

            return Ok();
        }
    }
}
