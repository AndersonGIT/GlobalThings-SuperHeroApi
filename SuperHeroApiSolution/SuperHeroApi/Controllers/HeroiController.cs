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
        public IHttpActionResult Get()
        {
            List<Heroi> herois = null;
            try
            {
                herois = new HeroiService().ListarHerois();
                if(herois?.Count > 0)
                {
                    return Ok(herois);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        // GET api/values/5
        [HttpGet]
        public IHttpActionResult Get(long idHeroi)
        {
            Heroi heroi = null;
            try
            {
                heroi = new HeroiService().ConsultarHeroi(idHeroi);
                
                if (heroi != null)
                {
                    return Ok(heroi);
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        // POST api/values
        [HttpPost]
        public IHttpActionResult Post([FromBody] Heroi heroi)
        {
            try
            {
                HeroiService heroiService = new HeroiService();
                
                Heroi heroiExistentePeloNome = heroiService.ProcurarHeroiPorNome(heroi.Nome);
                if(heroiExistentePeloNome == null)
                {
                    string validacaoHeroi = heroi.ValidarHeroi(Objetos.Enums.RotinaEmExecucao.Cadastro);
                    if (string.IsNullOrWhiteSpace(validacaoHeroi))
                    {
                        long idHeroiCriado = heroiService.CadastrarHeroi(heroi);

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
                else
                {
                    return BadRequest("Já existe um herói com este nome");
                }
            }
            catch (Exception exception) {
                return InternalServerError(exception);
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
