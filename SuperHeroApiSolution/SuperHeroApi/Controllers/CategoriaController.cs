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
        public IEnumerable<Categoria> Get()
        {
            List<Categoria> categorias = new CategoriaService().ListarCategorias();

            return categorias;
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            Categoria categoria = new CategoriaService().ConsultarCategoria(id);
            if (categoria != null)
                return Ok(categoria);
            else
                return NotFound();
        }

        // POST api/<controller>
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
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}