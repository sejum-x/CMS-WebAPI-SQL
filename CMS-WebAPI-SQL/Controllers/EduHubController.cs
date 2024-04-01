using CMS_WebAPI_SQL.Models;
using CMS_WebAPI_SQL.Business;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CMS_WebAPI_SQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EduHubController : ControllerBase
    {
        private readonly IEduHubService _eduHubService;

        public EduHubController(IEduHubService eduHubService)
        {
            _eduHubService = eduHubService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetStudents()
        {
            var students = _eduHubService.GetStudents();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public ActionResult<Student> GetStudent(int id)
        {
            var student = _eduHubService.GetStudent(id);
            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        [HttpPost]
        public IActionResult PostStudent(Student student)
        {
            try
            {
                _eduHubService.Add(student);
                return Ok(new { success = true, message = "Student added successfully" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = "InvalidData", message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "UnknownError", message = "Unknown error" });
            }
        }


        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, Student student)
        {
            try
            {
                _eduHubService.UpdateStudent(id, student);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = "InvalidData", message = ex.Message });
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { error = "ConcurrencyError", message = "Concurrency error" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "UnknownError", message = "Unknown error" });
            }
        }



        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            var success = _eduHubService.DeleteStudent(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("all")]
        public IActionResult DeleteAllStudents()
        {
            _eduHubService.DeleteAllStudents();

            return NoContent();
        }
    }
}
