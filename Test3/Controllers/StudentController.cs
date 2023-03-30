using System;
using System.Web.Http;
using Test3.Data;
using Test3.Models;

namespace Test3.Controllers
{
    public class StudentController : ApiController
    {
        private readonly StudentRepository _studentRepository;

        public StudentController()
        {
            _studentRepository = new StudentRepository();
        }

        public IHttpActionResult Get()
        {
            try
            {
                return Ok(_studentRepository.Get());
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        public IHttpActionResult Get(Guid id)
        {
            try
            {
                var student = _studentRepository.GetById(id);

                if (student == null)
                {
                    throw new Exception("user not found.");
                }

                return Ok(student);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        public IHttpActionResult Post([FromBody] StudentBindingModel value)
        {
            try
            {
                _studentRepository.Add(new Student
                {
                    Name = value.Name,
                    Surname = value.Surname,
                    CellPhoneNumber = value.CellPhoneNumber,
                    YearOfStudy = value.YearOfStudy,
                    CourseOfStudy = value.CourseOfStudy,
                    StudentNumber = value.StudentNumber,
                    DateOfBirth = value.DateOfBirth
                });

                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        public IHttpActionResult Put(Guid id, [FromBody] StudentBindingModel value)
        {
            try
            {
                var student = _studentRepository.GetById(id);
                if (student == null)
                {
                    throw new Exception("Record not found.");
                }

                if (value == null)
                {
                    throw new Exception("Payload error.");
                }

                student.Name = value.Name;
                student.Surname = value.Surname;
                student.CellPhoneNumber = value.CellPhoneNumber;
                student.YearOfStudy = value.YearOfStudy;
                student.CourseOfStudy = value.CourseOfStudy;
                student.StudentNumber = value.StudentNumber;
                student.DateOfBirth = value.DateOfBirth;

                _studentRepository.Edit(student);

                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        public IHttpActionResult Delete(Guid id)
        {
            try
            {
                _studentRepository.Delete(id);
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}