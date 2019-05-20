﻿using LES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LES.Models
{
	public class Usuario : Dominio
	{
		public string Email { get; set; }
		public string Login { get; set; }
		public string Nome { get; set; }
		public string Senha { get; set; }
		public Perfil Perfil { get; set; }
		public Unidade Unidade { get; set; }
	}
}