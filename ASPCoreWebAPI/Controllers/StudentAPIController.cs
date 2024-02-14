using ASPCoreWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {
        private readonly StudentDBContext studentDB;

        public StudentAPIController(StudentDBContext studentDB)
        {
            this.studentDB = studentDB;
        }

        [HttpGet]
        public ActionResult<List<Student>> GetStudents()
        {
            var data = studentDB.Students.ToList();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public ActionResult<Student> GetStudentById(int id)
        {
            var student = studentDB.Students.Find(id);
            if(student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPost]
        public ActionResult<Student> CreateStudent(Student std)
        {
            studentDB.Students.Add(std);
            studentDB.SaveChanges();
            return Ok(std);
        }

        [HttpPut("{id}")]
        public ActionResult<Student> UpdateStudent(int id, Student std)
        {
            if(id != std.Id)
            {
                return BadRequest();
            }
            studentDB.Entry(std).State = EntityState.Modified;
            studentDB.SaveChanges();
            return Ok(std);
        }

        [HttpDelete("{id}")]
        public ActionResult<Student> DeleteStudent(int id)
        {
            var std = studentDB.Students.Find(id);
            if (std == null)
            {
                return NotFound();
            }
            studentDB.Remove(std);
            studentDB.SaveChanges();
            return Ok(std);
        }

    }
}
