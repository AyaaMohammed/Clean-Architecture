using MediatR;
using Project.Core.Bases;
using Project.Core.Features.Students.Queries.Results;
using Project.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Features.Students.Queries.Models
{
    // this is response List of students
    public class GetStudentListQuery: IRequest<Response<List<GetStudentListResponse>>>
    {
    }
}
