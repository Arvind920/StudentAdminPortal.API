using StudentAdminPortal.API.DataModels;

namespace StudentAdminPortal.API.Repository
{
    public interface IStudentRepository
    {
        List<Student> GetStudents();
    }
}
