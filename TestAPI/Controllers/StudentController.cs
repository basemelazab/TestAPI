using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TestAPI.Data;
using TestAPI.Models;

namespace TestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly StudentDBContext _studentDBContext;
        public StudentController(StudentDBContext studentDBContext)
        {
            _studentDBContext = studentDBContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentDBContext.Students.ToListAsync();

            return Ok(students);
        }
        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] Student student)
        {
            await _studentDBContext.Students.AddAsync(student);
            await _studentDBContext.SaveChangesAsync();
            return Ok(student);
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetStudent([FromRoute] int id)
        {
            var student = await _studentDBContext.Students.FirstOrDefaultAsync(x => x.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> GetStudent([FromRoute] int id, Student updateStudentRequest)
        {
            var student = await _studentDBContext.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            student.Name = updateStudentRequest.Name;
            student.Age = updateStudentRequest.Age;
            student.MobileNo = updateStudentRequest.MobileNo;
            student.Address = updateStudentRequest.Address;

            await _studentDBContext.SaveChangesAsync();

            return Ok(student);
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteStudent([FromRoute] int id)
        {
            var student = await _studentDBContext.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            _studentDBContext.Students.Remove(student);
            await _studentDBContext.SaveChangesAsync();
            return Ok(student);
        }
    }
}
