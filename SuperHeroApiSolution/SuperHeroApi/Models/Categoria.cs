using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using static SuperHeroApi.Objetos.Enums;

namespace SuperHeroApi.Models
{
    public class Categoria
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string ValidarCategoria(RotinaEmExecucao rotinaEmExecucao)
        {
            StringBuilder retornoValidacao = new StringBuilder();

            if (rotinaEmExecucao == RotinaEmExecucao.Alteracao)
            {
                if (Id <= 0)
                {
                    retornoValidacao.AppendLine("ID da categoria está incorreto");
                }
            }

            if (string.IsNullOrWhiteSpace(Nome))
            {
                retornoValidacao.AppendLine("Nome da categoria está incorreto");
            }

            return retornoValidacao.ToString();
        }

    }
}