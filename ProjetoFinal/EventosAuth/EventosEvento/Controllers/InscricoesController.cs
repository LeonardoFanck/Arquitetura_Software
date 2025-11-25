using EventosShared.Interfaces;
using EventosShared.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventosEvento.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class InscricoesController(IRepository<Inscricao> repository) : ControllerBase
{
	private readonly IRepository<Inscricao> _repository = repository;

	[HttpGet("getAll")]
	public async Task<ActionResult> GetAll()
	{
		var eventos = await _repository.GetAllAsync();
		return Ok(eventos);
	}

	[HttpGet("getAllByUser")]
	public async Task<ActionResult> GetAllByUser(Guid id)
	{
		var eventos = await _repository.GetAllByExpression(e => e.UserId == id);
		return Ok(eventos);
	}

	[HttpGet("getAllByEvent")]
	public async Task<ActionResult> GetAllByEvent(Guid id)
	{
		var eventos = await _repository.GetAllByExpression(e => e.EventoId == id);
		return Ok(eventos);
	}

	[HttpGet]
	public async Task<ActionResult> Get(Guid id)
	{
		var evento = await _repository.GetByIdAsync(id);
		if (evento is null)
			return NotFound("Inscrição não localizada");
		return Ok(evento);
	}

	[HttpPost]
	public async Task<ActionResult> Create(Inscricao inscricao)
	{
		await _repository.CreateAsync(inscricao);
		return CreatedAtAction(nameof(Get), new { id = inscricao.Id }, inscricao);
	}

	[HttpPut]
	public async Task<ActionResult> Edit(Inscricao inscricao)
	{
		if (await _repository.GetByIdAsync(inscricao.Id, true) is null)
			return NotFound("Inscrição não localizada");
		await _repository.UpdateAsync(inscricao);
		return NoContent();
	}

	[HttpDelete]
	public async Task<ActionResult> Delete(Guid id)
	{
		if (await _repository.GetByIdAsync(id, true) is null)
			return NotFound("Inscrição não localizada");
		await _repository.DeleteAsync(id);
		return NoContent();
	}
}
