using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using Project.Core.Bases;
using Project.Core.Features.Students.Queries.Models;
using Project.Core.Features.Students.Queries.Results;
using Project.Core.Rescources;
using Project.Core.Wrappers;
using Project.Data.Entities;
using Project.Service.Abstracts;
using Project.Service.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Features.Students.Queries.Handlers
{
    public class StudentQueryHandler :ResponseHandler, IRequestHandler<GetStudentListQuery, Response<List<GetStudentListResponse>>>,
                                                        IRequestHandler<GetStudentByIDQuery, Response<GetSingleStudentResponse>>,
                                                        IRequestHandler<GetStudentPaginatedListQuery, PaginatedResult<GetStudentPaginatedListResponse>>
    {
        IStudentService _studentService;
        IMapper _mapper;
        IStringLocalizer<SharedRescources> _stringLocalizer;
        public StudentQueryHandler(IStudentService studentService, IMapper mapper, IStringLocalizer<SharedRescources> stringLocalizer)
            : base(stringLocalizer) 
        {
            _studentService = studentService;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }
        public async Task<Response<List<GetStudentListResponse>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
            var StudentList = await _studentService.GetStudentsListAsync();
            var StudentListMapper=_mapper.Map<List<GetStudentListResponse>>(StudentList);   
            return Success(StudentListMapper);
        }

        public async Task<Response<GetSingleStudentResponse>> Handle(GetStudentByIDQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetStudentByIdAsync(request.Id);
            if (student == null)
            {
                return NotFound<GetSingleStudentResponse>(_stringLocalizer[SharedRescourcesKeys.NotFound]);
            }
            var studentMapper = _mapper.Map<GetSingleStudentResponse>(student);
            return Success(studentMapper);
        }

        public async Task<PaginatedResult<GetStudentPaginatedListResponse>> Handle(GetStudentPaginatedListQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Student, GetStudentPaginatedListResponse>> expression = s => new GetStudentPaginatedListResponse
            {
                Id = s.Id,
                Name = s.NameAr,
                Email = s.Email,
                PhoneNumber = s.PhoneNumber,
                Address = s.Address,
                DateOfBirth = s.DateOfBirth,
                DepartmentName = s.Department != null ? s.Department.NameAr : "No Department Assigned"
            };
            var querable = _studentService.GetStudentQueryable();
            var FilterQuery = _studentService.FilterStudentPaginatedQuerable(request.OrderBy,request.SearchTerm);
            var paginatedList = await querable.Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }

    }
}
