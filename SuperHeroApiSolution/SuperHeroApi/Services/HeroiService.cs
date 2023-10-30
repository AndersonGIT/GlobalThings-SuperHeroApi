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
        public Heroi AlterarCategoria(Heroi heroi)
        {
            throw new NotImplementedException();
        }

        public long CadastrarHeroi(Heroi heroi)
        {
            throw new NotImplementedException();
        }

        public Heroi ConsultarHeroi(long idHeroi)
        {
            Heroi heroi = null;
            var heroiDataResult = (DataTable)GenericDatabase.ExecuteCommand(String.Format("Select ID, NOME, ID_CATEGORIA FROM Herois WHERE ID = {0}", idHeroi), System.Data.CommandType.Text, null, GenericDatabase.ExecutionType.ExecuteDataTable);

            if (heroiDataResult != null)
            {
                heroi = new Heroi
                {
                    Id = Convert.ToInt64(heroiDataResult.Rows[0]["ID"]),
                    Nome = heroiDataResult.Rows[0]["NOME"].ToString(),
                    IdCategoria = Convert.ToInt64(heroiDataResult.Rows[0]["ID_CATEGORIA"])
                };
            }

            return heroi;
        }

        public List<Heroi> ListarHerois()
        {
            List<Heroi> herois = null;
            var heroisDataResult = (DataTable)GenericDatabase.ExecuteCommand("Select ID, NOME, ID_CATEGORIA FROM Herois", System.Data.CommandType.Text, null, GenericDatabase.ExecutionType.ExecuteDataTable);

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

            return herois;
        }

        public List<Heroi> ListarHeroisPorCategoria(long idCategoria)
        {
            List<Heroi> herois = null;
            var heroisDataResult = (DataTable)GenericDatabase.ExecuteCommand(string.Format("Select ID, NOME, ID_CATEGORIA FROM Herois WHERE ID_CATEGORIA = {0}", idCategoria), System.Data.CommandType.Text, null, GenericDatabase.ExecutionType.ExecuteDataTable);

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

            return herois;
        }

        public bool RemoverHeroi(long idHeroi)
        {
            throw new NotImplementedException();
        }
    }
}