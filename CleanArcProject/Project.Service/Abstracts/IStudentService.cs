using Project.Data.Entities;
using Project.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Abstracts
{
    public interface IStudentService
    {
        Task<List<Student>> GetStudentsListAsync();
        Task<Student> GetStudentByIdAsync(int id);
        Task<string> AddAsync(Student student);
        Task<bool> IsNameExist(string name);
        Task<bool> IsNameExistExcludeSelf(string name , int id);
        Task<string> EditAsync(Student student);

        Task<bool> DeleteAsync(Student student);
        IQueryable<Student> GetStudentQueryable();
        IQueryable<Student> FilterStudentPaginatedQuerable(StudentOrderingEnum orderingEnum,string searchTerm = null);

    }
}
