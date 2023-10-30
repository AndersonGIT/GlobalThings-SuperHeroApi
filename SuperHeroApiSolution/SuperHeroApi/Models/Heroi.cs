using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using static SuperHeroApi.Objetos.Enums;

namespace SuperHeroApi.Models
{
    public class Heroi
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public long IdCategoria { get; set; }

        public string ValidarHeroi(RotinaEmExecucao rotinaEmExecucao)
        {
            StringBuilder retornoValidacao = new StringBuilder();

            if (rotinaEmExecucao == RotinaEmExecucao.Alteracao)
            {
                if (Id <= 0)
                {
                    retornoValidacao.AppendLine("ID de Herói está incorreto");
                }
            }

            if (string.IsNullOrWhiteSpace(Nome))
            {
                retornoValidacao.AppendLine("Nome de Herói está incorreto");
            }

            if (IdCategoria <= 0)
            {
                retornoValidacao.AppendLine("Id de categoria de Herói está incorreto");
            }

            return retornoValidacao.ToString();
        }
    }
}