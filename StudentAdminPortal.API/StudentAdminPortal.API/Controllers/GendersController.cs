using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortal.API.DomainModels;
using StudentAdminPortal.API.Repository;

namespace StudentAdminPortal.API.Controllers
{
    [ApiController]
    public class GendersController : Controller
    {
        private readonly IStudentRepository studentRepository;
        private IMapper mapper;

        public GendersController(IStudentRepository studentRepository,IMapper mapper)
        {
            this.studentRepository = studentRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        [Route("[Controller]")]
        public async Task<IActionResult>GetAllGenders()
        {
            var genderList= await studentRepository.GetGendersAsync();
            if(genderList==null || !genderList.Any())
            {
                return NotFound();
            
            }
            return Ok(mapper.Map<List<Gender>>(genderList));
        }
    }
}
