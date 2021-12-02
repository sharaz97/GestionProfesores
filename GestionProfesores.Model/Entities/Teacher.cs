using System;
using System.Collections.Generic;

#nullable disable

namespace GestionProfesores.Model.Entities
{
    public partial class Teacher
    {
        public int TeacherId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
    }
}
