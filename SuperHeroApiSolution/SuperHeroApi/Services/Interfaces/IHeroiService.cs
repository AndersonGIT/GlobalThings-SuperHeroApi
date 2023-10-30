using SuperHeroApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperHeroApi.Services.Interfaces
{
    internal interface IHeroiService
    {
        List<Heroi> ListarHerois();
        List<Heroi> ListarHeroisPorCategoria(long idCategoria);
        Heroi ConsultarHeroi(long idHeroi);
        Heroi AlterarHeroi(Heroi heroi);
        long CadastrarHeroi(Heroi heroi);
        bool RemoverHeroi(long idHeroi);
    }
}