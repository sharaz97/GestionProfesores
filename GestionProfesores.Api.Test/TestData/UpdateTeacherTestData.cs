using GestionProfesores.Model.Entities;
using System;
using System.Collections.Generic;

namespace GestionProfesores.Api.Test.TestData
{
    internal class UpdateTeacherTestData : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[]
            {
                1, new Teacher {TeacherId = 1, Name = "Sharaz", Surname = "Muhammad", Age = 20 }
            },
            new object[]
            {
                2, new Teacher {TeacherId = 2, Name = "Luis", Surname = "Suarez", Age = 45 }
            },
             new object[]
            {
                3, new Teacher {TeacherId = 3, Name = "Messi", Surname = "Lionel", Age = 56 }
            }
        };

        public IEnumerator<object[]> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
