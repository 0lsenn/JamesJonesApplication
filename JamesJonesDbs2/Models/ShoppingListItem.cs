namespace JamesJonesDbs2.Models
{
    public class ShoppingListItem
    {
        public int Id { get; set; } 

        //FK
        public int SamsWareHouseItemId { get; set; }
        public int ShoppingListId {get; set; }
        public int Quantity { get; set; }
        
        //Nav Property
        public ShoppingList ShoppingList { get; set; }
        public SamsWareHouseItem  SamsWareHouseItem { get; set; } 
    }
}
