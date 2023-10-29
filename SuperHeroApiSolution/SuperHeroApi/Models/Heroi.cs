using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperHeroApi.Models
{
    public class Heroi
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public long IdCategoria { get; set; }
    }
}