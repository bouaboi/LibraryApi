using LibraryBusinessLayer;
using LibraryDTOs;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    [Route("api/Fine")]
    [ApiController]
    public class FInesController : ControllerBase
    {

        [HttpGet(Name = "GetAllFines")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<FineDto>> GetAllFines()
        {
            List<FineDto> FinesList = FinesService.GetAllFines();

            if (FinesList == null || FinesList.Count == 0)
            {
                return NotFound("No Fines Found");
            }
            return Ok(FinesList);
        }




        [HttpPut("{fineId}", Name = "SettleFine")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult SettleFine(int fineId, [FromBody] SettleFineDto settleFineDto)
        {
            if (fineId < 1)
            {
                return BadRequest($"Not Accepted ID {fineId}");
            }

            if (FinesService.SettleFine(fineId, settleFineDto.PaidAmount))
                return Ok($"Fine with ID {fineId} has been settled.");
            else
                return NotFound($"Fine with ID {fineId} not found.");

        }




    }
}
