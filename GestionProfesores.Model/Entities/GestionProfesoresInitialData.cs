namespace GestionProfesores.Model.Entities
{
    public static class GestionProfesoresInitialData
    {
        public static void Seed(this GestionProfesoresContext dbContext)
        {
            SeedTeachers(dbContext);
            SeedUsers(dbContext);
            dbContext.SaveChanges();
        }

        static void SeedUsers(GestionProfesoresContext dbContext)
        {
            dbContext.Users.Add(new User { Name = "Admin", Password = "password", Email = "admin@example.com" });
        }

        static void SeedTeachers(GestionProfesoresContext dbContext)
        {
            dbContext.Teachers.Add(new Teacher { Name = "Sharaz", Surname = "Muhammad", Age = 24 });
            dbContext.Teachers.Add(new Teacher { Name = "Luis", Surname = "Suarez", Age = 24 });
            dbContext.Teachers.Add(new Teacher { Name = "Messi", Surname = "Lionel", Age = 24 });
            dbContext.Teachers.Add(new Teacher { Name = "Busquets", Surname = "Barcelona", Age = 24 });
            dbContext.Teachers.Add(new Teacher { Name = "Suarez", Surname = "Gracia", Age = 24 });
            dbContext.Teachers.Add(new Teacher { Name = "Puyol", Surname = "Apellido", Age = 24 });
            dbContext.Teachers.Add(new Teacher { Name = "Jordi", Surname = "Alba", Age = 24 });
            dbContext.Teachers.Add(new Teacher { Name = "Thiago", Surname = "Alcantara", Age = 24 });
        }
    }
}