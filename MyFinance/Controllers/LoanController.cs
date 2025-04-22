using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Models;
using MyFinance.Models.DtoModels;
using MyFinance.Services.IRepository;

namespace MyFinance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class LoanController : ControllerBase
    {
        private readonly ILoanRepositoryService _loanRepositoryService;
        private readonly IMemberRepositoryService _memberRepositoryService;
        private readonly IMapper _mapper;
        private readonly string? user;

        public LoanController(ILoanRepositoryService loanRepositoryService, IMemberRepositoryService memberRepositoryService,
            IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _loanRepositoryService = loanRepositoryService;
            _memberRepositoryService = memberRepositoryService;
            _mapper = mapper;
            user = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;
            user = user?.Split('@')[0];
        }

        [HttpGet("unapproved")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType<List<GetLoanDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetUnapproved()
        {
            var loans = await _loanRepositoryService.GetAllUnApprovedLoansAsync();
            var loansDto = _mapper.Map<List<GetLoanDto>>(loans);
            return Ok(loansDto);
        }

        [HttpGet("Approved")]
        [ProducesResponseType<List<GetLoanDto>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetApproved()
        {
            var loans = await _loanRepositoryService.GetAllApprovedLoansAsync();
            var loansDto = _mapper.Map<List<GetLoanDto>>(loans);
            return Ok(loansDto);
        }

        [HttpGet("unapproved/{id}")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType<GetLoanDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetUnapprovedById(int id)
        {
            var loan = await _loanRepositoryService.GetUnapprovedLoanbyIdAsync(id);
            if(loan == null)
                return NotFound();
            var loanDto = _mapper.Map<GetLoanDto>(loan);
            return Ok(loanDto);
        }

        [HttpGet("approved/{id}")]
        [ProducesResponseType<GetLoanDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetApprovedById(int id)
        {
            var loan = await _loanRepositoryService.GetApprovedLoanbyIdAsync(id);
            if (loan == null)
                return NotFound();

            var loanDto = _mapper.Map<GetLoanDto>(loan);
            return Ok(loanDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Post([FromBody] PostLoanDto value)
        {
            var member = await _memberRepositoryService.GetApprovedMemberbyIdAsync(value.MemberId);
            if (member == null)
                return BadRequest();

            var existingLoan = await _loanRepositoryService.GetLoanbyMemberIdAsync(value.MemberId);
            if(existingLoan != null)
                return Conflict();

            var loan = _mapper.Map<Loan>(value);
            loan = await _loanRepositoryService.AddLoanAsync(loan);
            return Created($"api/Loan/unapproved/{loan.LoanId}",loan);
        }

        [HttpPut("approve/{id}")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutApproveLoan(int id)
        {
            var success = await _loanRepositoryService.ApproveLoan(id);
            if (!success)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpPut("unapproved/{id}")]
        [Authorize(Roles ="Manager")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(int id, [FromBody] PostLoanDto value)
        {
            var loan = await _loanRepositoryService.GetUnapprovedLoanbyIdAsync(id);
            if (loan == null)
            {
                return NotFound();
            }
            loan = _mapper.Map(value,loan);
            await _loanRepositoryService.UpdateLoanAsync(loan);
            return NoContent();
        }

        [HttpPut("approved/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutDisbursed(int id, [FromBody] PostLoanDto value)
        {
            var loan = await _loanRepositoryService.GetApprovedLoanbyIdAsync(id);
            if (loan == null)
            {
                return NotFound();
            }
            loan = _mapper.Map(value, loan);
            loan.IsDisbursed = true;
            loan.DisbursedBy = user;
            loan.DisbursedOn = DateTime.Now;
            await _loanRepositoryService.UpdateLoanAsync(loan);
            return NoContent();
        }

        [HttpDelete("unapproved/{id}")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var loan = await _loanRepositoryService.GetUnapprovedLoanbyIdAsync(id);
            if (loan == null)
            {
                return NotFound();
            }

            await _loanRepositoryService.DeleteLoanAsync(loan);
            return NoContent();
        }
    }
}
