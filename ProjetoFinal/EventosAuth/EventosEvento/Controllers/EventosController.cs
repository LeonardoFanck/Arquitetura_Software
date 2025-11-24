using EventosShared.Interfaces;
using EventosShared.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventosEvento.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class EventosController(IRepository<Evento> repository) : ControllerBase
{
	private readonly IRepository<Evento> _repository = repository;

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
			return NotFound("Evento não localizado");
		return Ok(evento);
	}

	[HttpPost]
	public async Task<ActionResult> Create(Evento evento)
	{
		await _repository.CreateAsync(evento);
		return CreatedAtAction(nameof(Get), new { id = evento.Id }, evento);
	}

	[HttpPut]
	public async Task<ActionResult> Edit(Evento evento)
	{
		if (await _repository.GetByIdAsync(evento.Id, true) is null)
			return NotFound("Evento não localizado");
		await _repository.UpdateAsync(evento);
		return NoContent();
	}

	[HttpDelete]
	public async Task<ActionResult> Delete(Guid id)
	{
		if (await _repository.GetByIdAsync(id, true) is null)
			return NotFound("Evento não localizado");
		await _repository.DeleteAsync(id);
		return NoContent();
	}
}
