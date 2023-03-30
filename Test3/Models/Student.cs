using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Test3.Models
{
    public class Student
    {
        public Guid Id { get; set; }
        public string StudentNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int YearOfStudy { get; set; }
        public string CourseOfStudy { get; set; }
        public string CellPhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastUpdated { get; set; }
    }

    public class StudentBindingModel
    {
        [Required]
        public string StudentNumber { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public int YearOfStudy { get; set; }

        [Required]
        public string CourseOfStudy { get; set; }

        [Required]
        public string CellPhoneNumber { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }
    }
}