using JamesJonesDbs2.Models;
using JamesJonesDbs2.Models.Database;
using JamesJonesDbs2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JamesJonesDbs2.Controllers
{
    /// <summary>
    /// Controller dedicated to handling WareHouse Items
    /// </summary>
    public class SamsWareHouseItemController : Controller
    {
        /// <summary>
        /// Dependency Injection
        /// </summary>
        private readonly ILogger<SamsWareHouseItemController> _logger;
        private readonly DatabaseContext _samsWareHouseItemContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly SaniteserService _saniteserService;

        /// <summary>
        /// Constructors
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="samsWareHouseItem"></param>
        /// <param name="webHostEnvironment"></param>
        public SamsWareHouseItemController(ILogger<SamsWareHouseItemController> logger, DatabaseContext samsWareHouseItem, IWebHostEnvironment webHostEnvironment, SaniteserService saniteserService)
        {
            _logger = logger;
            _samsWareHouseItemContext = samsWareHouseItem;
            _webHostEnvironment = webHostEnvironment;
            _saniteserService = saniteserService;
        }


        /// <summary>
        /// Index view or main view of the WareHouseItems
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            if(_webHostEnvironment.IsDevelopment())
            {
                if (HttpContext.Session.Get("ID") == null)
                {
                    HttpContext.Session.SetString("ID", "0");
                }
            }

            //Get all shop items available from the database
            var samsWareHouseItem = _samsWareHouseItemContext.SamsWareHouseItems.ToList();
            //Retrun view with all items
            return View(samsWareHouseItem);
        }

        /// <summary>
        /// Returns a view for the user to input Creations of new WareHouseItem
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult> CreateNewShopItem()
        {

            //Return View for new Product
            return PartialView("_CreateNewShopItem");

        }

        /// <summary>
        /// Handles and Creates the new item into the database based on object provided 
        /// </summary>
        /// <param name="samsWareHouseItem"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateNewShopItem([FromBody] SamsWareHouseItem samsWareHouseItem)
        {
            Thread.Sleep(2000);
            try
            {
                string cleanItemName = _saniteserService.Sanitiser.Sanitize(samsWareHouseItem.ItemName);
                string cleanItemUnit = _saniteserService.Sanitiser.Sanitize(samsWareHouseItem.Unit);

                if (ModelState.ErrorCount < 2)
                {
                    SamsWareHouseItem item = new SamsWareHouseItem
                    {
                        ItemName = cleanItemName,
                        Unit = cleanItemUnit,
                        UnitPrice = samsWareHouseItem.UnitPrice
                    };
                    _samsWareHouseItemContext.SamsWareHouseItems.Add(item);
                }
                _samsWareHouseItemContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return BadRequest("Error Handling Details, Please Contact Support.");
            }
        }

        /// <summary>
        /// Get the Warehouse item from database based on id provided and return view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        // GET: ShopItemController/Edit/5
        public async Task<ActionResult> EditShopItem(int id)
        {
            
            if(id == 0)
            {
                return RedirectToAction(nameof(Index));
            }

            //Check match with database
            var samsWareHouseItem = await _samsWareHouseItemContext.SamsWareHouseItems.FirstOrDefaultAsync(c => c.SamsWareHouseItemId == id);
            if (samsWareHouseItem == null)
            {
                return NotFound();
            }

            //Tertiary Statement
            return samsWareHouseItem != null ? PartialView("_EditWareHouseItem", samsWareHouseItem) : RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Handles the updating of the specified item based on id and change on provided object
        /// </summary>
        /// <param name="id"></param>
        /// <param name="samsWareHouseItem"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditShopItem([FromQuery]int id,[FromBody] SamsWareHouseItem samsWareHouseItem)
        {
            Thread.Sleep(1000);
            try
            {
                //Sanitise from JSON object
                string cleanItemName = _saniteserService.Sanitiser.Sanitize(samsWareHouseItem.ItemName);
                string cleanItemUnit = _saniteserService.Sanitiser.Sanitize(samsWareHouseItem.Unit);

                //Reassign the sanitised items
                samsWareHouseItem.ItemName= cleanItemName;
                samsWareHouseItem.Unit= cleanItemUnit;

                if (ModelState.ErrorCount < 2)
                {
                    //Update based on given object
                    _samsWareHouseItemContext.SamsWareHouseItems.Update(samsWareHouseItem);
                    //Save
                    await _samsWareHouseItemContext.SaveChangesAsync();
                    Thread.Sleep(3000);
                    //return view
                    return View("SamsWareHouseItem", "Index");
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// Deletes a shop item based on provided id
        /// </summary>
        /// <param name="samsWareHouseItemId"></param>
        /// <returns></returns>
        [Authorize(Roles = "Customer")]
        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveShopItem(int id)
        {
            Thread.Sleep(2000);
            try
            {
                if(_samsWareHouseItemContext == null)
                {
                    return Problem("The Entity set 'SamsWareHouseItemContext.SamsWareHouseItem' is null.");
                }

                var samsWareHouseItem = await _samsWareHouseItemContext.SamsWareHouseItems.FindAsync(id);
                if(samsWareHouseItem != null)
                {
                    _samsWareHouseItemContext.SamsWareHouseItems.Remove(samsWareHouseItem);
                }
                await _samsWareHouseItemContext.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return Problem("Problem in Removing the Shop Item.");
            }
        }

       
    }
}
