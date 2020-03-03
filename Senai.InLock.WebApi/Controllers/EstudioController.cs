using Microsoft.AspNetCore.Mvc;
using Senai.InLock.WebApi.Domains;
using Senai.InLock.WebApi.Interfaces;
using Senai.InLock.WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.InLock.WebApi.Controllers
{
    [Produces("application/jason")]
    [Route("api/[controller]")]
    [ApiController]

    public class EstudioController : ControllerBase
    {
        private IEstudioRepository _EstudioRepository { get; set; }

        public EstudioController() 
        {
            _EstudioRepository = new EstudioRepository();
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_EstudioRepository.Listar());
        }

        [HttpPost]
        public IActionResult Post(EstudioDomain novoEstudio)
        {
            if (novoEstudio.NomeEstudio == null)
            {
                return BadRequest("De um nome ao estúdio!!");
            }
            _EstudioRepository.Cadastrar(novoEstudio);

            return Created("http:localhost:5000/api/Estudio", novoEstudio);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            EstudioDomain estudioBuscado = _EstudioRepository.BuscarPorId(id);

            if (estudioBuscado != null)
            {
                return Ok(estudioBuscado);
            }
            return NotFound("Nenhum Estudio Encontrado :(");
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, EstudioDomain estudioAtualizado)
        {
            EstudioDomain estudioBuscado = _EstudioRepository.BuscarPorId(id);

            if (estudioBuscado != null)
            {
                try
                {
                    _EstudioRepository.Atualizar(id, estudioAtualizado);

                    return NoContent();
                }
                catch (Exception erro)
                {
                    return BadRequest(erro);
                }
            }
            return NotFound
                (
                new
                {
                    mensagem = "Estudio Não Encontrado",
                    erro = true
                });
        }
        public IActionResult Delete(int id)
        {
            EstudioDomain estudioBuscado = _EstudioRepository.BuscarPorId(id);

            if (estudioBuscado != null)
            {
                _EstudioRepository.Deletar(id);

                return Ok($"O estudio {id} foi deletado com sucesso! :)");
            }

            return NotFound("Nenhum estudio encontrado");
        }
        

    }
}
