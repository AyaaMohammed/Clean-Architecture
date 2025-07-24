using Microsoft.EntityFrameworkCore;
using Project.Data.Entities;
using Project.Infrastructure.Abstracts;
using Project.Infrastructure.Context;
using Project.Infrastructure.InfrastructireBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.Repositories
{
    public class StudentRepository : GenericRepositoryAsync<Student>, IStudentRepository
    {
        private readonly DbSet<Student> _students;
        public StudentRepository(ApplicationDBContext db) : base(db)
        {
            _students = db.Set<Student>();
        }
        public async Task<List<Student>> GetAllStudentsAsync()
        {
            return await _students.Include(d => d.Department).ToListAsync();

        }
        public async Task<Student> GetStudentByIdAsync(int id)
        {
            return await _students.Include(d => d.Department).AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
