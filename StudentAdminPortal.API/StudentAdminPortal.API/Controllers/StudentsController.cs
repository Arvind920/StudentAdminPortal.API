using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortal.API.DataModels;
using StudentAdminPortal.API.DomainModels;
using StudentAdminPortal.API.Repository;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using Student = StudentAdminPortal.API.DomainModels.Student;

namespace StudentAdminPortal.API.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentRepository StudentRepository;
        private readonly IMapper mapper;

        public StudentsController(IStudentRepository StudentRepository, IMapper mapper)
        {
            this.StudentRepository = StudentRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllStudentAsync()
        {
           var students= await StudentRepository.GetStudentsAsync();

            return  Ok(mapper.Map<List<Student>>(students));
            
        }

        [HttpGet]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> GetStudentAsync([FromRoute] Guid studentId)
        {
            //Fetch Student Detials
            var student = await StudentRepository.GetStudentAsync(studentId);
            if(student == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<Student>(student));

            //Return Student
        }
        [HttpPut]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> UpdateStudentAsync([FromRoute]Guid studentId, [FromBody]UpdateStudentRequest request)
        {
            if(await StudentRepository.Exists(studentId))
            {
                var updatedStudent = await StudentRepository.UpdateStudent(studentId, mapper.Map<DataModels.Student>(request));

                if(updatedStudent != null)
                {
                    return Ok(mapper.Map<Student>(updatedStudent));
                }
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> DeleteStudentAsync([FromRoute] Guid studentId)
        {
            if(await StudentRepository.Exists(studentId))
            {
                var student= await StudentRepository.DeleteStudent(studentId);
                return Ok(mapper.Map<Student>(student));
            }
            return NotFound();
        }
        
    
    }
}
