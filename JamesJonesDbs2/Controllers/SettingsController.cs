using Microsoft.AspNetCore.Mvc;

namespace JamesJonesApplication.Controllers
{
    /// <summary>
    /// Controller that in-charge of switcing the colour theme to either light or dark mode
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        [HttpPost("SetTheme")]

        public async Task<IActionResult> SetTheme([FromBody] ThemeSetting updatedTheme)
        {
            HttpContext.Session.SetString("theme", updatedTheme.Theme);
            return Ok();
        }

        public class ThemeSetting
        {
            public string Theme { get; set; }
        }
    }
}
