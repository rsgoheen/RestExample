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

        [HttpPost]
        [Route("shoppinglists/{listId:long}/drinks")]
        public IHttpActionResult AddDrink(long listId, [FromBody]Drink drink)
        {
            if (drink == null)
                return BadRequest("Could not parse Drink from request");

            var shoppingList = _repository.GetList(listId);

            if (shoppingList == null)
                return NotFound();
            
            // Note: for now, consider an add of an existing drink to be an update
            if(shoppingList.HasDrink(drink))
                shoppingList.UpdateDrink(drink);
            else
                shoppingList.AddDrink(drink);

            return Created<Drink>($"{Request.RequestUri}/drinks/{drink.Name}", drink);
        }
    }
}