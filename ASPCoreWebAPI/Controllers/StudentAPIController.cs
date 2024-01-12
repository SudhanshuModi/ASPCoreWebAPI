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
        public async Task<ActionResult<List<Student>>> GetStudents()
        {
            var data = await studentDB.Students.ToListAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudentById(int id)
        {
            var student = await studentDB.Students.FindAsync(id);
            if(student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent(Student std)
        {
            await studentDB.Students.AddAsync(std);
            await studentDB.SaveChangesAsync();
            return Ok(std);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> UpdateStudent(int id, Student std)
        {
            if(id != std.Id)
            {
                return BadRequest();
            }
            studentDB.Entry(std).State = EntityState.Modified;
            await studentDB.SaveChangesAsync();
            return Ok(std);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
            var std = await studentDB.Students.FindAsync(id);
            if (std == null)
            {
                return NotFound();
            }
            studentDB.Remove(std);
            await studentDB.SaveChangesAsync();
            return Ok(std);
        }

    }
}
