using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LES.Utils
{
	public class MD5Manipulator
	{
		private MD5 MD5Technology { get; set; }

		public MD5Manipulator()
		{
			MD5Technology = MD5.Create();
		}

		public string Generate (string OriginalText)
		{
			if (string.IsNullOrWhiteSpace(OriginalText))
				return null;

			var TextBytes = Encoding.ASCII.GetBytes(OriginalText);
			var Hash = MD5Technology.ComputeHash(TextBytes);
			var EncryptedText = new StringBuilder();

			for (int i = 0; i < Hash.Length; i++)
				EncryptedText.Append(Hash[i].ToString("X2"));

			return EncryptedText.ToString();
		}
	}
}
