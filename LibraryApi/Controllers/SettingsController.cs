using LibraryDTOs;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    [Route("api/Settings")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        [HttpPut(Name = "UpdateSettings")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<SettingDto> UpdateSettings(UpdateSettingDto updatedSettings)
        {
            if (updatedSettings == null || updatedSettings.MaxBorrowLimit < 1 ||
                updatedSettings.BorrowDurationDays < 1 || updatedSettings.DailyFineRate < 0)
            {
                return BadRequest("Invalid Settings Data");
            }

            SettingService Settings = SettingService.GetCurrentSettings();
            if (Settings == null)
                return NotFound("Settings not found");

            // Apply the new values
            Settings.MaxBorrowLimit = updatedSettings.MaxBorrowLimit;
            Settings.BorrowDurationDays = updatedSettings.BorrowDurationDays;
            Settings.DailyFineRate = updatedSettings.DailyFineRate;

            if (Settings.Save())
                return Ok(Settings.SEDTO);
            else
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Updating Settings");
        }

        [HttpGet(Name = "GetSettings")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<SettingDto> GetSettings()
        {

            // Fetch the settings to send back to the user
            SettingDto SDTO = SettingService.GetSettings();
            if (SDTO == null)
                return NotFound("Settings not found");
            return Ok(SDTO);
        }
    }
}
