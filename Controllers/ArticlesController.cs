using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheComputerShop.Models;
using TheComputerShop.Models.DTO;
using TheComputerShop.Repository;
using TheComputerShop.Repository.IRepository;

namespace TheComputerShop.Controllers
{
    [ApiController]
    [Route("api/articles")]
    public class ArticlesController : ControllerBase
    {

        private readonly IArticleRepository _articleRepository;

        private readonly IMapper _mapper;


        public ArticlesController(IArticleRepository articleRepository, IMapper mapper)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
        }

        #region GetAllArticles
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public IActionResult GetArticles()
        {
            var listArticles = _articleRepository.GetAllArticles();

            var listArticleDto = new List<ArticleDto>();

            foreach (var article in listArticles)
            {
                listArticleDto.Add(_mapper.Map<ArticleDto>(article));
            }

            return Ok(listArticleDto);
        }
        #endregion

        #region GetArticle
        [AllowAnonymous]
        [HttpGet("{articleId}", Name = "GetArticle")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllArticle(int id)
        {
            var itemsArticle = _articleRepository.GetArticles(id);

            if (itemsArticle == null) 
            {
                return BadRequest();
            }

            var itemsArticleDto = _mapper.Map<ArticleDto>(itemsArticle);

            return Ok(itemsArticleDto);
        }

        #endregion

        #region CreateArticles
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ArticleDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateArticles([FromBody] ArticleDto articleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (articleDto == null)
            {
                return BadRequest(articleDto);
            }

            if (_articleRepository.ExistArticle(articleDto.Name))
            {
                ModelState.AddModelError("", "Article already exists");
                return StatusCode(404, ModelState);
            }

            var article = _mapper.Map<Articles>(articleDto);
            if (!_articleRepository.CreateArticle(article))
            {
                ModelState.AddModelError("", $"Something went wrong while saving {article.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetArticle", new { articleId = article.Id }, article);

        }
        #endregion

        #region UpdatePatchArticle
        [Authorize(Roles = "admin")]
        [HttpPatch("{articleId:int}", Name = "UpdatePatchArticle")]
        [ProducesResponseType(201, Type = typeof(ArticleDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePatchArticle(int articleId, [FromBody] ArticleDto articleDto)
        {
            if(!ModelState.IsValid) {  return BadRequest(ModelState); }

            if(articleDto == null || articleId != articleDto.Id)
            {
                return BadRequest(ModelState);
            }

            var article = _mapper.Map<Articles>(articleDto);

            if (!_articleRepository.UpdateArticle(article))
            {
                ModelState.AddModelError("",$"Algo salio mal al actualizar el articulo {articleDto.Price}");
            }

            return NoContent();
        }

        #endregion

        #region DeleteArticles
        [Authorize(Roles = "admin")]
        [HttpDelete("{categoriaId:int}", Name = "BorrarCategoria")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteArticles(int articleId)
        {
            if (!_articleRepository.ExistArticleId(articleId)) 
            {
                return NotFound();
            }

            var article = _articleRepository.GetArticles(articleId);

            if (!_articleRepository.DeleteArticle(article))
            {
                ModelState.AddModelError("",$"Algo salio mal al borrar el articulo seleccionado{article.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        #endregion
    }
}
