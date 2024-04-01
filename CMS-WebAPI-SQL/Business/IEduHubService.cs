using CMS_WebAPI_SQL.Models;

namespace CMS_WebAPI_SQL.Business
{
    public interface IEduHubService
    {
        void Add(Student student);
        IEnumerable<Student> GetStudents();
        Student GetStudent(int id);
        void UpdateStudent(int id, Student student);
        bool DeleteStudent(int id);
        bool DeleteAllStudents();
    }
}
