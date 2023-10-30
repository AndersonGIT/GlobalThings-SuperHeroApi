using DataLib;
using SuperHeroApi.Models;
using SuperHeroApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SuperHeroApi.Controllers
{
    public class HeroiController : ApiController
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<Heroi> Get()
        {
            List<Heroi> herois = new HeroiService().ListarHerois();
            return herois;
        }

        // GET api/values/5
        [HttpGet]
        public Heroi Get(int id)
        {
            Heroi heroi = new HeroiService().ConsultarHeroi(id);
            return heroi;
        }

        // POST api/values
        [HttpPost]
        public IHttpActionResult Post([FromBody] Heroi heroi)
        {
            long idHeroiCriado = new HeroiService().CadastrarHeroi(heroi);
            string validacaoHeroi = heroi.ValidarHeroi(Objetos.Enums.RotinaEmExecucao.Cadastro);
            if (string.IsNullOrWhiteSpace(validacaoHeroi))
            {
                if (idHeroiCriado > 0)
                {
                    return Ok(idHeroiCriado);
                }
                else
                {
                    return BadRequest("Herói não criado");
                }
            }
            else
            {
                return BadRequest(validacaoHeroi);
            }
        }

        // PUT api/values/5
        [HttpPut]
        public IHttpActionResult Put([FromBody] Heroi heroi)
        {
            try
            {
                Heroi heroiAtualizado = null;
                string validacaoHeroi = heroi.ValidarHeroi(Objetos.Enums.RotinaEmExecucao.Alteracao);
                if (string.IsNullOrWhiteSpace(validacaoHeroi))
                {
                    heroiAtualizado = new HeroiService().AlterarHeroi(heroi);
                }

                if (heroiAtualizado != null)
                {
                    return Ok(heroiAtualizado);
                }
                else
                {
                    return BadRequest(validacaoHeroi);
                }

            }catch(Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        // DELETE api/values/5
        [HttpDelete]
        public IHttpActionResult Delete(long idHeroi)
        {
            try
            {                
                new HeroiService().RemoverHeroi(idHeroi);

                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
