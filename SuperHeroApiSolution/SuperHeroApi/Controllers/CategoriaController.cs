using DataLib;
using Microsoft.Ajax.Utilities;
using SuperHeroApi.Models;
using SuperHeroApi.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SuperHeroApi.Controllers
{
    [Authorize]
    public class CategoriaController : ApiController
    {
        // GET api/<controller>
        [HttpGet]
        [Route("api/categoria/listarCategorias")]
        public IEnumerable<Categoria> Get()
        {
            List<Categoria> categorias = new CategoriaService().ListarCategorias();

            return categorias;
        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("api/categoria/obterCategoria")]
        public IHttpActionResult Get(long idCategoria)
        {
            try
            {
                Categoria categoria = new CategoriaService().ConsultarCategoria(idCategoria);
                if (categoria != null)
                    return Ok(categoria);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST api/<controller>
        [HttpPost]
        [Route("api/categoria/inserirCategoria")]
        public IHttpActionResult Post([FromBody] Categoria categoria)
        {
            try
            {
                string validacaoCategoria = categoria.ValidarCategoria(Objetos.Enums.RotinaEmExecucao.Cadastro);
                if (string.IsNullOrWhiteSpace(validacaoCategoria))
                {
                    CategoriaService categoriaService = new CategoriaService();
                    Categoria categoriaPorNome = categoriaService.ProcurarCategoriaPorNome(categoria.Nome);
                    if (categoriaPorNome == null)
                    {
                        long idHCategoriaCriada = categoriaService.CadastrarCategoria(categoria);

                        if (idHCategoriaCriada > 0)
                        {
                            return Ok(idHCategoriaCriada);
                        }
                        else
                        {
                            return BadRequest("Categoria não criado");
                        }
                    }
                    else
                    {
                        return BadRequest("Categoria já existe.");
                    }
                }
                else
                {
                    return BadRequest(validacaoCategoria);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("api/categoria/atualizarCategoria")]
        public IHttpActionResult Put([FromBody] Categoria categoria)
        {
            try
            {
                Categoria categoriaAtualizada = null;
                string validacaoCategoria = categoria.ValidarCategoria(Objetos.Enums.RotinaEmExecucao.Alteracao);
                if (string.IsNullOrWhiteSpace(validacaoCategoria))
                {
                    categoriaAtualizada = new CategoriaService().AlterarCategoria(categoria);
                }

                if (categoriaAtualizada != null)
                {
                    return Ok(categoriaAtualizada);
                }
                else
                {
                    return BadRequest(validacaoCategoria);
                }
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        [Route("api/categoria/removerCategoria/")]
        public IHttpActionResult Delete(long idCategoria)
        {
            try
            {
                CategoriaService categoriaService = new CategoriaService();
                if (!categoriaService.ExisteHeroiAssociadoNaCategoria(idCategoria))
                {
                    categoriaService.RemoverCategoria(idCategoria);
                    return Ok();
                }
                else
                {
                    return BadRequest("Impossível remover categoria associada a um herói.");
                }
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}