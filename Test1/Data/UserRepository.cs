using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Test1.Models;

namespace Test1.Data
{
    public class UserRepository
    {
        private readonly string _dataLocation;
        private readonly XDocument _fileData;

        public UserRepository()
        {
            try
            {
                _dataLocation = HttpContext.Current.Server.MapPath("/data/users.xml");
                _fileData = XDocument.Load(_dataLocation);
            }
            catch (Exception exception)
            {

                throw;
            }
        }

        public IEnumerable<User> Get()
        {
            var items = from xElement in _fileData.Descendants("user")
                select new User
                {
                    Id = xElement.Element("id") != null ? Guid.Parse(xElement.Element("id").Value) : new Guid(),
                    Name = xElement.Element("name")?.Value,
                    Surname = xElement.Element("surname")?.Value,
                    CellPhoneNumber = xElement.Element("cellPhoneNumber")?.Value
                };

            return items;
        }

        public User GetById(Guid id)
        {
            XElement xElement = _fileData.Descendants("user")
                .FirstOrDefault(element => element.Element("id").Value == id.ToString());

            if (xElement == null)
            {
                return null;
            }

            return new User
            {
                Id = Guid.Parse(xElement.Element("id").Value),
                Name = xElement.Element("name")?.Value,
                Surname = xElement.Element("surname")?.Value,
                CellPhoneNumber = xElement.Element("cellPhoneNumber")?.Value
            };
        }

        public void Add(User model)
        {
            XElement xElement = new XElement("user",
                new XElement("id", Guid.NewGuid().ToString()),
                new XElement("name",model.Name),
                new XElement("surname", model.Surname),
                new XElement("cellPhoneNumber", model.CellPhoneNumber));

            _fileData.Root?.Add(xElement);
            _fileData.Save(_dataLocation);
        }

        public void Add(IEnumerable<User> users)
        {
            foreach (var user in users)
            {
                XElement xElement = new XElement("user",
                    new XElement("id", Guid.NewGuid().ToString()),
                    new XElement("name", user.Name),
                    new XElement("surname", user.Surname),
                    new XElement("cellPhoneNumber", user.CellPhoneNumber));

                _fileData.Root?.Add(xElement);
            }

            _fileData.Save(_dataLocation);
        }

        public void Edit(User model)
        {
            XElement xElement = _fileData.Descendants("user")
                .FirstOrDefault(element => element.Element("id").Value == model.Id.ToString());

            if (xElement != null)
            {
                xElement.Element("name").Value = model.Name;
                xElement.Element("surname").Value = model.Surname;
                xElement.Element("cellPhoneNumber").Value = model.CellPhoneNumber;

                _fileData.Save(_dataLocation);
                return;
            }

            throw new Exception("User not found.");
        }

        public void Delete(Guid id)
        {
            XElement xElement = _fileData.Descendants("user")
                .FirstOrDefault(element => element.Element("id").Value == id.ToString());

            if (xElement != null)
            {
                xElement.Remove();
                _fileData.Save(_dataLocation);
                return;
            }

            throw new Exception("User not found.");
        }
    }
}