using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace apisiase.Controllers
{
    public class FilesController : BaseController
    {

        private readonly IBlobRepository _blobRepository;
        private readonly IGenericRepository<BlobFile> _genericRepository;

        public FilesController(IBlobRepository blobRepository, IGenericRepository<BlobFile> genericRepository)
        {
            _blobRepository = blobRepository;
            _genericRepository = genericRepository;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult> GetFiles()
        {

            var result = await _genericRepository.getAllAsync();
            return Ok(result);

        }

        [HttpGet("GetByID/{id}")]
        public async Task<ActionResult> GetByID(int id)
        {
            var result = await _genericRepository.getByID(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("Add")]
        public async Task<ActionResult> UploadFile(string tag, string title)
        {
            IFormFile file = Request.Form.Files[0];
            if (file == null)
            {
                return BadRequest();
            }

            var result = await _blobRepository.UploadFile(
                "blobcontainer",
                file.OpenReadStream(),
                file.ContentType,
                file.FileName
                );

            if (result == null)
            {
                return BadRequest();
            }

            var blob = new BlobFile
            {
                Tag = tag,
                Title = title,
                Uri = result
            };

            var record = await _genericRepository.add(blob);

            if (record == null)
            {
                return BadRequest();
            }

            return Ok(blob);

        }


    }
}
