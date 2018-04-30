using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RSKeyExchange.RetroShareAPI;

namespace RSKeyExchange.Pages
{
	public struct PeerInfo
	{
		public string name;
		public string location;
		public string type;
		public string certificate;
	}

    public class KeysModel : PageModel
    {
		private RetroShareHTTPClient _client;
		public IList<PeerInfo> Peers { get; private set; }

		public KeysModel(RetroShareHTTPClient rsapi)
		{
			_client = rsapi;
		}

		public async Task OnGetAsync()
        {
			Peers = new List<PeerInfo>();
			var locations = await _client.GetActivePeers();
			foreach(Location l in locations) {
				String cert = await _client.GetCert(l.peer_id);
				PeerInfo info = new PeerInfo {
					name = l.name,
					location = l.location,
					type = l.nodeType,
					certificate = cert
				};
				Peers.Add(info);
			}

        }
    }
}