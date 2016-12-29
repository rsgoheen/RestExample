using System.Net;
using System.Web.Http;
using DrinkStore.WebApi.Models;
using DrinkStore.WebApi.Repository;

namespace DrinkStore.WebApi.Controllers
{
    [RoutePrefix("api")]
    public class DrinkController : ApiController
    {
        private readonly IShoppingListRepository _repository;

        public DrinkController(IShoppingListRepository repository)
        {
            _repository = repository;
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
            if (shoppingList.HasDrink(drink))
                shoppingList.UpdateDrink(drink);
            else
                shoppingList.AddDrink(drink);

            return Created<Drink>($"{Request.RequestUri}/drinks/{drink.Name}", drink);
        }

        [HttpDelete]
        [Route("shoppinglists/{listId:long}/drinks/{drinkName}")]
        public IHttpActionResult DeleteDrink(long listId, string drinkName)
        {
            var shoppingList = _repository.GetList(listId);

            if (shoppingList == null)
                return NotFound();

            if (!shoppingList.RemoveDrink(drinkName))
                return NotFound();

            _repository.Update(shoppingList);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}