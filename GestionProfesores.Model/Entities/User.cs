using System;
using System.Collections.Generic;

#nullable disable

namespace GestionProfesores.Model.Entities
{
    public partial class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
