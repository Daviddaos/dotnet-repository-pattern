namespace PocketBook.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PocketBook.Core.IConfiguratoin;
    using PocketBook.Data;
    using PocketBook.Model;

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public UserController(ILogger<UserController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                user.Id = Guid.NewGuid();
                await _unitOfWork.Users.AddAsync(user);
                await _unitOfWork.SaveChangesAsync();
                return CreatedAtAction("GetById", new { user.Id }, user);
            }

            return BadRequest();
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            return user == null ? NotFound() : Ok(user);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _unitOfWork.Users.GetAllAsync();
            return users == null ? NotFound() : Ok(users);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _unitOfWork.Users.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return deleted ? NoContent() : NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> Upsert(User user)
        {
            if (ModelState.IsValid && user.Id != Guid.Empty)
            {
                await _unitOfWork.Users.UpsertAsync(user);
                await _unitOfWork.SaveChangesAsync();
                return NoContent();
            }

            return BadRequest();
        }
    }
}