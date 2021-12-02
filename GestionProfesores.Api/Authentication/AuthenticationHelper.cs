using GestionProfesores.Model.Entities;
using System;
using System.Linq;

namespace GestionProfesores.Api.Authentication
{
    public class AuthenticationHelper
    {
        public static User Login(GestionProfesoresContext dbContext, string userName, string userpassword) => dbContext.Users.ToList().FirstOrDefault(user => user.Name.Equals(userName, StringComparison.OrdinalIgnoreCase) && user.Password == userpassword);
    }
}
