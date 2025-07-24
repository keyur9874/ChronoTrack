//using ChronoTrack.Model.DTOs.Organization;
//using ChronoTrack.Service.Interfaces;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System.Security.Claims;

//namespace ChronoTrack.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class OrganizationController(IOrganizationService organizationService) : ControllerBase
//    {
//        [HttpGet("{id}")]
//        public async Task<ActionResult<OrganizationDto?>> GetOrganizationById(int id)
//        {
//            try
//            {
//                var organization = await organizationService.GetOrganizationByIdAsync(id);
//                if (organization == null)
//                {
//                    return NotFound();
//                }
//                return Ok(organization);
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }

//        public async Task<ActionResult<List<OrganizationDto>>> GetUserOrganizations()
//        {
//            try
//            {
//                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
//                var organizations = await organizationService.GetUserOrganizationsAsync(userId);
//                return Ok(organizations);
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }

//        public async Task<ActionResult<OrganizationDto?>> CreateOrganization(CreateOrganizationDto createOrganizationDto)
//        {
//            try
//            {
//                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
//                var organization = await organizationService.CreateOrganizationAsync(createOrganizationDto, userId);
//                if (organization == null)
//                {
//                    return BadRequest("Failed to create organization.");
//                }
//                return CreatedAtAction(nameof(GetOrganizationById), new { id = organization.Id }, organization);
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }

//        [HttpPost("{organizationId}/users/{userId}")]
//        public async Task<ActionResult<bool>> AddUserToOrganization(int organizationId, string userId, string role = "Member")
//        {
//            try
//            {
//                var result = await organizationService.AddUserToOrganizationAsync(organizationId, userId, role);
//                if (!result)
//                {
//                    return BadRequest("Failed to add user to organization.");
//                }
//                return Ok(result);
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }

//        [HttpDelete("{organizationId}/users/{userId}")]
//        public async Task<ActionResult<bool>> RemoveUserFromOrganization(int organizationId, string userId)
//        {
//            try
//            {
//                var result = await organizationService.RemoveUserFromOrganizationAsync(organizationId, userId);
//                if (!result)
//                {
//                    return BadRequest("Failed to remove user from organization.");
//                }
//                return Ok(result);
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }
//    }
//}
