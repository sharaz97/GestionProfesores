using GestionProfesores.Api.Client;
using GestionProfesores.Win.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace GestionProfesores.Win.Controllers
{
    public class MVCTeacherController : Controller
    {
        GestionProfesoresApiClient apiClient = new GestionProfesoresApiClient();
        static readonly string USER_NAME = "Admin";
        static readonly string USER_PASSWORD = "password";

        public async Task<IActionResult> Index(string searchName)
        {     
            var teachers = await apiClient.GetTeachers<MVCTeacher>(USER_NAME, USER_PASSWORD);
            if(!string.IsNullOrEmpty(searchName))
            {
                teachers = teachers.Where(teacher => teacher.Name.Contains(searchName, System.StringComparison.OrdinalIgnoreCase));
            }
            return View(teachers);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MVCTeacher teacher)
        {
            if(!ModelState.IsValid)
            {
                return View(teacher);
            }
            await apiClient.CreateTeacher(USER_NAME, USER_PASSWORD, teacher);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? teacherId)
        {
            if(!teacherId.HasValue)
            {
                NotFound();
            }
            var techer = await apiClient.GetTeacher<MVCTeacher>(USER_NAME, USER_PASSWORD, teacherId.Value);
            return View(techer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MVCTeacher teacher)
        {
            if (!ModelState.IsValid)
            {
                return View(teacher);
            }
            await apiClient.UpdateTeacher(USER_NAME, USER_PASSWORD, teacher.TeacherId, teacher);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? teacherId)
        {
            if (!teacherId.HasValue)
            {
                NotFound();
            }
            var techer = await apiClient.GetTeacher<MVCTeacher>(USER_NAME, USER_PASSWORD, teacherId.Value);
            return View(techer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePOST(int? teacherId)
        {
            await apiClient.DeleteTeacher(USER_NAME, USER_PASSWORD, teacherId.Value);
            return RedirectToAction("Index");
        }
    }
}