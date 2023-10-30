using DataLib;
using SuperHeroApi.Models;
using SuperHeroApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SuperHeroApi.Services
{
    public class HeroiService : IHeroiService
    {
        public Heroi AlterarHeroi(Heroi heroi)
        {
            Heroi heroiRetorno = null;
            try
            {
                GenericDatabase.ExecuteCommand(String.Format("UPDATE HEROIS SET (NOME = '{0}', ID_CATEGORIA = {1}) WHERE ID = {3}", heroi.Nome, heroi.IdCategoria, heroi.Id), CommandType.Text, null, GenericDatabase.ExecutionType.ExecuteNonQuery);
                heroiRetorno = this.ConsultarHeroi(heroi.Id);
            }
            catch (Exception exception)
            {
                throw new Exception("Ocorreu um erro ao alterar o herói. Exception: " + exception.Message);
            }
            return heroiRetorno;
        }

        public long CadastrarHeroi(Heroi heroi)
        {
            long idendityRegistrado = -1;
            try
            {
                var idHeroiCadastrado = GenericDatabase.ExecuteCommand(String.Format("INSERT INTO HEROIS (NOME, ID_CATEGORIA) VALUES ('{0}', {1}) SELECT @@IDENTITY", heroi.Nome, heroi.IdCategoria), CommandType.Text, null, GenericDatabase.ExecutionType.ExecuteScalar);

                if (idHeroiCadastrado != null)
                {
                    idendityRegistrado = Convert.ToInt64(idHeroiCadastrado);
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Ocorreu um erro ao cadastrar o herói. Exception: " + exception.Message);
            }

            return idendityRegistrado;
        }

        public Heroi ConsultarHeroi(long idHeroi)
        {
            Heroi heroi = null;
            try
            {
                var heroiDataResult = (DataTable)GenericDatabase.ExecuteCommand(String.Format("Select ID, NOME, ID_CATEGORIA FROM Herois WHERE ID = {0}", idHeroi), CommandType.Text, null, GenericDatabase.ExecutionType.ExecuteDataTable);

                if (heroiDataResult != null)
                {
                    heroi = new Heroi
                    {
                        Id = Convert.ToInt64(heroiDataResult.Rows[0]["ID"]),
                        Nome = heroiDataResult.Rows[0]["NOME"].ToString(),
                        IdCategoria = Convert.ToInt64(heroiDataResult.Rows[0]["ID_CATEGORIA"])
                    };
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Ocorreu um erro ao consultar o herói. Exception: " + exception.Message);
            }

            return heroi;
        }

        public List<Heroi> ListarHerois()
        {
            List<Heroi> herois = null;
            try
            {
                var heroisDataResult = (DataTable)GenericDatabase.ExecuteCommand("Select ID, NOME, ID_CATEGORIA FROM Herois", CommandType.Text, null, GenericDatabase.ExecutionType.ExecuteDataTable);

                if (heroisDataResult != null)
                {
                    herois = new List<Heroi>();
                    foreach (DataRow categoriaRow in heroisDataResult.Rows)
                    {
                        herois.Add(new Heroi
                        {
                            Id = Convert.ToInt64(categoriaRow["ID"]),
                            Nome = categoriaRow["NOME"].ToString(),
                            IdCategoria = Convert.ToInt64(categoriaRow["ID_CATEGORIA"])
                        });
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Ocorreu um erro ao listar os heróis. Exception: " + exception.Message);
            }

            return herois;
        }

        public List<Heroi> ListarHeroisPorCategoria(long idCategoria)
        {
            List<Heroi> herois = null;

            try
            {
                var heroisDataResult = (DataTable)GenericDatabase.ExecuteCommand(string.Format("Select ID, NOME, ID_CATEGORIA FROM Herois WHERE ID_CATEGORIA = {0}", idCategoria), System.Data.CommandType.Text, null, GenericDatabase.ExecutionType.ExecuteDataTable);

                if (heroisDataResult != null)
                {
                    herois = new List<Heroi>();
                    foreach (DataRow heroiRow in heroisDataResult.Rows)
                    {
                        herois.Add(new Heroi
                        {
                            Id = Convert.ToInt64(heroiRow["ID"]),
                            Nome = heroiRow["NOME"].ToString(),
                            IdCategoria = Convert.ToInt64(heroiRow["ID_CATEGORIA"])
                        });
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Ocorreu um erro ao listar os heróis por categoria. Exception: " + exception.Message);
            }

            return herois;
        }

        public bool RemoverHeroi(long idHeroi)
        {
            try
            {
                GenericDatabase.ExecuteCommand(String.Format("DELETE FROM HEROIS WHERE ID = {0}", idHeroi), CommandType.Text, null, GenericDatabase.ExecutionType.ExecuteNonQuery);
                return true;
            }catch(Exception exception)
            {
                throw new Exception("Problemas ao deletar herói. Exception: " + exception.Message);
            }
        }

        private Heroi ProcurarHeroiPorNome(string nomeHeroi)
        {
            Heroi heroi = null;
            try
            {
                var heroisDataResult = (DataTable)GenericDatabase.ExecuteCommand(String.Format("Select ID, NOME, ID_CATEGORIA FROM HEROIS WHERE NOME = {0}", nomeHeroi), System.Data.CommandType.Text, null, GenericDatabase.ExecutionType.ExecuteDataTable);

                if (heroisDataResult != null)
                {
                    if (heroisDataResult.Rows.Count > 0)
                    {
                        heroi = new Heroi
                        {
                            Id = Convert.ToInt64(heroisDataResult.Rows[0]["ID"]),
                            Nome = heroisDataResult.Rows[0]["NOME"].ToString(),
                            IdCategoria = Convert.ToInt64(heroisDataResult.Rows[0]["ID_CATEGORIA"])
                        };
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Problemas ao consultar o herói pelo nome. Exception: " + exception.Message);
            }

            return heroi;
        }
    }
}