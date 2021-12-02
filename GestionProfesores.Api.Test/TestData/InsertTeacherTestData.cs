using GestionProfesores.Model.Entities;
using System;
using System.Collections.Generic;

namespace GestionProfesores.Api.Test.TestData
{
    internal class InsertTeacherTestData : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[]
            {
                new Teacher {TeacherId = 9, Name = "Sharaz", Surname = "Muhammad", Age = 20 }
            },
            new object[]
            {
                new Teacher {TeacherId = 10, Name = "Luis", Surname = "Suarez", Age = 45 }
            },
             new object[]
            {
                new Teacher {TeacherId = 11, Name = "Messi", Surname = "Lionel", Age = 56 }
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
