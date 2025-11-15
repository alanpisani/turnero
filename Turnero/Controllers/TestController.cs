using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Turnero.Data;


[ApiController]
[Route("api/[controller]")]
public class DbTestController : ControllerBase
{
	private readonly TurneroContext _context;

	public DbTestController(TurneroContext context)
	{
		_context = context;
	}

	[HttpGet]
	public async Task<IActionResult> Test()
	{
		try
		{
			var user = await _context.Usuarios.FirstOrDefaultAsync();
			return Ok(user != null ? "OK" : "VACIO");
		}
		catch (Exception ex)
		{
			return StatusCode(500, $"ERROR: {ex.Message}");
		}
	}
}
