using Microsoft.AspNetCore.Mvc;
using StudentAdminPortal.API.Repository;

namespace StudentAdminPortal.API.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentRepository StudentRepository;

        public StudentsController(IStudentRepository StudentRepository)
        {
            this.StudentRepository = StudentRepository;
        }

        [HttpGet]
        [Route("[Controller]")]
        public IActionResult GetAllStudent()
        {
           return Ok(StudentRepository.GetStudents());
        }
    }
}
