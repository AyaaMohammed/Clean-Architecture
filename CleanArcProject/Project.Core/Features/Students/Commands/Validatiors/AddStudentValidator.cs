using FluentValidation;
using Microsoft.Extensions.Localization;
using Project.Core.Features.Students.Commands.Models;
using Project.Core.Rescources;
using Project.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Features.Students.Commands.Validatiors
{
    public class AddStudentValidator:AbstractValidator<AddStudentCommand>
    {
        private readonly IStudentService _studentservice;
        IStringLocalizer<SharedRescources> _stringLocalizer;
        public AddStudentValidator(IStudentService studentService, IStringLocalizer<SharedRescources> _stringLocalizer)
        {
            _stringLocalizer = _stringLocalizer;
            _studentservice = studentService;
            ApplyValidationsRule();
            ApplyCustomValidationsRules();
        }
        public void ApplyValidationsRule()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(_stringLocalizer[SharedRescourcesKeys.Required])
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");
            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Date of Birth is required.")
                .LessThan(DateTime.Now).WithMessage("Date of Birth must be in the past.");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone Number is required.")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Invalid phone number format.");
            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(250).WithMessage("Address cannot exceed 250 characters.");
            RuleFor(x => x.DepartmentId)
                .GreaterThan(0).When(x => x.DepartmentId.HasValue).WithMessage("Department ID must be greater than 0.");
        }
        public void ApplyCustomValidationsRules()
        {
            RuleFor(x => x.Name)
                .MustAsync(async (key, CancellationToken) => !await _studentservice.IsNameExist(key))
                .WithMessage("A student with this name already exists.");



        }
    }
}
