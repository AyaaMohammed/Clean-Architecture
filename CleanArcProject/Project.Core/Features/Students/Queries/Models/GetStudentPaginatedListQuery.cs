using MediatR;
using Project.Core.Features.Students.Queries.Results;
using Project.Core.Wrappers;
using Project.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Features.Students.Queries.Models
{
    public class GetStudentPaginatedListQuery:IRequest<PaginatedResult<GetStudentPaginatedListResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; } 
        public string? SearchTerm { get; set; }
        public StudentOrderingEnum OrderBy { get; set; }

    }
}
