using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Routing;
using DrinkStore.WebApi.Models;
using DrinkStore.WebApi.Repository;

namespace DrinkStore.WebApi.Controllers
{
    [RoutePrefix("api")]

    public class ShoppingListController : ApiController
    {
        private readonly IShoppingListRepository _repository;
        private const int MaxPageSize = 10;

        public ShoppingListController(IShoppingListRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("shoppinglists/{id:long}")]
        public IHttpActionResult GetList(long id)
        {
            var shoppingList = _repository.GetShoppingList(id);

            if (shoppingList == null)
                return NotFound();

            return Ok(shoppingList);
        }

        [HttpGet]
        [Route("shoppinglists", Name = "ShoppingLists")]
        public IHttpActionResult GetLists(int page = 1, int pageSize = MaxPageSize)
        {
            pageSize = (pageSize < MaxPageSize) ? pageSize : MaxPageSize;

            var shoppingLists = _repository.GetShoppingLists();
            var shoppingListCount = _repository.GetShoppingLists().Count();
            var totalPages = (int) Math.Ceiling((double)shoppingListCount / pageSize);

            page = (page < totalPages) ? page : totalPages;
            page = (page > 0) ? page : 1;

            var shoppingListPaged = shoppingLists
                .Skip(pageSize * (page - 1))
                .Take(pageSize);

            var urlHelper = new UrlHelper(Request);
            var prevPageLink = page > 1
               ? urlHelper.Link("ShoppingLists", new {page = page - 1, pageSize = pageSize})
               : "";
            var nextPageLink = page < totalPages 
                ? urlHelper.Link("ShoppingLists", new {page = page + 1, pageSize = pageSize}) 
                : "";

            var pagingHeader = new
            {
                currentPage = page,
                pageSize = pageSize,
                totalCount = shoppingListCount,
                totalPages = totalPages,
                previousPageLink = prevPageLink,
                nextPageLink = nextPageLink
            };

            HttpContext.Current.Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(pagingHeader));

            return Ok(shoppingListPaged);
        }

        [HttpPost]
        [Route("shoppinglists")]
        public IHttpActionResult CreateList(ShoppingList postedShoppingList)
        {
            if (postedShoppingList == null)
                return BadRequest("Could not parse ShoppingList from request");

            var shoppingList = _repository.CreateShoppingList(postedShoppingList);

            return Created($"{Request.RequestUri}/{shoppingList.Id}", shoppingList);
        }
    }
}