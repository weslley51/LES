using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LES.Utils
{
	/// <summary>
	/// Classe auxiliar para realizar requisições HTTP
	/// </summary>
	/// <typeparam name="T">Tipo da entidade que será retornada</typeparam>
	public class HttpHelper<T>
	{

		/// <summary>
		/// Realiza uma requisição GET
		/// </summary>
		/// <param name="Url">Url a qual será realizada o POST</param>
		/// <param name="Parameters">Parâmetros que serão enviados na requisição</param>
		/// <param name="Headers">Headers que deverão ser adicionaos a requisição</param>
		/// <returns>Objeto Result contendo o resultado da requisição sendo ele um erro ou a classe</returns>
		public async Task<Result<T>> Get(string Url, KeyValuePair<string, string>[] Parameters = null, KeyValuePair<string, string>[] Headers = null)
		{
			using (var Client = new HttpClient())
			{
				if (Parameters != null)
				{
					Url += "?";

					for (var i = 0; i < Parameters.Length; i++)
					{
						Url += $"{Parameters[i].Key}={Parameters[i].Value}";

						if (i + 1 != Parameters.Length)
							Url += "&";
					}
				}

				if (Headers != null && Headers.Length > 0)
				{
					foreach (var Header in Headers)
						Client.DefaultRequestHeaders.Add(Header.Key, Header.Value);
				}

				using (var Response = await Client.GetAsync(Url))
				{
					if (!Response.IsSuccessStatusCode)
					{
						var ErrorMessage = $"Erro ao realizar a requisição para a Url: {Url} " +
							  $"{await Response.Content.ReadAsStringAsync()}";

						return new Result<T>(new Message(ErrorMessage));
					}
					else
					{
						var JsonResult = await Response.Content.ReadAsStringAsync();
						var Result = JsonConvert.DeserializeObject<T>(JsonResult);
						return new Result<T>(Result);
					}
				}
			}
		}

		/// <summary>
		/// Realiza uma requisição POST
		/// </summary>
		/// <param name="Url">Url a qual será realizada o POST</param>
		/// <param name="Parameters">Parâmetros que serão enviados na requisição</param>
		/// <param name="Headers">Headers que deverão ser adicionaos a requisição</param>
		/// <returns>Objeto Result contendo o resultado da requisição sendo ele um erro ou a classe</returns>
		public async Task<Result<T>> Post(string Url, KeyValuePair<string, string>[] Parameters, KeyValuePair<string, string>[] Headers = null, bool IsUrl = false)
		{
			using (var Client = new HttpClient())
			{
				if (IsUrl)
				{
					Url += "?";

					for (var i = 0; i < Parameters.Length; i++)
					{
						Url += $"{Parameters[i].Key}={Parameters[i].Value}";

						if (i + 1 != Parameters.Length)
							Url += "&";
					}
				}

				if (Headers != null && Headers.Length > 0)
				{
					foreach (var Header in Headers)
						Client.DefaultRequestHeaders.Add(Header.Key, Header.Value);
				}

				using (var Response = await Client.PostAsync(Url, IsUrl ? null : new FormUrlEncodedContent(Parameters)))
				{
					if (!Response.IsSuccessStatusCode)
					{
						var ErrorMessage = $"Erro ao realizar a requisição para a Url: {Url} " +
							  $"{await Response.Content.ReadAsStringAsync()}";

						return new Result<T>(new Message(ErrorMessage));
					}
					else
					{
						var JsonResult = await Response.Content.ReadAsStringAsync();
						var Result = JsonConvert.DeserializeObject<Result<T>>(JsonResult);
						return Result;
					}
				}
			}
		}

		/// <summary>
		/// Realiza uma requisição POST
		/// </summary>
		/// <param name="Url">Url a qual será realizada o POST</param>
		/// <param name="Entity">Classe que será enviada na requisição</param>
		/// <param name="Headers">Headers que deverão ser adicionaos a requisição</param>
		/// <returns>Objeto Result contendo o resultado da requisição sendo ele um erro ou a classe</returns>
		public async Task<Result<T>> Post(string Url, T Entity, KeyValuePair<string, string>[] Headers = null)
		{
			using (var Client = new HttpClient())
			{
				var SerializedObject = JsonConvert.SerializeObject(Entity);
				var Content = new StringContent(SerializedObject, Encoding.UTF8, "application/json");

				if (Headers != null && Headers.Length > 0)
				{
					foreach (var Header in Headers)
						Client.DefaultRequestHeaders.Add(Header.Key, Header.Value);
				}

				using (var Response = await Client.PostAsync(Url, Content))
				{
					if (!Response.IsSuccessStatusCode)
					{
						var ErrorMessage = $"Erro ao realizar a requisição para a Url: {Url} " +
							  $"{await Response.Content.ReadAsStringAsync()}";

						return new Result<T>(new Message(ErrorMessage));
					}
					else
					{
						var JsonResult = await Response.Content.ReadAsStringAsync();
						var Result = JsonConvert.DeserializeObject<Result<T>>(JsonResult);
						return Result;
					}
				}
			}
		}
	}
}
