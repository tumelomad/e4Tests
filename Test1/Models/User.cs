using System;
using System.ComponentModel.DataAnnotations;

namespace Test1.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CellPhoneNumber { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastUpdated { get; set; }
    }

    public class UserBindingModel
    {

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Display(Name = "Cellphone Number")]
        public string CellPhoneNumber { get; set; }

        public static implicit operator UserBindingModel(User user)
        {
            return new UserBindingModel
            {
                Name = user.Name,
                Surname = user.Surname,
                CellPhoneNumber = user.CellPhoneNumber
            };
        }

        public static implicit operator User(UserBindingModel user)
        {
            return new User
            {
                Name = user.Name,
                Surname = user.Surname,
                CellPhoneNumber = user.CellPhoneNumber
            };
        }
    }

    public class UserViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Display(Name = "Cellphone Number")]
        public string CellPhoneNumber { get; set; }

        [Display(Name = "Last Updated")]
        public DateTime LastUpdated { get; set; }

        public static implicit operator UserViewModel(User user)
        {
            return new UserViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                CellPhoneNumber = user.CellPhoneNumber,
                LastUpdated = user.LastUpdated
            };
        }
    }
}