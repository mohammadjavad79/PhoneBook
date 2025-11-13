using Application.Contracts;
using Application.Contracts.DTOs;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application
{
    public class PhoneBookApplicationService : IPhoneBookApplicationService
    {
        private readonly IPhoneBookRepository _repository;

        public PhoneBookApplicationService(IPhoneBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Add(AddPhoneBookRequestDto request, CancellationToken cancellationToken)
        {
            var phoneBook = new PhoneBook(request.Title);

            await _repository.Add(phoneBook, cancellationToken);

            return phoneBook.Id;
        }

        public async Task<int> AddRow(int phoneBookId, AddRowRequestDto request, CancellationToken cancellationToken)
        {
            var phoneBook = await _repository.GetById(phoneBookId, cancellationToken);
            if(phoneBook == null)
            {
                throw new KeyNotFoundException(NotFoundMessage(phoneBookId));
            }

            var row = new Row(request.FirstName, request.LastName, request.PhoneNumber, request.Tag, phoneBookId);
            phoneBook.AddRow(row);

            await _repository.Update(phoneBook, cancellationToken);

            return row.Id;
        }

        public async Task DeleteRow(int phoneBookId, int rowId, CancellationToken cancellationToken)
        {
            var phoneBook = await _repository.GetById(phoneBookId, cancellationToken);

            if (phoneBook == null)
            {
                throw new KeyNotFoundException(NotFoundMessage(phoneBookId));
            }

            phoneBook.RemoveRow(rowId);

            await _repository.Update(phoneBook, cancellationToken);
        }

        public async Task UpdateRow(int phoneBookId, int rowId, UpdateRowRequestDto request, CancellationToken cancellationToken)
        {
            var phoneBook = await _repository.GetById(phoneBookId, cancellationToken);
            if(phoneBook == null)
            {
                throw new KeyNotFoundException($"Phone book with ID {phoneBookId} Not Found");
            }
            phoneBook.UpdateRow(rowId, request.FirstName, request.LastName, request.PhoneNumber, request.Tag);

            await _repository.Update(phoneBook,cancellationToken);
        }
        public async Task<IEnumerable<RowDto>> GetRowsByTag(int phoneBookId, string? tag, CancellationToken cancellationToken)
        {
            IEnumerable<Row> rows = await _repository.GetRowsByTag(phoneBookId, tag, cancellationToken);

            List<RowDto> rowDtos = new List<RowDto>();

            foreach (var row in rows)
            {
                rowDtos.Add(new RowDto()
                {
                    Id = row.Id,
                    FirstName = row.FirstName,
                    LastName = row.LastName,
                    PhoneNumber = row.PhoneNumber,
                    Tag = row.Tag
                });
            }

            return rowDtos;
        }

        private string NotFoundMessage(int id)
        {
            return $"Phone book with ID {id} Not Found";
        }
    }
}
