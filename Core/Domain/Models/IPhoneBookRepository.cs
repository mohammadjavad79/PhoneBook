using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public interface IPhoneBookRepository
    {
        Task Add(PhoneBook phoneBook, CancellationToken cancellationToken);

        Task Update(PhoneBook phoneBook, CancellationToken cancellationToken);

        Task<PhoneBook?> GetById(int id, CancellationToken cancellationToken);

        Task<IEnumerable<Row>> GetRowsByTag(int phoneBookId, string? tag, CancellationToken cancellationToken);
    }
}
