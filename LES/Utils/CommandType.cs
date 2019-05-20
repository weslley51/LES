using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LES.Utils
{
	[AttributeUsage(AttributeTargets.Class)]
	public class CommandType : Attribute
	{
		public List<Command> Commands { get; set; }

		public CommandType(params Command[] Commands)
		{
			this.Commands = Commands.ToList();
		}
	}
}
