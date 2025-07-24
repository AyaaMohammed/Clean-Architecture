using MediatR;
using Project.Core.Bases;
using Project.Core.Features.Students.Queries.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Features.Students.Queries.Models
{
    public class GetStudentByIDQuery:IRequest<Response<GetSingleStudentResponse>>
    {
        public int Id { get; set; }

    }
}
