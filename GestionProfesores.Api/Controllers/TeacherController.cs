using GestionProfesores.Model.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GestionProfesores.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : Controller
    {
        readonly GestionProfesoresContext _dbContext;

        public TeacherController(GestionProfesoresContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<Teacher> Get() => _dbContext?.Teachers;

        [HttpGet("{id}")]
        public Teacher Get(int id) => GetTeacherAndCreateNotFoundResponseIfNotExists(id);

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var profesor = GetTeacherAndCreateNotFoundResponseIfNotExists(id);
            _dbContext.Remove(profesor);
            _dbContext.SaveChanges();
        }

        [HttpPost]
        public void Insert([FromBody] Teacher profesor)
        {
            _dbContext.Teachers.Add(profesor);
            _dbContext.SaveChanges();
        }

        [HttpPut("{id}")]
        public void Update(int id, [FromBody] Teacher teacherNewValues)
        {
            var tacherOldValues = GetTeacherAndCreateNotFoundResponseIfNotExists(id);
            _dbContext.Entry(tacherOldValues).CurrentValues.SetValues(teacherNewValues);
            _dbContext.SaveChanges();
        }

        Teacher GetTeacherAndCreateNotFoundResponseIfNotExists(int id)
        {
            var profesor = _dbContext?.Teachers.Find(id);
            if (profesor == null)
            {
                NotFound(id);
            }
            return profesor;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
