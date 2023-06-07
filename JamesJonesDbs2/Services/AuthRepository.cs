using JamesJonesDbs2.Models;
using JamesJonesDbs2.Models.Database;
using JamesJonesDbs2.Models.DataTransferObject;

namespace JamesJonesApplication.Services
{
    public enum Roles
    {
        Admin,
        Customer,
        Guest
    }

    // TODO 7 - Create a service that will have access to the database, to retrieve and authenitcate a user
    public class AuthRepository
    {
        private readonly DatabaseContext _context;
        public AuthRepository(DatabaseContext context)
        {
            _context = context;
        }



        public AppUser Authenticate(LoginAppUserDTO credentials)
        {
            var userDetails = GetUserByEmail(credentials.EmailAddress);

            if (userDetails == null)
            {
                return null;
            }

            if (BCrypt.Net.BCrypt.EnhancedVerify(credentials.Password, userDetails.PasswordHash))
            {
                return userDetails;
            }

            return null;

        }


        // TODO - Remove the salting - as the bcrypt library generates a salt automatically
        public AppUser CreateUser(LoginAppUserDTO loginDetails, string role)
        {

            // if the string the user provides is not in the 'Roles' enum, then use the Guest role
            // this should never happen when using a dropdown list, but could be circumvented using JS
            if (!System.Enum.IsDefined(typeof(Roles), role))
            {
                role = Roles.Guest.ToString();
            }

            var userDetails = GetUserByEmail(loginDetails.EmailAddress);

            // Username Exists
            if (userDetails != null)
            {
                return null;
            }

            AppUser user = new AppUser()
            {
                EmailAddress = loginDetails.EmailAddress,
                PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(loginDetails.Password),
            };

            _context.AppUsers.Add(user);
            _context.SaveChanges();
            return user;
        }

        private AppUser GetUserByEmail(string userEmail)
        {
            var user = _context.AppUsers.Where(c => c.EmailAddress.Equals(userEmail)).FirstOrDefault();

            return user;
        }


    }
}