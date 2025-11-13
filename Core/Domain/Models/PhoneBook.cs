using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class PhoneBook
    {
        public int Id { get; private set; }
        public string Title { get; private set; }

        private readonly List<Row> _rows = new List<Row>();
        public IReadOnlyCollection<Row> Rows => _rows.AsReadOnly();

        public PhoneBook(string title)
        {
            Title = title;
        }
        public void AddRow(Row row)
        {
            _rows.Add(row);
        }

        public void RemoveRow(int rowId)
        {
            var row = _rows.FirstOrDefault(r => r.Id == rowId);

            if (row == null)
            {
                throw new KeyNotFoundException($"Row With ID {rowId} Not Found");
            }

            _rows.Remove(row);
        }

        public void UpdateRow(int rowId, string firstName, string lastName, string phoneNumber, string tag)
        {
            var row = _rows.FirstOrDefault(r => r.Id == rowId);
            if (row == null)
            {
                throw new KeyNotFoundException($"Row With ID {rowId} Not Found");
            }

            row.Update(firstName, lastName, phoneNumber, tag);
        }
    }
}
