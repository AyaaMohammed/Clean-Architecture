using Microsoft.EntityFrameworkCore;
using Project.Data.Entities;
using Project.Data.Helpers;
using Project.Infrastructure.Abstracts;
using Project.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Implementation
{
    public class StudentService : IStudentService
    {
        IStudentRepository _StudentRepository;
        public StudentService(IStudentRepository _studentRepositoty)
        {
            _StudentRepository = _studentRepositoty;
        }
        public async Task<List<Student>> GetStudentsListAsync()
        {
            return await _StudentRepository.GetAllStudentsAsync();
        }
        public async Task<Student> GetStudentByIdAsync(int id)
        {
            return await _StudentRepository.GetStudentByIdAsync(id);
        }
        public async Task<string> AddAsync(Student student)
        {
            // check if name is exist Or not
            var existingStudent = await _StudentRepository.GetTableNoTracking().Where(s => s.NameAr == student.NameAr).FirstOrDefaultAsync();
            if (existingStudent != null)
            {
                return "exist";
            }
            // Added student
            await _StudentRepository.AddAsync(student);
            return "Student added successfully.";

        }

        public async Task<bool> IsNameExist(string name)
        {
            var student = await _StudentRepository.GetTableNoTracking().Where(s => s.NameAr == name).FirstOrDefaultAsync();
            if (student != null)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> IsNameExistExcludeSelf(string name, int id)
        {
            var student = await _StudentRepository.GetTableNoTracking().Where(s => s.NameAr == name && s.Id != id).FirstOrDefaultAsync();
            if (student != null)
            {
                return true;
            }
            return false;
        }
        public async Task<string> EditAsync(Student student)
        {
            await _StudentRepository.UpdateAsync(student);
            return "Success";
        }
        public async Task<bool> DeleteAsync(Student student)
        {
            var transaction = _StudentRepository.BeginTransaction();
            try
            {
                await _StudentRepository.DeleteAsync(student);
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return false;
            }
            finally
            {
                transaction.Dispose();
            }

        }
        public IQueryable<Student> GetStudentQueryable()
        {
            return _StudentRepository.GetTableNoTracking().Include(x => x.Department).AsQueryable();
        }

        public IQueryable<Student> FilterStudentPaginatedQuerable(StudentOrderingEnum orderingEnum,string searchTerm = null)
        {
            var queryable = _StudentRepository.GetTableNoTracking().Include(x => x.Department).AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                queryable = queryable.Where(s => s.NameAr.Contains(searchTerm) || s.Email.Contains(searchTerm) );
            }
            switch(orderingEnum)
            {
                case StudentOrderingEnum.Id:
                    queryable = queryable.OrderBy(s => s.Id);
                    break;
                case StudentOrderingEnum.Name:
                    queryable = queryable.OrderBy(s => s.NameAr);
                    break;
                case StudentOrderingEnum.Email: 
                    queryable = queryable.OrderBy(s => s.Email);
                    break;
                case StudentOrderingEnum.DepartmentName:
                    queryable = queryable.OrderBy(s => s.Department.NameAr);
                    break;
            }
            return queryable;
        }
    }
}
