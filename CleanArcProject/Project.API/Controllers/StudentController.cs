using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Project.API.Base;
using Project.Core.Features.Students.Commands.Models;
using Project.Core.Features.Students.Queries.Models;
using Project.Data.AppMetaData;

namespace Project.API.Controllers
{

    [ApiController]
    public class StudentController : AppControllerBase
    {
        [HttpGet(Router.StudentRouting.GetAll)]
        public async Task<IActionResult> GetAllStudents()
        {
            var response = await Mediator.Send(new GetStudentListQuery());
            return NewResult(response);
        }
        [HttpGet(Router.StudentRouting.GetById)]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var response = await Mediator.Send(new GetStudentByIDQuery { Id = id });
            return NewResult(response);
        }
        [HttpPost(Router.StudentRouting.Add)]
        public async Task<IActionResult> AddStudent([FromBody] AddStudentCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpPut(Router.StudentRouting.Edit)]
        public async Task<IActionResult> EditStudent([FromBody] EditStudentCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpDelete(Router.StudentRouting.Delete)]
        public async Task<IActionResult> DeleteStudent([FromRoute] int id)
        {
            var response = await Mediator.Send(new DeleteStudentCommand(id));
            return NewResult(response);
        }
        [HttpGet(Router.StudentRouting.Pagnated)]
        public async Task<IActionResult> GetPaginatedStudents([FromQuery] GetStudentPaginatedListQuery query)
        {
            var response = await Mediator.Send(query);
            return Ok(response);
        }
    }
}