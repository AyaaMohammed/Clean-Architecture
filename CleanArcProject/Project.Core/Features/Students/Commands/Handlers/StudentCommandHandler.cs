using AutoMapper;
using MediatR;
using Project.Core.Bases;
using Project.Core.Features.Students.Commands.Models;
using Project.Data.Entities;
using Project.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Core.Bases;
using Project.Core.Rescources;
using Microsoft.Extensions.Localization;
namespace Project.Core.Features.Students.Commands.Handlers
{
    public class StudentCommandHandler:ResponseHandler,IRequestHandler<AddStudentCommand, Response<string>>   
                                                       ,IRequestHandler<EditStudentCommand,Response<string>> 
                                                        ,IRequestHandler<DeleteStudentCommand, Response<string>>
    {
        private readonly IStringLocalizer<SharedRescources> _stringLocalizer;
        IStudentService _studentService;
        IMapper _mapper;
        public StudentCommandHandler(IStudentService studentService, IMapper mapper, IStringLocalizer<SharedRescources> stringLocalizer):base(stringLocalizer)
        {
            _studentService = studentService;
            _mapper = mapper;
        }
        public async Task<Response<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            var studentmapper  = _mapper.Map<Student>(request);
            var result = await _studentService.AddAsync(studentmapper);
            if (result== "exist")
            {
                return UnprocessableEntity<string>("Student already exists");

            }
            return Success(result);
        }

        public async  Task<Response<string>> Handle(EditStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetStudentByIdAsync(request.Id);
            if (student == null)
            {
                return NotFound<string>("Student not found");
            }
            var studentmapper = _mapper.Map<Student>(request);
            var result = await _studentService.EditAsync(studentmapper);
            if(result=="Success")return Success<string>("Student updated successfully");
            return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetStudentByIdAsync(request.Id);
            if (student == null)
            {
                return NotFound<string>("Student not found");
            }
            var result = await _studentService.DeleteAsync(student);
            if (result)
            {
                return Deleted<string>();
            }
            else
            {
                return BadRequest<string>("Failed to delete student");
            }

        }
    }
}
