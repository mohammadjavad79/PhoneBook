using Application;
using Application.Contracts.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Sqlite;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class PhoneBookApplicationServiceTests
    {
        private readonly PhoneBookApplicationService _service;
        private readonly PhoneBookDbContext _context;
        private readonly PhoneBookRepository _repository;

        public PhoneBookApplicationServiceTests()
        {
            var options = new DbContextOptionsBuilder<PhoneBookDbContext>()
                .UseInMemoryDatabase(databaseName: "PhoneBookTestDb")
                .Options;

            _context = new PhoneBookDbContext(options);
            _repository = new PhoneBookRepository(_context);
            _service = new PhoneBookApplicationService(_repository);
        }

        [Fact(DisplayName = " Add PhoneBook successfully")]
        public async Task AddPhoneBook_Should_Add_New_Record()
        {
            // Arrange
            var request = new AddPhoneBookRequestDto { Title = "Friends" };
            var cancellationToken = CancellationToken.None;

            // Act
            var id = await _service.Add(request, cancellationToken);

            // Assert
            var phoneBook = await _repository.GetById(id, cancellationToken);
            Assert.NotNull(phoneBook);
            Assert.Equal("Friends", phoneBook.Title);
        }

        [Fact(DisplayName = " Add Row to PhoneBook successfully")]
        public async Task AddRow_Should_Add_New_Row_To_PhoneBook()
        {
            // Arrange
            var phoneBookId = await _service.Add(new AddPhoneBookRequestDto { Title = "Family" }, CancellationToken.None);

            var request = new AddRowRequestDto
            {
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "123456789",
                Tag = "Brother"
            };

            // Act
            var rowId = await _service.AddRow(phoneBookId, request, CancellationToken.None);

            // Assert
            var phoneBook = await _repository.GetById(phoneBookId, CancellationToken.None);
            Assert.Single(phoneBook.Rows);
            Assert.Equal("John", phoneBook.Rows.First().FirstName);
            Assert.Equal(rowId, phoneBook.Rows.First().Id);
        }

        [Fact(DisplayName = " Remove Row from PhoneBook successfully")]
        public async Task RemoveRow_Should_Delete_Row_From_PhoneBook()
        {
            // Arrange
            var phoneBookId = await _service.Add(new AddPhoneBookRequestDto { Title = "Colleagues" }, CancellationToken.None);
            var addRowRequest = new AddRowRequestDto
            {
                FirstName = "Alice",
                LastName = "Smith",
                PhoneNumber = "987654321",
                Tag = "Work"
            };

            var rowId = await _service.AddRow(phoneBookId, addRowRequest, CancellationToken.None);

            // Act
            await _service.DeleteRow(phoneBookId, rowId, CancellationToken.None);

            // Assert
            var phoneBook = await _repository.GetById(phoneBookId, CancellationToken.None);
            Assert.Empty(phoneBook.Rows);
        }
    }
}
