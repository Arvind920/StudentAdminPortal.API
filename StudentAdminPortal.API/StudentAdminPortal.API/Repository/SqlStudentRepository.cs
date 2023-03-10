using StudentAdminPortal.API.DataModels;

namespace StudentAdminPortal.API.Repository
{
    public class SqlStudentRepository : IStudentRepository
    {
        private readonly StudentAdminContext context;
        public SqlStudentRepository(StudentAdminContext context)
        {
            this.context = context;
        }

        public List<Student> GetStudents()
        {
            return context.Student.ToList();
        }
    }
}
