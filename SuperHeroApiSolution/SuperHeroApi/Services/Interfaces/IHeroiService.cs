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
        List<Heroi> ListarHeroisPorCategoria(Categoria categoria);
        Heroi ConsultarHeroi(long idHeroi);
        Heroi AlterarCategoria(Heroi heroi);
        long CadastrarHeroi(Heroi heroi);
        bool RemoverHeroi(long idHeroi);
    }
}