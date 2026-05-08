using LibraryDTOs;
using Microsoft.AspNetCore.Mvc;
using LibraryBusinessLayer;

namespace LibraryApi.Controllers
{
    [Route("api/Members")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        [HttpGet(Name ="GetAllMembers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<MemberDTO>> GetAllMembers()
        {
            List<MemberDTO> MembersList = MemberService.GetAllMembers();
            if (MembersList.Count == 0)
            {
                return NotFound("No Members Found");
            }
            return Ok(MembersList);
        }

        [HttpGet("{memberId}", Name = "GetMemberByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult <MemberDTO> GetMemberByID(int memberId)
        {
            if (memberId < 0)
            {
                return BadRequest($"Not Accepted ID {memberId}");
            }

            MemberService Member = MemberService.Find(memberId);

            if (Member == null)
            {
                return NotFound($"Member with ID {memberId} not found");
            }

            MemberDTO MDTO = Member.MDTO;

            return Ok(MDTO);
        }

        [HttpPost(Name = "AddNewMember")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<MemberDTO> AddNewMember(CreateMemberDto newMemberDTO)
        {
            if (newMemberDTO == null || string.IsNullOrEmpty(newMemberDTO.FirstName) || string.IsNullOrEmpty(newMemberDTO.LastName) ||
                newMemberDTO.DateOfBirth >= DateTime.Now || newMemberDTO.DateOfBirth.Year < 1900)
            {
                return BadRequest("Invalid Member Data");
            }
            // DateOfJoin is set automatically by the DB, passing MinValue as placeholder

            MemberService Member = new MemberService(new MemberDTO(0, newMemberDTO.FirstName, newMemberDTO.LastName,
                newMemberDTO.NationalID, newMemberDTO.DateOfBirth, DateTime.MinValue, newMemberDTO.Address, 
                newMemberDTO.Phone));

            Member.Save();


            return CreatedAtRoute("GetMemberByID", new { MemberId = Member.MemberID }, Member.MDTO);
        }




        [HttpPut("{memberId}", Name = "UpdateMember")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]


        public ActionResult<MemberDTO> UpdateMember(int memberId, UpdateMemberDto updatedMember)
        {
            if (memberId < 1 || updatedMember == null || string.IsNullOrEmpty(updatedMember.FirstName) ||
                 string.IsNullOrEmpty(updatedMember.LastName) ||
                 updatedMember.DateOfBirth >= DateTime.Now || updatedMember.DateOfBirth.Year < 1900)
            {
                return BadRequest("Invalid Member Data");
            }

            MemberService Member = MemberService.Find(memberId);

            if (Member == null)
            {
                return NotFound($"Member with ID {memberId} not found");
            }

            // Update the member's details with the new data
            Member.NationalID = updatedMember.NationalID;
            Member.FirstName = updatedMember.FirstName;
            Member.LastName = updatedMember.LastName;
            Member.DateOfBirth = updatedMember.DateOfBirth;
            Member.Address = updatedMember.Address;
            Member.Phone = updatedMember.Phone;

            // Save changes and return the updated member
            if (Member.Save())
            return Ok(Member.MDTO);

            else
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Updating Member");
        }

        [HttpDelete("{memberId}", Name = "DeleteMember")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult DeleteMember (int memberId)
        {
            if (memberId < 1)
            {
                return BadRequest($"Not Accepted ID {memberId}");
            }

            if (MemberService.DeleteMember(memberId))
                return NoContent();
            else
                return NotFound($"Member with ID {memberId} not found. no rows deleted.");
        }


        [HttpPut("{memberId}/reactivate", Name = "ReactiveMember")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult ReactiveMember(int memberId)
        {
            if (memberId < 1)
            {
                return BadRequest($"Not Accepted ID {memberId}");
            }

            if (MemberService.ReactiveMember(memberId))
                return Ok($"Member with ID {memberId} has been Activate.");
            else
                return NotFound($"Member with ID {memberId} not found. no rows activated.");
        }



        [HttpGet("Search", Name = "SearchMember")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<MemberDTO>> SearchMember(string query)
        {
            List<MemberDTO> MembersList = MemberService.SearchMember(query);
            if (MembersList == null || MembersList.Count == 0)
            {
                return NotFound("No Members Found");
            }
            return Ok(MembersList);
        }

    }
}
