using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RSKeyExchange
{

	//https://nodogmablog.bryanhogan.net/2017/10/httpcontent-readasasync-with-net-core-2/
	//TODO Remove when it becomes available in new .net core version
	public static class HttpContentExtensions
	{
		public static async Task<T> ReadAsJsonAsync<T>(this HttpContent content)
		{
			string json = await content.ReadAsStringAsync();
			T value = JsonConvert.DeserializeObject<T>(json);
			return value;
		}
	}
}
