using AngleSharp.Css.Dom;
using JamesJonesDbs2.Models;
using JamesJonesDbs2.Models.Database;
using JamesJonesDbs2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Security.Claims;

namespace JamesJonesApplication.Controllers
{
    /// <summary>
    /// Controller Dedicated for Shopping List
    /// </summary>
    public class ShoppingListController : Controller
    {
        //Dependency Injection 
        DatabaseContext _databaseContext;
        SaniteserService _saniteserService;

        //Constructor
        public ShoppingListController(DatabaseContext databaseContext, SaniteserService saniteserService)
        {
            _databaseContext = databaseContext;
            _saniteserService= saniteserService;
        }

        /// <summary>
        /// Checks for a user if they are logged in proceeds to return view
        /// </summary>
        /// <returns></returns>
        //For the Controller and return Index, Return a view of the shopping lists
        [Authorize(Roles = "Customer, Admin")]
        public IActionResult Index()
        {
            int Sid = Int32.Parse(HttpContext.User.Claims.Where( c => c.Type == ClaimTypes.Sid).Select( c => c.Value ).SingleOrDefault());
            if(Sid == 0 || Sid == null)
            {
                return RedirectToAction("Login", "AppUser");
            }
            ViewBag.AppUserId = HttpContext.Session.GetString("ID");
            return View();
        }

        /// <summary>
        /// Calls the method to view a dropdown list of the user
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Customer, Admin")]
        public async Task <IActionResult> ShoppingListDDL()
        {
            int Sid = Int32.Parse(HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault());
            if (Sid == 0 || Sid == null)
            {
                return Unauthorized();
            }

            
            var user = _databaseContext.AppUsers.Where(c => c.Id == Sid)
                .Include(c => c.ShoppingList).FirstOrDefault();

            if (user == null)
            {
                return BadRequest();
            }

            var selectedList = user.ShoppingList.Select(c => new SelectListItem
            {
                Text = c.Name + " Created: " + c.Created,
                Value = c.Id.ToString()
                
            }).ToList();

            if(selectedList.Count == 0)
            {
                selectedList.Add(new SelectListItem
                {
                    Text = "No Shopping Lists yet...",
                    Value = "0"
                });
            }

            ViewBag.SelectList = selectedList;

            return PartialView("_ShoppingListDDL");
        }

        /// <summary>
        /// Post method that intakes list name from user and creates a new list for the user
        /// </summary>
        /// <param name="listName"></param>
        /// <returns></returns>
        [Authorize(Roles = "Customer, Admin")]
        [HttpPost]
        public async Task<IActionResult> AddNewShoppingList([FromBody] string listName)
        {
            //Retrieve the user Id From the Claims
            int Sid = Int32.Parse(HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault());

            if (Sid == 0 || Sid == null)
            {
                return Unauthorized();
            }

            string cleanListName = _saniteserService.Sanitiser.Sanitize(listName);

            if (_databaseContext.ShoppingLists.Any(c => c.Name == cleanListName && c.AppUserID == Sid))
            {
                return BadRequest();
            }

            ShoppingList newList = new ShoppingList()
            {
                Name = cleanListName,
                AppUserID = Sid,
            };

            _databaseContext.ShoppingLists.Add(newList);
            _databaseContext.SaveChanges();

            return Ok();
        }


        /// <summary>
        /// Gets all items from the SamsWareHouseItem and Add Into Items of the shopping list to create a full view of the contents of the shopping list as 
        /// partial view 
        /// </summary>
        /// <param name="listId"></param>
        /// <returns></returns>
        [Authorize(Roles = "Customer, Admin")]
        public async Task<IActionResult> GetSamsWareHouseItemForList([FromQuery] int listId)
        {
            List<SamsWareHouseItem> shopItems = _databaseContext.ShoppingListItems.Include(c => c.SamsWareHouseItem)
                                                                                    .ThenInclude(c => c.ShoppingListItems)
                                                                                    .Where(c => c.ShoppingListId == listId)
                                                                                    .Select(c => c.SamsWareHouseItem)
                                                                                    .ToList();      
            return PartialView("_SamsItemForListPartial", shopItems);
        }

        /// <summary>
        /// Removes a WareHouse Item based on given id from iterating through the for each container model.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [Authorize(Roles = "Customer, Admin")]
        [HttpDelete]
        public async Task<IActionResult> RemoveItemFromList([FromBody] ShoppingListItem item
            )
        {
            var shoppingListItem = _databaseContext.ShoppingListItems.Where(
                c => c.ShoppingListId == item.ShoppingListId && 
                c.SamsWareHouseItemId == item.SamsWareHouseItemId)
                .FirstOrDefault();

            if (shoppingListItem != null)
            {
                _databaseContext.ShoppingListItems.Remove(shoppingListItem);
                _databaseContext.SaveChanges();
                return Ok();
            }

            return BadRequest();
        }

        /// <summary>
        /// Adds an item to the user list
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [Authorize(Roles = "Customer, Admin")]
        [HttpPost]
        public async Task<IActionResult> AddItemToShoppingList([FromBody] ShoppingListItem item)
        {
            Thread.Sleep(2000);

            if (_databaseContext.ShoppingListItems.Any(c => c.ShoppingListId == item.ShoppingListId && 
            c.SamsWareHouseItemId == item.SamsWareHouseItemId))
            {
                return BadRequest();
            }

            _databaseContext.ShoppingListItems.Add(item);
            _databaseContext.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// Deletes an item from a user's shopping list
        /// </summary>
        /// <param name="listID"></param>
        /// <returns></returns>
        [Authorize (Roles ="Admin, Customer")]
        [HttpDelete]
        public async Task<IActionResult>DeleteShoppingListItem([FromQuery]int listID)
        {
            try
            {
                var shoppingList = _databaseContext.ShoppingLists.FirstOrDefault(c => c.Id == listID);

                if (shoppingList != null) 
                { 
                    _databaseContext.ShoppingLists.Remove(shoppingList); 

                    await _databaseContext.SaveChangesAsync();

                    Thread.Sleep(2000);

                    return Ok();
                }
                return BadRequest();

            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}

