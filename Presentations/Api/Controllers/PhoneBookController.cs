using Application.Contracts;
using Application.Contracts.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneBookController : ControllerBase
    {
        private readonly IPhoneBookApplicationService _phoneBookApplicationService;

        public PhoneBookController(IPhoneBookApplicationService phoneBookApplicationService)
        {
            _phoneBookApplicationService = phoneBookApplicationService;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Add([FromBody] AddPhoneBookRequestDto request, CancellationToken cancellationToken)
        {
            return Ok(await _phoneBookApplicationService.Add(request, cancellationToken));
        }

        [HttpPost(template: "{phoneBookId}/rows")]
        public async Task<ActionResult<int>> AddRow(
            [FromRoute] int phoneBookId,
            [FromBody] AddRowRequestDto request,
            CancellationToken cancellationToken)
        {
            return Ok(await _phoneBookApplicationService.AddRow(phoneBookId, request, cancellationToken));
        }

        [HttpPut(template: "{phoneBookId}/rows/{rowId}")]
        public async Task<ActionResult> UpdateRow(
            [FromRoute] int phoneBookId,
            [FromRoute] int rowId,
            [FromBody] UpdateRowRequestDto request,
            CancellationToken cancellationToken)
        {
            await _phoneBookApplicationService.UpdateRow(phoneBookId, rowId, request, cancellationToken);
            return Ok();
        }

        [HttpDelete(template: "{phoneBookId}/rows/{rowId}")]
        public async Task<ActionResult> DeleteRow(
            [FromRoute] int phoneBookId,
            [FromRoute] int rowId,
            CancellationToken cancellationToken)
        {
            await _phoneBookApplicationService.DeleteRow(phoneBookId, rowId, cancellationToken);

            return Ok();
        }

        [HttpGet(template: "{phoneBookId}/rows")]
        public async Task<ActionResult<IEnumerable<RowDto>>> GetRows(
            [FromRoute] int phoneBookId,
            [FromQuery] string? tag,
            CancellationToken cancellationToken)
        {
            return Ok(await _phoneBookApplicationService.GetRowsByTag(phoneBookId, tag, cancellationToken));
        }
    }
}
