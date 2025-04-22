using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFinance.DtoModels.Models;
using MyFinance.Models;
using MyFinance.Services.IRepository;
using System.Text.RegularExpressions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyFinance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class MembersController : ControllerBase
    {
        private readonly ILogger<MembersController> _logger;
        private readonly IMemberRepositoryService _memberRepositoryService;
        private readonly IMapper _mapper;

        public MembersController(ILogger<MembersController> logger, IMemberRepositoryService memberRepositoryService, IMapper mapper)
        {
            _logger = logger;
            _memberRepositoryService = memberRepositoryService;
            _mapper = mapper;
        }

        [HttpGet("approved")]
        [ProducesResponseType<List<MemberDto>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var members = await _memberRepositoryService.GetAllApprovedMembersAsync();

            var membersDto = _mapper.Map<List<MemberDto>>(members);
            return Ok(membersDto);
        }

        [HttpGet("approved/{id}")]
        [ProducesResponseType<MemberDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var member = await _memberRepositoryService.GetApprovedMemberbyIdAsync(id);
            if (member == null)
            {
                return NotFound();
            }

            var memberDto = _mapper.Map<MemberDto>(member);
            return Ok(memberDto);
        }

        [HttpGet("unapproved")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType<List<MemberDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetUnApproved()
        {
            var members = await _memberRepositoryService.GetAllUnApprovedMembersAsync();

            var membersDto = _mapper.Map<List<MemberDto>>(members);
            return Ok(membersDto);
        }

        [HttpGet("unapproved/{id}", Name ="GetUnApprovedById")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType<MemberDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetUnApproved(int id)
        {
            var member = await _memberRepositoryService.GetUnApprovedMemberbyIdAsync(id);
            if (member == null)
            {
                return NotFound();
            }

            var memberDto = _mapper.Map<MemberDto>(member);
            return Ok(memberDto);
        }

        [HttpPut("approve/{id}")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutApproveMember(int id)
        {
            var success = await _memberRepositoryService.ApproveOnboarding(id);
            if (!success)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpGet("search/name/{name}")]
        [ProducesResponseType<List<MemberSearchDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByName(string name)
        {
            var members = await _memberRepositoryService.GetMembersByNameAsync(name);
            if (members == null)
            {
                return NotFound();
            }

            //var membersDto = _mapper.Map<List<MemberDto>>(members);
            return Ok(members);
        }

        [HttpGet("search/mobileNumber/{mobileNumber}")]
        [ProducesResponseType<MemberSearchDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByMobileNumber(string mobileNumber)
        {
            if(mobileNumber==null || mobileNumber.Length<10)
            {
                return BadRequest("Mobile number should have exactly 10 digits");
            }
            var member = await _memberRepositoryService.GetMemberbyMobileNumber(mobileNumber);
            if (member == null)
            {
                return NotFound();
            }

            //var memberDto = _mapper.Map<MemberDto>(member);
            return Ok(member);
        }

        [HttpGet("search/aadharNumber/{aadharNumber}")]
        [ProducesResponseType<MemberSearchDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByAadharNumber(string aadharNumber)
        {
            if (string.IsNullOrWhiteSpace(aadharNumber))
            {
                return BadRequest("Aadhaar number is required.");
            }

            if (!IsValidAadhaar(aadharNumber))
            {
                return BadRequest("Invalid Aadhaar number.");
            }

            var member = await _memberRepositoryService.GetMemberbyAadharNumber(aadharNumber);
            if (member == null)
            {
                return NotFound();
            }

            //var memberDto = _mapper.Map<MemberDto>(member);
            return Ok(member);
        }

        //POST api/<MembersController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Post([FromBody] MemberDto memberDto)
        {
            var member = _mapper.Map<Member>(memberDto);
            var existing = await _memberRepositoryService.GetMemberbyAadharNumber(member.AadharNumber);
            if (existing != null)
                return Conflict();

            member = await _memberRepositoryService.AddMemberAsync(member);
            return Created($"api/Members/unapproved/{member.MemberId}", member);
        }

        [HttpPut("approved/{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutApproved(int id, [FromBody] MemberDto memberDto)
        {
            var member = await _memberRepositoryService.GetApprovedMemberbyIdAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            member = _mapper.Map(memberDto,member);
            await _memberRepositoryService.UpdateMemberAsync(member, memberDto);
            return NoContent();
        }

        [HttpPut("unapproved/{id}")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutUnApproved(int id, [FromBody] MemberDto memberDto)
        {
            var member = await _memberRepositoryService.GetUnApprovedMemberbyIdAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            member = _mapper.Map(memberDto, member);
            await _memberRepositoryService.UpdateMemberAsync(member, memberDto);
            return NoContent();
        }

        [HttpDelete("approved/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType<List<MemberDto>>(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteApproved(int id)
        {
            var member = await _memberRepositoryService.GetApprovedMemberbyIdAsync(id);
            if (member == null)
            {
                return NotFound();
            }

            await _memberRepositoryService.DeleteMemberAsync(member);
            return NoContent();
        }

        [HttpDelete("unapproved/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType<List<MemberDto>>(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> DeleteUnapproved(int id)
        {
            var member = await _memberRepositoryService.GetUnApprovedMemberbyIdAsync(id);
            if (member == null)
            {
                return NotFound();
            }

            await _memberRepositoryService.DeleteMemberAsync(member);
            return NoContent();
        }

        /// <summary>
        /// checks the valid aadhar number
        /// </summary>
        /// <param name="aadhaarNumber"></param>
        /// <returns>true if aadhar is valid else false</returns>
        private bool IsValidAadhaar(string aadhaarNumber)
        {
            // Format check: must be 12 digits and start with 2-9
            if (!Regex.IsMatch(aadhaarNumber, "^[2-9][0-9]{11}$"))
                return false;

            return VerhoeffValidate(aadhaarNumber);
        }

        /// <summary>
        /// Checks valid aadhar number using checksum algorithm
        /// </summary>
        /// <param name="num"></param>
        /// <returns>True if aadhar number is valid</returns>
        private bool VerhoeffValidate(string num)
        {
            int[,] d = new int[,] {
        {0,1,2,3,4,5,6,7,8,9},
        {1,2,3,4,0,6,7,8,9,5},
        {2,3,4,0,1,7,8,9,5,6},
        {3,4,0,1,2,8,9,5,6,7},
        {4,0,1,2,3,9,5,6,7,8},
        {5,9,8,7,6,0,4,3,2,1},
        {6,5,9,8,7,1,0,4,3,2},
        {7,6,5,9,8,2,1,0,4,3},
        {8,7,6,5,9,3,2,1,0,4},
        {9,8,7,6,5,4,3,2,1,0}
    };

            int[,] p = new int[,] {
        {0,1,2,3,4,5,6,7,8,9},
        {1,5,7,6,2,8,3,0,9,4},
        {5,8,0,3,7,9,6,1,4,2},
        {8,9,1,6,0,4,3,5,2,7},
        {9,4,5,3,1,2,6,8,7,0},
        {4,2,8,6,5,7,3,9,0,1},
        {2,7,9,3,8,0,6,4,1,5},
        {7,0,4,6,9,1,3,2,5,8}
    };

            int[] inv = new int[] { 0, 4, 3, 2, 1, 5, 6, 7, 8, 9 };

            int c = 0;
            int[] myArray = num.Reverse().Select(ch => int.Parse(ch.ToString())).ToArray();

            for (int i = 0; i < myArray.Length; i++)
            {
                c = d[c, p[i % 8, myArray[i]]];
            }

            return c == 0;
        }


    }
}
