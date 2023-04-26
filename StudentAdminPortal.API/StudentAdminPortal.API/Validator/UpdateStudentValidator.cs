using FluentValidation;
using StudentAdminPortal.API.DomainModels;
using StudentAdminPortal.API.Repository;

namespace StudentAdminPortal.API.Validator
{
    public class UpdateStudentValidator:AbstractValidator<UpdateStudentRequest>
    {
        public UpdateStudentValidator(IStudentRepository studentRepository)
        {

            RuleFor(x => x.FirstName).NotEmpty();                           
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.DateOfBirth).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Mobile).NotEmpty().GreaterThan(9999).LessThan(99999999999);
            RuleFor(x => x.GenderId).Must(id =>
            {
                var gender = studentRepository.GetGendersAsync().Result.ToList().FirstOrDefault(x => x.Id == id);
                if (gender != null)
                {
                    return true;
                }
                return false;
            }).WithMessage("Please Select a Valid Gender");
            RuleFor(x => x.PhysicalAddress).NotEmpty();
            RuleFor(x => x.PostalAddress).NotEmpty();




        }
    }
}
