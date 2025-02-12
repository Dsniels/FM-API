using apisiase.Dto;
using apisiase.Extensions;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace apisiase.Controllers
{
    public class RentaController : BaseController
    {
        private readonly IRentaRepository _rentaRepository;
        private readonly IGenericRepository<Bicicleta> _bicicletaRepostory;
        private readonly UserManager<Usuario> _userManager;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Renta> _genericRepository;

        public RentaController(IGenericRepository<Bicicleta> bicicletaRepostory,IRentaRepository rentaRepository,IMapper mapper ,IGenericRepository<Renta> genericRepository, UserManager<Usuario> userManager)
        {
            _bicicletaRepostory = bicicletaRepostory;
            _userManager = userManager;
            _mapper = mapper;
            _genericRepository = genericRepository;
            _rentaRepository = rentaRepository;
        }



        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] RentaSpecificationParams rentaParams)
        {

            var RentaParams = new RentaWithUserAndBikeSpecification(rentaParams);

            var result = await _genericRepository.getAllWithSpec(RentaParams);

            return Ok(result);

        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetMyRentas()
        {
            var user = await _userManager.BuscarUsuarioAsync(HttpContext.User);
            var RentaParams = new BaseSpecification<Renta>(x => x.UsuarioID == user.Id);

            var result = await _genericRepository.getAllWithSpec(RentaParams);


            return Ok(result);
        }



        [HttpGet("GetByID/{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            var result = await _rentaRepository.GetRentaByID(id);
            var usuario = await _userManager.FindByIdAsync(result.UsuarioID);
            var bicicleta = await _bicicletaRepostory.getByID(result.BicicletaID);
            if (usuario == null ||result == null||bicicleta == null)
            {
                return NotFound("No se encontro un registro");
            }
            var details = _mapper.Map<Renta, RentaDetailDto>(result);
            details.Bicicleta = bicicleta;
            details.Usuario = usuario;
            return Ok(details);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddRenta(RentaDto rentaDto)
        {
       
            var renta = _mapper.Map<RentaDto, Renta>(rentaDto);
            var today = DateTimeOffset.Now;
            renta.FechaRenta = today;
            var result = await _genericRepository.add(renta);
            if (result == null)
            {
                throw new Exception("Entity can not be added");
            }

            return Ok(renta);

        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateRenta(int id, Renta renta)
        {
            renta.Id = id;
            renta.FechaEntrega = DateTimeOffset.Now;
            var result = await _genericRepository.update(renta);
            if (result == null)
            {
                throw new Exception("Entity can not be Updated");
            }

            return Ok(renta);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteRenta(int id)
        {
            var record = await _genericRepository.getByID(id);

            if (record == null)
            {
                return NotFound();
            }

            await _genericRepository.DeleteEntity(record);

            return Ok();
        }
    }
}
