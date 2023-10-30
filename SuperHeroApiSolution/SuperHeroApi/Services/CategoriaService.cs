using DataLib;
using SuperHeroApi.Models;
using SuperHeroApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace SuperHeroApi.Services
{
    public class CategoriaService : ICategoriaService
    {
        public Categoria AlterarCategoria(Categoria categoria)
        {
            throw new NotImplementedException();
        }

        public long CadastrarCategoria(Categoria categoria)
        {
            throw new NotImplementedException();
        }

        public Categoria ConsultarCategoria(long idCategoria)
        {
            Categoria categoria = null;
            var categoriasDataResult = (DataTable)GenericDatabase.ExecuteCommand(String.Format("Select * FROM Categorias WHERE ID = {0}", idCategoria), System.Data.CommandType.Text, null, GenericDatabase.ExecutionType.ExecuteDataTable);

            if (categoriasDataResult != null)
            {
                categoria = new Categoria
                {
                    Id = Convert.ToInt64(categoriasDataResult.Rows[0]["ID"]),
                    Nome = categoriasDataResult.Rows[0]["NOME"].ToString()
                };
            }

            return categoria;
        }

        public List<Categoria> ListarCategorias()
        {
            List<Categoria> categorias = new List<Categoria>();
            var categoriasDataResult = (DataTable)GenericDatabase.ExecuteCommand("Select * FROM Categorias", System.Data.CommandType.Text, null, GenericDatabase.ExecutionType.ExecuteDataTable);

            if(categoriasDataResult != null)
            {
                foreach (DataRow categoriaRow in categoriasDataResult.Rows)
                {
                    categorias.Add(new Categoria
                    {
                        Id = Convert.ToInt64(categoriaRow["ID"]),
                        Nome = categoriaRow["NOME"].ToString()
                    });
                }
            }

            return categorias;
        }

        public bool RemoverCategoria(long idCategoria)
        {
            throw new NotImplementedException();
        }
    }
}