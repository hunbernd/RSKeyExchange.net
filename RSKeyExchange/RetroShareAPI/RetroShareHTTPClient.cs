using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RSKeyExchange.RetroShareAPI
{
	//https://www.hanselman.com/blog/HttpClientFactoryForTypedHttpClientInstancesInASPNETCore21.aspx
	public class RetroShareHTTPClient
    {
		private HttpClient _client;
		private string URIADD = "api/v2";

		public RetroShareHTTPClient(IConfiguration config)
		{
			_client = new HttpClient();
			_client.BaseAddress = new Uri("http://localhost:1024");
			_client.DefaultRequestHeaders.Add("Accept", "application/json");
		}

		public async Task<IList<Peer>> GetPeers()
		{
			try {
				Uri peersUrl = new Uri($"/{URIADD}/peers", UriKind.Relative);
				var res = await _client.GetAsync(peersUrl);
				res.EnsureSuccessStatusCode();
				var resp = await res.Content.ReadAsJsonAsync<Response<IList<Peer>>>();
				if(resp.data == null)
					throw new Exception("Error accessing to RetroShare API: " + resp.debug_msg);
				else
					return resp.data;
			} catch(HttpRequestException) {
				throw;
			}
		}

		//public async Task<string> GetCert(string peerid)
		//{
		//	try {
		//		var episodesUrl = new Uri($"/v1/podcasts/shownum/episodes.json?api_key={_apiKey}", UriKind.Relative);
		//		_logger.LogWarning($"HttpClient: Loading {episodesUrl}");
		//		var res = await _client.GetAsync(episodesUrl);
		//		res.EnsureSuccessStatusCode();
		//		return await res.Content.ReadAsAsync<List<Show>>();
		//	} catch(HttpRequestException ex) {
		//		_logger.LogError($"An error occurred connecting to SimpleCast API {ex.ToString()}");
		//		throw;
		//	}
		//}

		public async Task<IList<Location>> GetActivePeers()
		{
			List<Location> locations = new List<Location>();
			DateTimeOffset timelimit = DateTimeOffset.Now - TimeSpan.FromDays(31);

			var peers = await GetPeers();
			foreach(Peer p in peers) {
				foreach(Location l in p.locations) {
					if(l.LastContact >= timelimit)
						if(l.groups != null)
							foreach(Group g in l.groups)
								if(g.group_name == "Turul") {
									locations.Add(l);
									break;
							}
				}
			}

			return locations;
		}
	}
}
