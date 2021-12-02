using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GestionProfesores.Win.Models
{
    public class MVCTeacher
    {
        [HiddenInput(DisplayValue = false)]
        public int TeacherId { get; set; }
        [Required(ErrorMessage = "The field is requiered")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The field is requiered")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "The field is requiered")]
        [Range(18,80, ErrorMessage = "The age must be between 18 and 80")]
        public int Age { get; set; }
    }
}
