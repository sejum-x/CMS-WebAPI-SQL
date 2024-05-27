using CMS_WebAPI_SQL.Business;
using CMS_WebAPI_SQL.Models;
using Microsoft.AspNetCore.Mvc;

namespace CMS_WebAPI_SQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("register")]
        public IActionResult RegisterAdmin(Admin admin)
        {
            try
            {
                _adminService.RegisterAdmin(admin);
                return Ok(new { success = true, message = "Admin registered successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "UnknownError", message = ex.Message });
            }
        }

        [HttpPost("login")]
        public IActionResult LoginAdmin([FromBody] Admin admin)
        {
            var loggedInAdmin = _adminService.LoginAdmin(admin.Email, admin.Password);
            if (loggedInAdmin == null)
            {
                return Unauthorized(new { error = "InvalidCredentials", message = "Invalid email or password" });
            }

            return Ok(loggedInAdmin);
        }



        [HttpPut("{id}")]
        public IActionResult UpdateAdmin(int id, Admin admin)
        {
            try
            {
                _adminService.UpdateAdmin(id, admin);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = "InvalidData", message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "UnknownError", message = "Unknown error" });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAdmin(int id)
        {
            var success = _adminService.DeleteAdmin(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("all")]
        public IActionResult DeleteAllAdmins()
        {
            _adminService.DeleteAllAdmins();

            return NoContent();
        }
    }
}
