using Microsoft.AspNetCore.Mvc;
using ProdutosCodeFirst.Models;
using ProdutosCodeFirst.Services;

namespace ProdutosCodeFirst.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly ProdutoService _service;

    public ProdutosController(ProdutoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var produtos = await _service.ObterTodosAsync();
        return Ok(produtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPorId(int id)
    {
        var produto = await _service.ObterPorIdAsync(id);
        if (produto == null) return NotFound("Produto não encontrado.");

        return Ok(produto);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Produto produto)
    {
        try
        {
            await _service.AdicionarAsync(produto);
            return CreatedAtAction(nameof(GetPorId), new { id = produto.Id }, produto);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Produto produto)
    {
        var atualizado = await _service.AtualizarAsync(id, produto);
        if (!atualizado) return NotFound("Produto não encontrado.");

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deletado = await _service.DeletarAsync(id);
        if (!deletado) return NotFound("Produto não encontrado.");

        return NoContent();
    }
}