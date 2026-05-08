using LibraryBusinessLayer;
using LibraryDTOs;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    [Route("api/Borrow")]
    [ApiController]
    public class BorrowsController : ControllerBase
    {
        [HttpPost(Name = "AddNewBorrow")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<BorrowDTO> AddNewBorrow(CreateBorrowDTO newBorrowDTO)
        {
            if (newBorrowDTO == null || newBorrowDTO.MemberID < 0 || newBorrowDTO.BookID < 0 )
            {
                return BadRequest("Invalid Data");
            }
            BorrowService Borrow = new BorrowService(new BorrowDTO(0, newBorrowDTO.MemberID, "", newBorrowDTO.BookID, "",
                DateTime.MinValue, DateTime.MinValue, null));

            if (Borrow.Save())

                return CreatedAtRoute("GetBorrowByID", new { borrowId = Borrow.BorrowID}, Borrow.BODTO);

            else
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Adding Borrow");

        }

        [HttpGet(Name = "GetAllBorrows")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<BorrowDTO>> GetAllBorrows()
        {
            List<BorrowDTO> BorrowList = BorrowService.GetAllBorrows();

            if (BorrowList.Count == 0)
            {
                return NotFound("No Borrows Found");
            }
            return Ok(BorrowList);
        }

        [HttpGet("{borrowId}", Name = "GetBorrowByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<BorrowDTO> GetBorrowByID(int borrowId)
        {
            if (borrowId < 1)
            {
                return BadRequest($"Not Accepted Borrow ID {borrowId}");
            }

            BorrowService Borrow = BorrowService.Find(borrowId);

            if (Borrow == null)
            {
                return NotFound($"Borrow with ID {borrowId} not found");
            }

            BorrowDTO BODTO = Borrow.BODTO;

            return Ok(BODTO);

        }


        [HttpGet("Member/{borrowMemberId}", Name = "GetBorrowByMemberID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<BorrowDTO>>GetBorrowByMemberID(int borrowMemberId)
        {
            if (borrowMemberId < 1)
            {
                return BadRequest($"Not Accepted Borrow Member ID {borrowMemberId}");
            }
            List<BorrowDTO> BorrowList = BorrowService.GetBorrowByMemberId(borrowMemberId);
            if (BorrowList == null || BorrowList.Count == 0)
                return NotFound($"No borrows found for Member ID {borrowMemberId}");


            return Ok(BorrowList);


        }

        [HttpGet("Active", Name = "GetActiveBorrows")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<BorrowDTO>> GetActiveBorrows()
        {
            List<BorrowDTO> BorrowList = BorrowService.GetActiveBorrows();

            if (BorrowList == null || BorrowList.Count == 0)
            {
                return NotFound("No Active Borrows Found");
            }
            return Ok(BorrowList);
        }


        [HttpPut("{borrowId}", Name = "ReturnBook")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult ReturnBook(int borrowId)
        {
            if (borrowId < 1)
            {
                return BadRequest($"Not Accepted ID {borrowId}");
            }

            if (BorrowService.ReturnBook(borrowId))
                return Ok($"Borrow with ID {borrowId} has been returned.");
            else
                return NotFound($"Borrow with ID {borrowId} not found.");

        }

    }
}
