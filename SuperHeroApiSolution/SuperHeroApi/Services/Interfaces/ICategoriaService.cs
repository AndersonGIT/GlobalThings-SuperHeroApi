using SuperHeroApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperHeroApi.Services.Interfaces
{
    internal interface ICategoriaService
    {
        List<Categoria> ListarCategorias();
        Categoria ConsultarCategoria(long idCategoria);
        Categoria AlterarCategoria(Categoria categoria);
        long CadastrarCategoria(Categoria categoria);
        bool RemoverCategoria(long idCategoria);
    }
}