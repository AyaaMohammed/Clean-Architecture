using Project.Data.Entities;
using Project.Infrastructure.InfrastructireBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.Abstracts
{
    public interface IStudentRepository:IGenericRepositoryAsync<Student>
    {
        Task<List<Student>> GetAllStudentsAsync();
        Task<Student> GetStudentByIdAsync(int id);

    }
}
