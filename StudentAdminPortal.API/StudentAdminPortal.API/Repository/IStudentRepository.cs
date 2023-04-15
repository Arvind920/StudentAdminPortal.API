using StudentAdminPortal.API.DataModels;
using StudentAdminPortal.API.DomainModels;
using Gender = StudentAdminPortal.API.DataModels.Gender;
using Student = StudentAdminPortal.API.DataModels.Student;

namespace StudentAdminPortal.API.Repository
{
    public interface IStudentRepository
    {
       Task<List<Student> >GetStudentsAsync();
        Task<Student> GetStudentAsync(Guid studentId);


        Task<List<Gender>>GetGendersAsync();
        Task<bool>Exists(Guid atudentId);
       Task<Student> UpdateStudent(Guid studentId, Student request);
        Task<Student> DeleteStudent(Guid studentId);

    }
}
