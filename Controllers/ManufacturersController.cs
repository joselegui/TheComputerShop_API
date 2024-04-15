using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheComputerShop.Models.DTO;
using TheComputerShop.Models;
using TheComputerShop.Repository;
using TheComputerShop.Repository.IRepository;

namespace TheComputerShop.Controllers
{

    [ApiController]
    [Route("api/manufacturers")]
    public class ManufacturersController : ControllerBase
    {
        private readonly IManufacturersRepository _manufacturersRepository;

        private readonly IMapper _mapper;

        public ManufacturersController(IMapper mapper, IManufacturersRepository manufacturersRepository)
        {
            _mapper = mapper;
            _manufacturersRepository = manufacturersRepository;
        }

        #region GetManufacturers
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public IActionResult GetManufacturers()
        {
            var listManufacturers = _manufacturersRepository.GetManufacturers();

            var listManufacturersDto = new List<ManufacturersDto>();

            foreach (var item in listManufacturers)
            {
                listManufacturersDto.Add(_mapper.Map<ManufacturersDto>(item));
            }

            return Ok(listManufacturersDto);
        }
        #endregion

        #region GetManufacture
        [AllowAnonymous]
        [HttpGet("{manufactureId}", Name = "GetManufacture")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetManufacture(int manufactureId)
        {
            var itemsManufacture = _manufacturersRepository.GetManufacturers(manufactureId);

            if (itemsManufacture == null)
            {
                return BadRequest();
            }

            var itemsManufactureDto = _mapper.Map<ManufacturersDto>(itemsManufacture);

            return Ok(itemsManufactureDto);
        }


        #endregion

        #region CreateManufacturers
        [Authorize(Roles = "Super Administrator, Administrator")]
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ManufacturersDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateManufacturers([FromBody] ManufacturersDto manufacturersDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (manufacturersDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_manufacturersRepository.ExistManufacture(manufacturersDto.Name))
            {
                ModelState.AddModelError("", "La marca ya existe");

                return StatusCode(400, ModelState);
            }

            var manufacture = _mapper.Map<Manufacturers>(manufacturersDto);

            if (!_manufacturersRepository.CreateManufacture(manufacture))
            {
                ModelState.AddModelError("", $"Something went wrong while saving {manufacture.Name}");

                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetManufacture", new { manufactureId = manufacture.Id }, manufacture);

        }
        #endregion

        #region UpdateManufacturers
        [Authorize(Roles = "Super Administrator, Administrator")]
        [HttpPatch("{manufacturersId:int}", Name = "UpdateManufacturers")]
        [ProducesResponseType(201, Type = typeof(ManufacturersDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateManufacturers(int manufacturersId, [FromBody] ManufacturersDto manufacturersDto)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            if (manufacturersDto == null || manufacturersId != manufacturersDto.Id)
            {
                return BadRequest(ModelState);
            }

            var manufacturers = _mapper.Map<Manufacturers>(manufacturersDto);

            if (!_manufacturersRepository.UpdateManufacture(manufacturers))
            {
                ModelState.AddModelError("", $"Algo salio mal al actualizar la Marca {manufacturersDto.Name}");
            }

            return NoContent();
        }


        #endregion

        #region SearchManufacturers
        [AllowAnonymous]
        [HttpGet("Search")]
        public IActionResult Search(string name)
        {
            try
            {
                var result = _manufacturersRepository.SearchManufacturers(name.Trim());

                if (result.Any()) { return Ok(result); }

                return NotFound("No se encontro la marca del producto");
            }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error recuperando datos"); }
        }
        #endregion
    }
}
