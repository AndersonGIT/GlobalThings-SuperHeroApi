using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperHeroApi.Models.Login
{
    public class Usuario
    {
        public int Id { get; set; }
        public string UsuarioLogin { get; set; } 
        public string UsuarioSenha { get; set; }
    }
}