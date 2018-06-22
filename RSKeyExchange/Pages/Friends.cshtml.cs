using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using MaxMind.GeoIP2.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RSKeyExchange.RetroShareAPI;

namespace RSKeyExchange.Pages
{
	public struct PeerLocation
	{
		public string name;
		public string location;
		public string type;
		public string avatar;
		public string address;
		public string state;
		public string lat;
		public string lon;
	}

	public class FriendsModel : PageModel
    {
		private RetroShareHTTPClient _client;
		private GeoIp _geoip;
		public IList<PeerLocation> Peers { get; private set; }

		public FriendsModel(RetroShareHTTPClient rsapi, GeoIp geoIp)
		{
			_client = rsapi;
			_geoip = geoIp;
		}

		public async Task OnGetAsync()
		{
			Peers = new List<PeerLocation>();
			var _peers = await _client.GetPeers();
			foreach(Peer p in _peers) {
				foreach(Location l in p.locations) {
					if(l.is_hidden_node) continue;
					GetNodeOptionsResp resp = await _client.GetNodeOptions(l.peer_id);
					CityResponse city = _geoip.GetCity(resp.ext_address);
					if(city == null) continue;
					PeerLocation info = new PeerLocation {
						name = l.name,
						location = l.location,
						type = l.nodeType,
						avatar = l.avatar_address,
						state = resp.status_message,
						address = resp.ext_address,
						lat = city.Location.Latitude?.ToString(CultureInfo.InvariantCulture),
						lon = city.Location.Longitude?.ToString(CultureInfo.InvariantCulture)
					};
					Peers.Add(info);
				}
			}
		}
	}
}