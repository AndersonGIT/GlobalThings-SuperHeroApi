using DataLib;
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
    public class CategoriaController : ApiController
    {
        // GET api/<controller>
        [HttpGet]
        public IEnumerable<Categoria> Get()
        {
            List<Categoria> categorias = new CategoriaService().ListarCategorias();

            return categorias;
        }

        // GET api/<controller>/5
        [HttpGet]
        public IHttpActionResult Get(long idCategoria)
        {
            Categoria categoria = new CategoriaService().ConsultarCategoria(idCategoria);
            if (categoria != null)
                return Ok(categoria);
            else
                return NotFound();
        }

        // POST api/<controller>
        [HttpPost]
        public IHttpActionResult Post([FromBody] Categoria categoria)
        {
            long idHCategoriaCriada = new CategoriaService().CadastrarCategoria(categoria);
            string validacaoCategoria = categoria.ValidarCategoria(Objetos.Enums.RotinaEmExecucao.Cadastro);
            if (string.IsNullOrWhiteSpace(validacaoCategoria))
            {
                if (idHCategoriaCriada > 0)
                {
                    return Ok(idHCategoriaCriada);
                }
                else
                {
                    return BadRequest("Herói não criado");
                }
            }
            else
            {
                return BadRequest(validacaoCategoria);
            }
        }

        // PUT api/<controller>/5
        [HttpPut]
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
        public IHttpActionResult Delete(long idCategoria)
        {
            try
            {
                CategoriaService categoriaService= new CategoriaService();
                if(!categoriaService.ExisteHeroiAssociadoNaCategoria(idCategoria)) { 
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