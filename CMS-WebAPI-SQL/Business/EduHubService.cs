using CMS_WebAPI_SQL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CMS_WebAPI_SQL.Business
{
    public class EduHubService : IEduHubService
    {
        private readonly StudentContext _studentContext;
        public EduHubService(StudentContext studentContext)
        {
            _studentContext = studentContext;
        }

        public enum StudentValidationResult
        {
            Valid,
            InvalidName,
            InvalidNameFormat,
            Underage
        }

        private (bool, StudentValidationResult) IsValidStudent(Student student)
        {
            if (string.IsNullOrWhiteSpace(student.Name))
            {
                return (false, StudentValidationResult.InvalidName);
            }

            if (!char.IsUpper(student.Name[0]))
            {
                return (false, StudentValidationResult.InvalidNameFormat);
            }

            if (!student.Name.All(c => char.IsLetter(c) || c == ' ' || c == '-'))
            {
                return (false, StudentValidationResult.InvalidNameFormat);
            }

            if (DateTime.Now.Year - student.Birthday.Year < 16)
            {
                return (false, StudentValidationResult.Underage);
            }
            return (true, StudentValidationResult.Valid);
        }


        public void Add(Student student)
        {
            var validationResult = IsValidStudent(student);
            if (!validationResult.Item1)
            {
                string errorMessage = validationResult.Item2 switch
                {
                    StudentValidationResult.InvalidName => "Invalid name",
                    StudentValidationResult.InvalidNameFormat => "Invalid name format",
                    StudentValidationResult.Underage => "Underage, must be over 16 years old",
                    _ => "Unknown error"
                };

                throw new ArgumentException(errorMessage);
            }

            _studentContext.Students.Add(student);
            _studentContext.SaveChanges();
        }

        public void UpdateStudent(int id, Student student)
        {
            if (id != student.ID)
            {
                throw new ArgumentException("Invalid ID");
            }

            var validationResult = IsValidStudent(student);
            if (!validationResult.Item1)
            {
                string errorMessage = validationResult.Item2 switch
                {
                    StudentValidationResult.InvalidName => "Invalid name",
                    StudentValidationResult.InvalidNameFormat => "Invalid name format",
                    StudentValidationResult.Underage => "Underage, must be over 16 years old",
                    _ => "Unknown error"
                };

                throw new ArgumentException(errorMessage);
            }

            _studentContext.Entry(student).State = EntityState.Modified;

            try
            {
                _studentContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }


        public bool DeleteStudent(int id)
        {
            var student = _studentContext.Students.Find(id);
            if (student == null)
            {
                return false;
            }

            _studentContext.Students.Remove(student);
            _studentContext.SaveChanges();

            return true;
        }


        public IEnumerable<Student> GetStudents()
        {
            return _studentContext.Students.ToList();
        }

        public Student GetStudent(int id)
        {
            return _studentContext.Students.Find(id);
        }

        public bool DeleteAllStudents()
        {
            _studentContext.Students.RemoveRange(_studentContext.Students);
            _studentContext.SaveChanges();

            return true;
        }
    }
}
