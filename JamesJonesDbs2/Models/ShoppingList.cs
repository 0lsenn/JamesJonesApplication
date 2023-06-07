namespace JamesJonesDbs2.Models
{
    public class ShoppingList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        
        //FK
        public int AppUserID { get; set; }
        
        //Nav Property
        public AppUser AppUser { get; set; }
        public ICollection<ShoppingListItem> ShoppingListItems { get; set; }
    }
}
