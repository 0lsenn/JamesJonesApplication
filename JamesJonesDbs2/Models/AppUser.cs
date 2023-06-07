using MessagePack;
using System.ComponentModel.DataAnnotations;

namespace JamesJonesDbs2.Models
{
    public class AppUser
    {
        
        public int Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set;}

        [Required]
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; }

        [Required]
        public string Role { get; set; }
        
        //Nav Property
        public ICollection<ShoppingList> ShoppingList { get; set; }

    }


}
