using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace JamesJonesDbs2.Models.DataTransferObject
{
    public class LoginAppUserDTO
    {
        
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set;}

        [DataType(DataType.Password)]
        public string Password { get; set;}
    }
}
