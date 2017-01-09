using System.Linq;
using System.Net;
using System.Web.Http;
using DrinkStore.WebApi.Models;
using DrinkStore.WebApi.Repository;

namespace DrinkStore.WebApi.Controllers
{
    [RoutePrefix("api")]
    public class DrinkController : ApiController
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IDrinkRepository _drinkRepository;

        public DrinkController(IShoppingListRepository shoppingListRepository, IDrinkRepository drinkRepository)
        {
            _shoppingListRepository = shoppingListRepository;
            _drinkRepository = drinkRepository;
        }

        [HttpPost]
        [Route("shoppinglists/{listId:long}/drinks")]
        public IHttpActionResult AddDrink(long listId, [FromBody]Drink drink)
        {
            if (drink == null)
                return BadRequest("Could not parse Drink from request");

            var shoppingList = _shoppingListRepository.GetShoppingList(listId);
            if (shoppingList == null)
                return NotFound();

            // Note: for now, consider an add of an existing drink to be an update
            if (shoppingList.HasDrink(drink))
            {
                shoppingList.UpdateDrink(drink);
            }
            else
            {
                drink = _drinkRepository.CreateDrink(drink);
                shoppingList.AddDrink(drink);
            }

            return Created<Drink>($"{Request.RequestUri}/drinks/{drink.Name}", drink);
        }

        [HttpPut]
        [Route("shoppinglists/{listId:long}/drinks")]
        public IHttpActionResult UpdateDrink(long listId, [FromBody]Drink drink)
        {
            if (drink == null)
                return BadRequest("Could not parse Drink from request");

            var shoppingList = _shoppingListRepository.GetShoppingList(listId);
            if (shoppingList == null)
                return NotFound();

            if (! shoppingList.HasDrink(drink))
                return NotFound();

            shoppingList.UpdateDrink(drink);

            return Ok(drink);
        }

        [HttpGet]
        [Route("shoppinglists/{listId:long}/drinks")]
        public IHttpActionResult GetAllDrinks(long listId)
        {
            var shoppingList = _shoppingListRepository.GetShoppingList(listId);

            if (shoppingList == null)
                return NotFound();

            return Ok(shoppingList.Drinks);
        }

        [HttpGet]
        [Route("shoppinglists/{listId:long}/drinks/{drinkId:int}")]
        public IHttpActionResult GetDrink(long listId, int drinkId)
        {
            var shoppingList = _shoppingListRepository.GetShoppingList(listId);

            if (shoppingList == null)
                return NotFound();

            var drink = shoppingList
                .Drinks
                .FirstOrDefault(x => x.Id == drinkId);

            if (drink == null)
                return NotFound();

            return Ok(drink);
        }

        [HttpDelete]
        [Route("shoppinglists/{listId:long}/drinks/{drinkId}")]
        public IHttpActionResult DeleteDrink(long listId, int drinkId)
        {
            var shoppingList = _shoppingListRepository.GetShoppingList(listId);

            if (shoppingList == null)
                return NotFound();

            if (!shoppingList.RemoveDrink(drinkId))
                return NotFound();

            _shoppingListRepository.UpdateShoppingList(shoppingList);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}