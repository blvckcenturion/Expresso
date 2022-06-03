using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expresso.Model
{
    public class Employee : BaseTable
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public string CI { get; set; }
        public string Phones { get; set; }
        public string Address { get; set; }
        public char Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Role { get; set; }
        public string Photo { get; set; }
        public byte TownID { get; set; }
        public string TownName { get; set; }
        public string Email { get; set; }

        /// <summary>
        /// SELECT
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="secondLastName"></param>
        /// <param name="cI"></param>
        /// <param name="phones"></param>
        /// <param name="address"></param>
        /// <param name="gender"></param>
        /// <param name="birthDate"></param>
        /// <param name="role"></param>
        /// <param name="photo"></param>
        /// <param name="townID"></param>
        /// <param name="status"></param>
        /// <param name="registerDate"></param>
        /// <param name="lastUpdate"></param>
        /// <param name="userID"></param>
        public Employee(int id, string userName, string password, string firstName, string lastName, string secondLastName, string cI, string phones, string address, char gender, DateTime birthDate, string role, string photo, byte townID,string email, byte status, DateTime registerDate, DateTime lastUpdate, byte userID)
            : base(status, registerDate, lastUpdate, userID)
        {
            Id = id;
            UserName = userName;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            SecondLastName = secondLastName;
            CI = cI;
            Phones = phones;
            Address = address;
            Gender = gender;
            BirthDate = birthDate;
            Role = role;
            Photo = photo;
            TownID = townID;
            Email = email;
        }

        /// <summary>
        /// INSERT 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="secondLastName"></param>
        /// <param name="cI"></param>
        /// <param name="phones"></param>
        /// <param name="address"></param>
        /// <param name="gender"></param>
        /// <param name="birthDate"></param>
        /// <param name="role"></param>
        /// <param name="userID"></param>
        public Employee(string userName, string password, string firstName, string lastName, string secondLastName, string cI, string phones, string address, char gender, DateTime birthDate, string role, string email, string townName)
        {
            UserName = userName;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            SecondLastName = secondLastName;
            CI = cI;
            Phones = phones;
            Address = address;
            Gender = gender;
            BirthDate = birthDate;
            Role = role;
            Email = email;
            TownName = townName;
        }

        /// <summary>
        /// GET
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="v3"></param>
        /// <param name="v4"></param>
        /// <param name="v5"></param>
        /// <param name="v6"></param>
        /// <param name="v7"></param>
        /// <param name="v8"></param>
        /// <param name="dateTime"></param>
        /// <param name="v9"></param>
        /// <param name="v10"></param>
        public Employee(byte id, string phones, string address, string role, string townName, string email, string photo)
        {
            Id = id;
            Phones = phones;
            Address = address;
            Role = role;
            TownName = townName;
            Email = email;
            Photo = photo;
        }

        public Employee(string firstName, string lastName, DateTime birthDate, string role, string userName)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            Role = role;
            UserName = userName;
        }

        public Employee()
        {
        }
    }
}
