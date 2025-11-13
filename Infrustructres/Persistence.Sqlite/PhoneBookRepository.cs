
using Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace Persistence.Sqlite
{
    public class PhoneBookRepository : IPhoneBookRepository
    {
        private PhoneBookDbContext _context;

        public PhoneBookRepository(PhoneBookDbContext context)
        {
            _context = context;
        }

        public async Task Add(PhoneBook phoneBook, CancellationToken cancellationToken)
        {
            await _context.PhoneBooks.AddAsync(phoneBook, cancellationToken);
            await _context.SaveChangesAsync();
        }

        public Task<PhoneBook?> GetById(int id, CancellationToken cancellationToken)
        {
            return _context.PhoneBooks.Include("_rows").FirstOrDefaultAsync(pb => pb.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Row>> GetRowsByTag(int phoneBookId, string? tag, CancellationToken cancellationToken)
        {
            var phoneBookExist = await _context.PhoneBooks.Where(pb => pb.Id == phoneBookId).AnyAsync();

            if (!phoneBookExist)
            {
                throw new KeyNotFoundException($"Phone book with id {phoneBookId} not found");
            }

            var query = _context.Rows.Where(r => r.PhoneBookId == phoneBookId);

            if(tag != null)
            {
                query = query.Where(r => r.Tag == tag);
            }

            return await query.ToListAsync();
        }

        public async Task Update(PhoneBook phoneBook, CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
