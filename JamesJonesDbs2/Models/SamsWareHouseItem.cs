using System.ComponentModel.DataAnnotations;

namespace JamesJonesDbs2.Models
{
    public class SamsWareHouseItem
    {
        [Key]
        public int SamsWareHouseItemId { get; set; }
        public string ItemName { get; set; }
        public string Unit { get; set; }
        public double UnitPrice { get; set; }

        //Nav Property
        public ICollection<ShoppingListItem> ShoppingListItems { get; set; }
    }
}
