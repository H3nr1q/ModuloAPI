using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ModuloAPI.Context;
using ModuloAPI.Entities;

namespace ModuloAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContatoController : ControllerBase
    {
        private readonly AgendaContext _context;
        public ContatoController(AgendaContext context)
        {
            this._context = context;
        }

        [HttpPost]
        public IActionResult Create(Contato contato)
        {
            this._context.Add(contato);
            this._context.SaveChanges();
            return CreatedAtAction(nameof(ObterContatoId), new {id = contato.Id}, contato);
        }

        [HttpGet("ObterPorId")]
        public IActionResult ObterContatoId(int id)
        {
            var contato = this._context.Contatos.Find(id);
            if(contato == null) 
                return NotFound();
            
            return Ok(contato);
        }

        [HttpPut("id")]
        public IActionResult AtualizaContatoId(int id, Contato contato)
        {
            var contatoBanco = this._context.Contatos.Find(id);
            if(contato == null) 
                return NotFound();

            contatoBanco.Nome = contato.Nome;
            contatoBanco.Telefone = contato.Telefone;                
            contatoBanco.Ativo = contato.Ativo;                

            this._context.Update(contatoBanco);
            this._context.SaveChanges();

            return Ok(contatoBanco);
        }

        [HttpDelete("id")]
        public IActionResult DeleteContatoID(int id)
        {
            var contatoBanco = this._context.Contatos.Find(id);
            if(contatoBanco == null) 
                return NotFound();

            this._context.Remove(contatoBanco);
            this._context.SaveChanges();

            return NoContent();
        }

        [HttpGet("ObterPorNome")]
        public IActionResult ObterPorNome(string nome)
        {
            var contatos = this._context.Contatos.Where( x => x.Nome.Contains(nome));
            return Ok(contatos);
        }
    }
}