using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentProj.Models;
using System;
using System.Linq;

namespace StudentProj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentContext _studentContext;

        
        public StudentController(StudentContext studentContext)
        {
            _studentContext = studentContext;
        }
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Student>>>GetStudent()
        //{
        //    if(_studentContext == null)
        //    {
        //        return NotFound();
        //    }
        //    return await _studentContext.Students.ToListAsync();
        //}
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Student>> GetStudent(int id)
        //{
        //    if (_studentContext == null)
        //    {
        //        return NotFound();
        //    }
        //    var student = await _studentContext.Students.FindAsync(id);
        //    if (student == null)
        //    {
        //        return NotFound();
        //    }

        //    return student;

        //}


        //[HttpGet("{id}")]
        //public  Task<ActionResult> GetStudent(int id)
        //{
        //    var student = await _studentContext.Students.Where(x => x.Id == id).FirstOrDefault();
        //    //.Include(_ => _.Subject)
        //    //.ToListAsync();

        //    return Ok(student);
        //}
        [HttpGet]
        public async Task<ActionResult> GetStudent()
        {
            var student = await _studentContext.Students
                .Include(_ => _.Subject).ToListAsync();
            return Ok(student);
        }



        [HttpGet("{id}")]
        public ActionResult<Student> GetStudent(int id)
        {
            if (id <= 0)
            {
                return NotFound("not found");
               
            }
            Student s1 = _studentContext.Students.Include(_ => _.Subject).FirstOrDefault(s => s.Id ==id);
            if (s1 == null)
            {
                return NotFound("notfound");
            }

            return Ok(s1);
        }

        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            _studentContext.Students.Add(student);
            await _studentContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutStudent(int id, Student student)
        {
            if (student == null)
            {
                return BadRequest();
            }
            _studentContext.Entry(student).State = EntityState.Modified;

            try
            {
                await _studentContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!StudentAvailable(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
        }
        private bool StudentAvailable(int id)
        {
            return (_studentContext.Students?.Any(x => x.Id == id)).GetValueOrDefault();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            if (_studentContext.Students == null)
            {
                return NotFound();
            }
            var student = await _studentContext.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            _studentContext.Students.Remove(student);
            await _studentContext.SaveChangesAsync();
            return Ok();
        }


    }
}
