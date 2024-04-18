using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TheComputerShop.Models;
using TheComputerShop.Models.DTO;
using TheComputerShop.Repository.IRepository;
using XAct.Security;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TheComputerShop.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RolesController : ControllerBase
    {

        private readonly IRolesRepository _rolesRepository;

        private readonly IMapper _mapper;

        private readonly RespuestaApi _repuestaApi;


        public RolesController(IRolesRepository rolesRepository, IMapper mapper)
        {
            _rolesRepository = rolesRepository;

            _mapper = mapper;

            this._repuestaApi = new RespuestaApi();
        }


        #region GetAllRoles
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public IActionResult Get()
        {
            var listRoles = _rolesRepository.GetRoles();

            var listaRolesDto = new List<RolesDto>();

            foreach (var lista in listRoles)
            {
                listaRolesDto.Add(_mapper.Map<RolesDto>(lista));
            }

            return Ok(listaRolesDto);
        }
        #endregion

        
    }
}
