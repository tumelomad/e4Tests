using System;
using System.Collections.Generic;
using System.Globalization;
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
                // logging here
                throw;
            }
        }

        public IEnumerable<User> Get()
        {
            var items = from xElement in _fileData.Descendants("user")
                select new User
                {
                    Id = xElement.Element("Id") != null ? Guid.Parse(xElement.Element("Id").Value) : new Guid(),
                    Name = xElement.Element("Name")?.Value,
                    Surname = xElement.Element("Surname")?.Value,
                    CellPhoneNumber = xElement.Element("CellPhoneNumber")?.Value,
                    DateCreated = !string.IsNullOrEmpty(xElement.Element("DateCreated")?.Value)
                        ? DateTime.Parse(xElement.Element("DateCreated").Value)
                        : new DateTime(),
                    LastUpdated = !string.IsNullOrEmpty(xElement.Element("LastUpdated")?.Value)
                        ? DateTime.Parse(xElement.Element("LastUpdated").Value)
                        : new DateTime()
                };

            return items;
        }

        public User GetById(Guid id)
        {
            XElement xElement = _fileData.Descendants("user")
                .FirstOrDefault(element => element.Element("Id").Value == id.ToString());

            if (xElement == null)
            {
                return null;
            }

            return new User
            {
                Id = Guid.Parse(xElement.Element("Id").Value),
                Name = xElement.Element("Name")?.Value,
                Surname = xElement.Element("Surname")?.Value,
                CellPhoneNumber = xElement.Element("CellPhoneNumber")?.Value,
                DateCreated = !string.IsNullOrEmpty(xElement.Element("DateCreated")?.Value)
                    ? DateTime.Parse(xElement.Element("DateCreated").Value)
                    : new DateTime(),
                LastUpdated = !string.IsNullOrEmpty(xElement.Element("LastUpdated")?.Value)
                    ? DateTime.Parse(xElement.Element("LastUpdated").Value)
                    : new DateTime()
            };
        }

        public void Add(User model)
        {
            XElement xElement = new XElement("user",
                new XElement("Id", Guid.NewGuid().ToString()),
                new XElement("Name", model.Name),
                new XElement("Surname", model.Surname),
                new XElement("CellPhoneNumber", model.CellPhoneNumber),
                new XElement("DateCreated", DateTime.Now.ToString(CultureInfo.InvariantCulture)),
                new XElement("LastUpdated", DateTime.Now.ToString(CultureInfo.InvariantCulture))
            );

            _fileData.Root?.Add(xElement);
            _fileData.Save(_dataLocation);
        }

        public void Add(IEnumerable<User> users)
        {
            foreach (var user in users)
            {
                XElement xElement = new XElement("user",
                    new XElement("Id", Guid.NewGuid().ToString()),
                    new XElement("Name", user.Name),
                    new XElement("Surname", user.Surname),
                    new XElement("CellPhoneNumber", user.CellPhoneNumber),
                    new XElement("DateCreated", DateTime.Now.ToString(CultureInfo.InvariantCulture)),
                    new XElement("LastUpdated", DateTime.Now.ToString(CultureInfo.InvariantCulture))
                );

                _fileData.Root?.Add(xElement);
            }

            _fileData.Save(_dataLocation);
        }

        public void Edit(User model)
        {
            XElement xElement = _fileData.Descendants("user")
                .FirstOrDefault(element => element.Element("Id").Value == model.Id.ToString());

            if (xElement != null)
            {
                xElement.Element("Name").Value = model.Name;
                xElement.Element("Surname").Value = model.Surname;
                xElement.Element("CellPhoneNumber").Value = model.CellPhoneNumber;
                xElement.Element("LastUpdated").Value = DateTime.Now.ToString(CultureInfo.InvariantCulture);

                _fileData.Save(_dataLocation);
                return;
            }

            throw new Exception("User not found.");
        }

        public void Delete(Guid id)
        {
            XElement xElement = _fileData.Descendants("user")
                .FirstOrDefault(element => element.Element("Id").Value == id.ToString());

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