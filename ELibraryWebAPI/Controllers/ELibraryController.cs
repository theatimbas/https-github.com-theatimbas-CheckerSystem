using ELibraryDataLogic;
using Microsoft.AspNetCore.Mvc;
using PFinderCommon;

namespace ELibraryWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ELibraryController : Controller
    {
        private readonly IFinderDataService _dataService = new PenFinderDB();

        [HttpPost("register")]
        public ActionResult<bool> Register([FromBody] UserAccount account)
        {
            if (string.IsNullOrWhiteSpace(account.UserName) || string.IsNullOrWhiteSpace(account.Password))
                return BadRequest("Username and password are required.");

            if (_dataService.GetAccountByUsername(account.UserName) != null)
                return Conflict("User already exists.");

            account.Favorites = new List<string>();
            _dataService.CreateAccount(account);
            return Ok(true);
        }

        [HttpPost("login")]
        public ActionResult<bool> Login([FromBody] UserAccount login)
        {
            var user = _dataService.GetAccountByUsername(login.UserName);
            if (user != null && user.Password == login.Password)
                return Ok(true);

            return Unauthorized();
        }

        [HttpGet("favorites/{username}")]
        public ActionResult<List<string>> GetFavorites(string username)
        {
            var user = _dataService.GetAccountByUsername(username);
            if (user == null) return NotFound();

            return Ok(_dataService.GetFavorites(username));
        }

        [HttpPost("favorites/add")]
        public ActionResult<bool> AddFavorite([FromBody] FavoriteRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.UserName) || string.IsNullOrWhiteSpace(req.BookTitle))
                return BadRequest();

            return Ok(_dataService.AddFavorite(req.UserName, req.BookTitle));
        }

        [HttpPost("favorites/remove")]
        public ActionResult<bool> RemoveFavorite([FromBody] FavoriteRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.UserName) || string.IsNullOrWhiteSpace(req.BookTitle))
                return BadRequest();

            return Ok(_dataService.RemoveFavorite(req.UserName, req.BookTitle));
        }

        [HttpPost("account/delete")]
        public ActionResult<bool> DeleteAccount([FromBody] string username)
        {
            var deleted = _dataService.DeleteAccount(username);
            return Ok(deleted);
        }

        [HttpPost("account/update-password")]
        public ActionResult<bool> UpdatePassword([FromBody] PasswordUpdateRequest req)
        {
            var user = _dataService.GetAccountByUsername(req.UserName);
            if (user == null) return NotFound();

            user.Password = req.NewPassword;
            _dataService.UpdateAccount(user);
            return Ok(true);
        }

        [HttpGet("genres")]
        public ActionResult<List<string>> GetGenres()
        {
            return Ok(_dataService.GetGenres());
        }

        [HttpGet("books/{genre}")]
        public ActionResult<List<string>> GetBooksByGenre(string genre)
        {
            return Ok(_dataService.GetBooksByGenre(genre));
        }

        [HttpGet("search")]
        public ActionResult<List<string>> Search([FromQuery] string keyword)
        {
            return Ok(_dataService.SearchBooksTitle(keyword));
        }
    }

    public class FavoriteRequest
    {
        public string UserName { get; set; } = "";
        public string BookTitle { get; set; } = "";
    }

    public class PasswordUpdateRequest
    {
        public string UserName { get; set; } = "";
        public string NewPassword { get; set; } = "";
    }
}
