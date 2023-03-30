using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Test3.Models;

namespace Test3.Data
{
    public class StudentRepository
    {
        private readonly string _dataLocation;
        private readonly XDocument _fileData;

        public StudentRepository()
        {
            try
            {
                _dataLocation = HttpContext.Current.Server.MapPath("/data/students.xml");
                _fileData = XDocument.Load(_dataLocation);
            }
            catch (Exception exception)
            {
                // Have some sort of logger
                throw;
            }
        }

        public IEnumerable<Student> Get()
        {
            var items = from xElement in _fileData.Descendants("student")
                select new Student
                {
                    Id = Guid.Parse(xElement.Element("Id").Value),
                    StudentNumber = xElement.Element("StudentNumber")?.Value,
                    Name = xElement.Element("Name")?.Value,
                    Surname = xElement.Element("Surname")?.Value,
                    CellPhoneNumber = xElement.Element("CellPhoneNumber")?.Value,
                    YearOfStudy = !string.IsNullOrEmpty(xElement.Element("YearOfStudy")?.Value)
                        ? int.Parse(xElement.Element("YearOfStudy").Value)
                        : 0,
                    CourseOfStudy = xElement.Element("CourseOfStudy")?.Value,
                    DateOfBirth = !string.IsNullOrEmpty(xElement.Element("DateOfBirth")?.Value)
                        ? DateTime.Parse(xElement.Element("DateOfBirth").Value)
                        : new DateTime(),
                    DateCreated = !string.IsNullOrEmpty(xElement.Element("DateCreated")?.Value)
                        ? DateTime.Parse(xElement.Element("DateCreated").Value)
                        : new DateTime(),
                    LastUpdated = !string.IsNullOrEmpty(xElement.Element("LastUpdated")?.Value)
                        ? DateTime.Parse(xElement.Element("LastUpdated").Value)
                        : new DateTime()
                };


            if (items.Count() <= 3)
            {
                AddDummyData();
                items = Get();
            }

            return items;
        }

        public Student GetById(Guid id)
        {
            XElement xElement = _fileData.Descendants("student")
                .FirstOrDefault(element => element.Element("Id").Value == id.ToString());

            if (xElement == null)
            {
                return null;
            }

            return new Student
            {
                Id = Guid.Parse(xElement.Element("Id").Value),
                StudentNumber = xElement.Element("StudentNumber")?.Value,
                Name = xElement.Element("Name")?.Value,
                Surname = xElement.Element("Surname")?.Value,
                CellPhoneNumber = xElement.Element("CellPhoneNumber")?.Value,
                YearOfStudy = !string.IsNullOrEmpty(xElement.Element("YearOfStudy")?.Value)
                    ? int.Parse(xElement.Element("YearOfStudy").Value)
                    : 0,
                CourseOfStudy = xElement.Element("CourseOfStudy")?.Value,
                DateOfBirth = !string.IsNullOrEmpty(xElement.Element("DateOfBirth")?.Value)
                    ? DateTime.Parse(xElement.Element("DateOfBirth").Value)
                    : new DateTime(),
                DateCreated = !string.IsNullOrEmpty(xElement.Element("DateCreated")?.Value)
                    ? DateTime.Parse(xElement.Element("DateCreated").Value)
                    : new DateTime(),
                LastUpdated = !string.IsNullOrEmpty(xElement.Element("LastUpdated")?.Value)
                    ? DateTime.Parse(xElement.Element("LastUpdated").Value)
                    : new DateTime()
            };
        }

        public void Add(Student model)
        {
            XElement xElement = new XElement("student",
                new XElement("Id", Guid.NewGuid().ToString()),
                new XElement("Name", model.Name),
                new XElement("Surname", model.Surname),
                new XElement("CellPhoneNumber", model.CellPhoneNumber),
                new XElement("YearOfStudy", model.YearOfStudy),
                new XElement("CourseOfStudy", model.CourseOfStudy),
                new XElement("StudentNumber", model.StudentNumber),
                new XElement("DateOfBirth", model.DateOfBirth),
                new XElement("DateCreated", DateTime.Now.ToString(CultureInfo.InvariantCulture)),
                new XElement("LastUpdated", DateTime.Now.ToString(CultureInfo.InvariantCulture))
            );

            _fileData.Root?.Add(xElement);
            _fileData.Save(_dataLocation);
        }

        public void Edit(Student model)
        {
            XElement xElement = _fileData.Descendants("student")
                .FirstOrDefault(element => element.Element("Id").Value == model.Id.ToString());

            if (xElement != null)
            {
                xElement.Element("Name").Value = model.Name;
                xElement.Element("Surname").Value = model.Surname;
                xElement.Element("CellPhoneNumber").Value = model.CellPhoneNumber;
                xElement.Element("YearOfStudy").Value = model.YearOfStudy.ToString();
                xElement.Element("CourseOfStudy").Value = model.CourseOfStudy;
                xElement.Element("StudentNumber").Value = model.StudentNumber;
                xElement.Element("DateOfBirth").Value = model.DateOfBirth.ToString(CultureInfo.InvariantCulture);
                xElement.Element("LastUpdated").Value = DateTime.Now.ToString(CultureInfo.InvariantCulture);

                _fileData.Save(_dataLocation);
                return;
            }

            throw new Exception("student not found.");
        }

        public void Delete(Guid id)
        {
            XElement xElement = _fileData.Descendants("student")
                .FirstOrDefault(element => element.Element("Id").Value == id.ToString());

            if (xElement != null)
            {
                xElement.Remove();
                _fileData.Save(_dataLocation);
                return;
            }

            throw new Exception("student not found.");
        }

        private void AddDummyData()
        {
            Random rnd = new Random();
            for (int i = 0; i < 4; i++)
            {
                Add(new Student
                {
                    Name = $"Test Name {i + 1}",
                    Surname = $"Test Surname {i + 1}",
                    StudentNumber = $"Test StudentNumber {i + 1}",
                    YearOfStudy = rnd.Next(1, 4),
                    CourseOfStudy = $"Test Course Of Study {i + 1}",
                    CellPhoneNumber = $"071234567{i + 1}",
                    DateOfBirth = RandomDate()
                });
            }
        }

        private DateTime RandomDate()
        {
            Random gen = new Random();
            DateTime start = new DateTime(1985, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range));
        }
    }
}