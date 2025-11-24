using EventosShared.Interfaces;
using EventosShared.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventosEvento.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class CheckInController(IRepository<CheckIn> repository) : ControllerBase
{
	private readonly IRepository<CheckIn> _repository = repository;

	[HttpGet("getAll")]
	public async Task<ActionResult> GetAll()
	{
		var eventos = await _repository.GetAllAsync();
		return Ok(eventos);
	}

	[HttpGet]
	public async Task<ActionResult> Get(Guid id)
	{
		var evento = await _repository.GetByIdAsync(id);
		if (evento is null)
			return NotFound("CheckIn não localizado");
		return Ok(evento);
	}

	[HttpPost]
	public async Task<ActionResult> Create(CheckIn checkIn)
	{
		await _repository.CreateAsync(checkIn);
		return CreatedAtAction(nameof(Get), new { id = checkIn.Id }, checkIn);
	}

	[HttpPut]
	public async Task<ActionResult> Edit(CheckIn checkIn)
	{
		if (await _repository.GetByIdAsync(checkIn.Id, true) is null)
			return NotFound("CheckIn não localizado");
		await _repository.UpdateAsync(checkIn);
		return NoContent();
	}

	[HttpDelete]
	public async Task<ActionResult> Delete(Guid id)
	{
		if (await _repository.GetByIdAsync(id, true) is null)
			return NotFound("CheckIn não localizado");
		await _repository.DeleteAsync(id);
		return NoContent();
	}
}
