using System.Web.Http;
using DrinkStore.WebApi.Models;
using DrinkStore.WebApi.Repository;

namespace DrinkStore.WebApi.Controllers
{
    [RoutePrefix("api")]

    public class ShoppingListController : ApiController
    {
        private readonly IShoppingListRepository _repository;

        public ShoppingListController(IShoppingListRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]

        [Route("shoppinglists/{id:long}")]
        public IHttpActionResult GetList(long id)
        {
            var shoppingList = _repository.GetList(id);

            if (shoppingList == null)
                return NotFound();

            return Ok(shoppingList);
        }

        [HttpPost]
        [Route("shoppinglists")]
        public IHttpActionResult CreateList(ShoppingList postedShoppingList)
        {
            if (postedShoppingList == null)
                return BadRequest("Could not parse ShoppingList from request");

            var shoppingList = _repository.Create(postedShoppingList);

            return Created<ShoppingList>($"{Request.RequestUri}/{shoppingList.Id}", shoppingList);
        }
    }
}