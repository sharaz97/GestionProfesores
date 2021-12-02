using GestionProfesores.Api.Controllers;
using GestionProfesores.Api.Test.TestData;
using GestionProfesores.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using Xunit;

namespace GestionProfesores.Api.Test
{
    public class TeacherControllerTest
    {
        TeacherController _teacherController;
        public TeacherControllerTest()
        {
            var dbContext = GetTestDataDbContext();
            dbContext.Seed();
            _teacherController = new TeacherController(dbContext);
        }

        GestionProfesoresContext GetTestDataDbContext() => new GestionProfesoresContext(GetInMemoryDbContextOptiond());

        private static DbContextOptions<GestionProfesoresContext> GetInMemoryDbContextOptiond() => new DbContextOptionsBuilder<GestionProfesoresContext>()
                            .UseInMemoryDatabase(Guid.NewGuid().ToString())
                            .EnableSensitiveDataLogging()
                            .Options;

        [Fact]
        public void GetTeachers()
        {
            var teachers = _teacherController.Get();
            Assert.NotEmpty(teachers);
        }

        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        [InlineData(4, 4)]
        [InlineData(5, 5)]
        [InlineData(6, 6)]
        [InlineData(7, 7)]
        [InlineData(8, 8)]
        [InlineData(100, null)]
        [InlineData(-10, null)]
        [InlineData(0, null)]
        [Theory]
        public void GetTeacherTest(int id, int? expectedId)
        {
            var teacher = _teacherController.Get(id);
            Assert.Equal(teacher?.TeacherId, expectedId);
        }

        [Theory]
        [ClassData(typeof(UpdateTeacherTestData))]
        public void UpdateTeacherTest(int id, Teacher newValues)
        {
            _teacherController.Update(id, newValues);
            var actualUpdatedTeacher = _teacherController.Get(id);
            Assert.Equal(JsonConvert.SerializeObject(newValues), JsonConvert.SerializeObject(actualUpdatedTeacher));
        }

        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [Theory]
        public void DeleteTeacherTest(int id)
        {
            _teacherController.Delete(id);
            var teacher = _teacherController.Get(id);
            Assert.Null(teacher);
        }

        [Theory]
        [ClassData(typeof(InsertTeacherTestData))]
        public void InsertTeacherTest(Teacher teacher)
        {
            _teacherController.Insert(teacher);
            var addedTeacher = _teacherController.Get(teacher.TeacherId);
            Assert.NotNull(addedTeacher);
        }

        [Fact]
        public void InsertTeacherThrowsArgumentNullExceptionTest()
        {
            Assert.Throws<ArgumentNullException>(() => _teacherController.Insert(null));
        }

        [Fact]
        public void UpdateTeacherThrowsArgumentNullExceptionTest()
        {
            Assert.Throws<ArgumentNullException>(() => _teacherController.Update(1, null));
        }
    }
}