using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Row
    {
        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Tag { get; private set; }
        public int PhoneBookId { get; private set; }

        public Row(string firstName, string lastName, string phoneNumber, string tag, int phoneBookId)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Tag = tag;
            PhoneBookId = phoneBookId;
        }

        public void Update(string firstName, string lastName, string phoneNumber, string tag)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Tag = tag;
        }
    }
}
