using FavoriteMovies.Server.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FavoriteMovies.Shared;
using Microsoft.AspNetCore.Identity;
using FavoriteMovies.Server.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace FavoriteMovies.Server.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("api/User")]
        public async Task<ActionResult<List<Movie>>> GetMovies()
        {
            var user = await _context.Users
                .Include(u => u.FavoriteMovies)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    FavoriteMovies = u.FavoriteMovies
                })
                .FirstOrDefaultAsync(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        [Route("api/add-movie")]
        public async Task<ActionResult> AddMovie([FromBody] Movie movie)
        {
            var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (user == null)
            {
                return NotFound();
            }
            user.FavoriteMovies.Add(movie);
            _context.SaveChanges();
            return Ok();
        }
    }
}
