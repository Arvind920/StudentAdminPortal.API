using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
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
        private readonly IImageRepository imageRepository;

        public StudentsController(IStudentRepository StudentRepository, IMapper mapper, IImageRepository imageRepository)
        {
            this.StudentRepository = StudentRepository;
            this.mapper = mapper;
            this.imageRepository = imageRepository;
        }

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllStudentAsync()
        {
           var students= await StudentRepository.GetStudentsAsync();

            return  Ok(mapper.Map<List<Student>>(students));
            
        }

        [HttpGet]
        [Route("[controller]/{studentId:guid}"), ActionName("GetStudentAsync")]
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
        [HttpPost]
        [Route("[controller]/Add")]
        public async Task<IActionResult> AddStudentAsync([FromBody] AddStudentRequest request)
        {
          var student = await StudentRepository.AddStudent(mapper.Map<DataModels.Student>(request));
            return CreatedAtAction(nameof(GetStudentAsync), new { studentId = student.Id },
                mapper.Map<Student>(student));
        }
        [HttpPost]
        [Route("[controller]/{studentId:guid}/upload-image")]
        public async Task<IActionResult> UploadImage([FromRoute] Guid studentId,IFormFile profileImage)
        {
            //Check if student exist
            if (await StudentRepository.Exists(studentId))
            {
                var fileName= Guid.NewGuid() + Path.GetExtension(profileImage.FileName);
                //upload image after to local storage
               var fileImagePath= await imageRepository.Upload(profileImage,fileName);

               if(await StudentRepository.UpdateProfileImage(studentId, fileImagePath))
                {
                    return Ok(fileImagePath);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, "Error uploading Image");

                //update the profile image path in the database
            }
            return NotFound(); 
        }
    
    }
}
