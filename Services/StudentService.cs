using Microsoft.EntityFrameworkCore;
using StudentRegApi.Models;

namespace StudentRegApi.Services
{
    public interface IStudentService
    {
        Task<List<Student>> GetList();
        Task<Student> Get(int StudentID);
        Task<Student> Add(Student model);
        Task<Student> Update(Student model);
        Task<bool> Delete(Student model);
    }
    public class StudentService : IStudentService
    {
        private readonly StudentDBContext _dbContext;

        public StudentService(StudentDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Student>> GetList()
        {
            try
            {
                List<Student> studentlist = new List<Student>();
                studentlist = await _dbContext.Students.ToListAsync();
                return studentlist;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Student> Get(int StudentID)
        {
            try
            {
                Student? studentFound = new Student();
                studentFound = await _dbContext.Students
                            .Where(s => s.StudentId == StudentID)
                            .FirstOrDefaultAsync();
                return studentFound;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Student> Add(Student model)
        {
            try
            {
                _dbContext.Students.Add(model);
                await _dbContext.SaveChangesAsync();
                return model;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Student> Update(Student model)
        {
            try
            {
                _dbContext.Students.Update(model);
                await _dbContext.SaveChangesAsync();
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Delete(Student model)
        {
            try
            {
                _dbContext.Students.Remove(model);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





    }
}
