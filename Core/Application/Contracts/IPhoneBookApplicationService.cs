using Application.Contracts.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IPhoneBookApplicationService
    {
        Task<int> Add(AddPhoneBookRequestDto request, CancellationToken cancellationToken);
        Task<int> AddRow(int phoneBookId, AddRowRequestDto request, CancellationToken cancellationToken);
        Task UpdateRow(int phoneBookId, int rowId, UpdateRowRequestDto request, CancellationToken cancellationToken);
        Task DeleteRow(int phoneBookId, int rowId, CancellationToken cancellationToken);
        Task<IEnumerable<RowDto>> GetRowsByTag(int phoneBookId, string? tag, CancellationToken cancellationToken);
    }
}
