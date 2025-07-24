//using ChronoTrack.Model.Common;
//using ChronoTrack.Model.DTOs.TimeEntry;
//using ChronoTrack.Service.Interfaces;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System.Security.Claims;

//namespace ChronoTrack.API.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    [Authorize]
//    public class TimeEntriesController : ControllerBase
//    {
//        private readonly ITimeEntryService _timeEntryService;

//        public TimeEntriesController(ITimeEntryService timeEntryService)
//        {
//            _timeEntryService = timeEntryService;
//        }

//        private string GetUserId()
//        {
//            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
//        }

//        [HttpPost]
//        public async Task<ActionResult<ApiResponse<TimeEntryDto>>> CreateTimeEntry([FromBody] CreateTimeEntryDto createTimeEntryDto)
//        {
//            try
//            {
//                var userId = GetUserId();
//                var result = await _timeEntryService.CreateTimeEntryAsync(userId, createTimeEntryDto);
                
//                if (result == null)
//                {
//                    return BadRequest(ApiResponse<TimeEntryDto>.ErrorResponse("Failed to create time entry"));
//                }

//                return Ok(ApiResponse<TimeEntryDto>.SuccessResponse(result, "Time entry created successfully"));
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ApiResponse<TimeEntryDto>.ErrorResponse(ex.Message));
//            }
//        }

//        [HttpPut("{id}")]
//        public async Task<ActionResult<ApiResponse<TimeEntryDto>>> UpdateTimeEntry(int id, [FromBody] UpdateTimeEntryDto updateTimeEntryDto)
//        {
//            try
//            {
//                var userId = GetUserId();
//                var result = await _timeEntryService.UpdateTimeEntryAsync(id, userId, updateTimeEntryDto);
                
//                if (result == null)
//                {
//                    return NotFound(ApiResponse<TimeEntryDto>.ErrorResponse("Time entry not found"));
//                }

//                return Ok(ApiResponse<TimeEntryDto>.SuccessResponse(result, "Time entry updated successfully"));
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ApiResponse<TimeEntryDto>.ErrorResponse(ex.Message));
//            }
//        }

//        [HttpDelete("{id}")]
//        public async Task<ActionResult<ApiResponse<bool>>> DeleteTimeEntry(int id)
//        {
//            try
//            {
//                var userId = GetUserId();
//                var result = await _timeEntryService.DeleteTimeEntryAsync(id, userId);
                
//                if (!result)
//                {
//                    return NotFound(ApiResponse<bool>.ErrorResponse("Time entry not found"));
//                }

//                return Ok(ApiResponse<bool>.SuccessResponse(result, "Time entry deleted successfully"));
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ApiResponse<bool>.ErrorResponse(ex.Message));
//            }
//        }

//        [HttpGet("{id}")]
//        public async Task<ActionResult<ApiResponse<TimeEntryDto>>> GetTimeEntry(int id)
//        {
//            try
//            {
//                var userId = GetUserId();
//                var result = await _timeEntryService.GetTimeEntryByIdAsync(id, userId);
                
//                if (result == null)
//                {
//                    return NotFound(ApiResponse<TimeEntryDto>.ErrorResponse("Time entry not found"));
//                }

//                return Ok(ApiResponse<TimeEntryDto>.SuccessResponse(result));
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ApiResponse<TimeEntryDto>.ErrorResponse(ex.Message));
//            }
//        }

//        [HttpGet]
//        public async Task<ActionResult<ApiResponse<List<TimeEntryDto>>>> GetUserTimeEntries([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
//        {
//            try
//            {
//                var userId = GetUserId();
//                var result = await _timeEntryService.GetUserTimeEntriesAsync(userId, startDate, endDate);
                
//                return Ok(ApiResponse<List<TimeEntryDto>>.SuccessResponse(result));
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ApiResponse<List<TimeEntryDto>>.ErrorResponse(ex.Message));
//            }
//        }

//        [HttpGet("project/{projectId}")]
//        public async Task<ActionResult<ApiResponse<List<TimeEntryDto>>>> GetProjectTimeEntries(int projectId, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
//        {
//            try
//            {
//                var userId = GetUserId();
//                var result = await _timeEntryService.GetProjectTimeEntriesAsync(projectId, userId, startDate, endDate);
                
//                return Ok(ApiResponse<List<TimeEntryDto>>.SuccessResponse(result));
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ApiResponse<List<TimeEntryDto>>.ErrorResponse(ex.Message));
//            }
//        }
//    }
//}