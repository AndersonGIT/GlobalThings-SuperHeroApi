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
            Categoria categoriaRetorno = null;
            try
            {
                GenericDatabase.ExecuteCommand(String.Format("UPDATE CATEGORIAS SET (NOME = '{0}') WHERE ID = {1}", categoria.Nome), CommandType.Text, null, GenericDatabase.ExecutionType.ExecuteNonQuery);
                categoriaRetorno = this.ConsultarCategoria(categoria.Id);
            }
            catch (Exception exception)
            {
                throw new Exception("Ocorreu um erro ao alterar a categoria. Exception: " + exception.Message);
            }
            return categoriaRetorno;
        }

        public long CadastrarCategoria(Categoria categoria)
        {
            long idendityRegistrado = -1;

            try
            {
                Categoria categoriaExistente = ProcurarCategoriaPorNome(categoria.Nome);

                if (categoriaExistente == null)
                {
                    var idCategoriaCadastrada = GenericDatabase.ExecuteCommand(String.Format("INSERT INTO CATEGORIAS (NOME) VALUES ('{0}') SELECT @@IDENTITY", categoria.Nome), CommandType.Text, null, GenericDatabase.ExecutionType.ExecuteScalar);

                    if (idCategoriaCadastrada != null)
                    {
                        idendityRegistrado = Convert.ToInt64(idCategoriaCadastrada);
                    }
                }
                else
                {
                    throw new Exception("Já existe uma categoria com este nome. Exception: ");
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Ocorreu um erro ao cadastrar a categoria. Exception: " + exception.Message);
            }

            return idendityRegistrado;
        }

        public Categoria ConsultarCategoria(long idCategoria)
        {
            Categoria categoria = null;
            var categoriasDataResult = (DataTable)GenericDatabase.ExecuteCommand(String.Format("Select * FROM Categorias WHERE ID = {0}", idCategoria), System.Data.CommandType.Text, null, GenericDatabase.ExecutionType.ExecuteDataTable);

            if (categoriasDataResult != null)
            {
                if (categoriasDataResult.Rows.Count > 0)
                {
                    categoria = new Categoria
                    {
                        Id = Convert.ToInt64(categoriasDataResult.Rows[0]["ID"]),
                        Nome = categoriasDataResult.Rows[0]["NOME"].ToString()
                    };
                }
            }

            return categoria;
        }

        public List<Categoria> ListarCategorias()
        {
            List<Categoria> categorias = new List<Categoria>();
            var categoriasDataResult = (DataTable)GenericDatabase.ExecuteCommand("Select * FROM Categorias", CommandType.Text, null, GenericDatabase.ExecutionType.ExecuteDataTable);

            if (categoriasDataResult != null)
            {
                if (categoriasDataResult.Rows.Count > 0)
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
            }

            return categorias;
        }

        public bool RemoverCategoria(long idCategoria)
        {
            try
            {
                GenericDatabase.ExecuteCommand(String.Format("DELETE FROM CATEGORIAS WHERE ID = {0}", idCategoria), CommandType.Text, null, GenericDatabase.ExecutionType.ExecuteNonQuery);
                return true;
            }
            catch (Exception exception)
            {
                throw new Exception("Problemas ao deletar categoria. Exception: " + exception.Message);
            }
        }

        public bool ExisteHeroiAssociadoNaCategoria(long idCategoria)
        {
            try
            {
                var qtdResultadoConsulta = GenericDatabase.ExecuteCommand(String.Format("SELECT COUNT(ID) FROM CATEGORIAS WHERE ID = {0}", idCategoria), CommandType.Text, null, GenericDatabase.ExecutionType.ExecuteScalar);
                long valorQuantidade = -1;
                if (qtdResultadoConsulta != null)
                {
                    valorQuantidade = Convert.ToInt64(qtdResultadoConsulta);
                }

                if (valorQuantidade > 0)
                    return true;
                else return false;
            }
            catch (Exception exception)
            {
                throw new Exception("Problemas ao deletar categoria. Exception: " + exception.Message);
            }
        }

        private Categoria ProcurarCategoriaPorNome(string nomeCategoria)
        {
            Categoria categoria = null;
            var categoriasDataResult = (DataTable)GenericDatabase.ExecuteCommand(String.Format("Select ID, NOME FROM Categorias WHERE NOME = '{0}'", nomeCategoria), System.Data.CommandType.Text, null, GenericDatabase.ExecutionType.ExecuteDataTable);

            if (categoriasDataResult != null)
            {
                if (categoriasDataResult.Rows.Count > 0)
                {
                    categoria = new Categoria
                    {
                        Id = Convert.ToInt64(categoriasDataResult.Rows[0]["ID"]),
                        Nome = categoriasDataResult.Rows[0]["NOME"].ToString()
                    };
                }
            }

            return categoria;
        }
    }
}