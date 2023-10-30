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
        public IEnumerable<Heroi> Get()
        {
            List<Heroi> herois = new HeroiService().ListarHerois();
            return herois;
        }

        // GET api/values/5
        public Heroi Get(int id)
        {
            Heroi heroi = new HeroiService().ConsultarHeroi(id);
            return heroi;
        }

        // POST api/values
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
        public IHttpActionResult Put([FromBody] Heroi heroi)
        {
            Heroi heroiAtualizado = null;
            string validacaoHeroi = heroi.ValidarHeroi(Objetos.Enums.RotinaEmExecucao.Alteracao);
            if (string.IsNullOrWhiteSpace(validacaoHeroi))
            {
                heroiAtualizado = new HeroiService().AlterarHeroi(heroi);                 
            }

            if(heroiAtualizado != null)
            {
                return Ok(heroiAtualizado);
            }
            else
            {
                return BadRequest(validacaoHeroi);
            }             
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
