using EventosShared.Interfaces;
using EventosShared.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventosAuth.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController(IRepository<User> userRepository) : ControllerBase
{
	private readonly IRepository<User> _userRepository = userRepository;

	[HttpGet("getAll")]
	[Authorize]
	public async Task<ActionResult> GetAll()
	{
		var users = await _userRepository.GetAllAsync();
		return Ok(users);
	}

	// GET: UserController/Details/5
	[HttpGet]
	[Authorize]
	public async Task<ActionResult> Get(Guid id)
	{
		var user = await _userRepository.GetByIdAsync(id);

		if (user is null)
			return NotFound("Usuário não localizado");

		return Ok(user);
	}

        [HttpGet("getByEmail")]
        [Authorize]
        public async Task<ActionResult> GetByEmail(string email)
        {
            var user = await _userRepository.GetByExpression(x => x.Email == email);

            if (user is null)
                return NotFound("Usuário não localizado");

            return Ok(user);
        }

        [HttpPost]
	public async Task<ActionResult> Create(User user)
	{
		await _userRepository.CreateAsync(user);

		return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
	}

	[HttpPost("simpleCreate")]
	public async Task<ActionResult> SimpleCreate(User user)
	{
		await _userRepository.CreateAsync(user);

		return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
	}

	// PUT: UserController/Edit/5
	[HttpPut]
	[Authorize]
	public async Task<ActionResult> Edit(User user)
	{
		if (await _userRepository.GetByIdAsync(user.Id, true) is null)
			return NotFound("Usuário não localizado");

		await _userRepository.UpdateAsync(user);

		return Ok(user);
	}

	// DELETE: UserController/Delete/5
	[HttpDelete]
	[Authorize]
	public async Task<ActionResult> Delete(Guid id)
	{
		if (await _userRepository.GetByIdAsync(id) is null)
			return NotFound("Usuário não localizado");

		await _userRepository.DeleteAsync(id);
		return Ok();
	}

}
