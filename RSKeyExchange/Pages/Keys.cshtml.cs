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
		public string avatar;
		public string address;
		public string state;
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
				GetNodeOptionsResp resp = await _client.GetNodeOptions(l.peer_id);
				PeerInfo info = new PeerInfo {
					name = l.name,
					location = l.location,
					type = l.nodeType,
					certificate = resp.certificate,
					avatar = l.avatar_address,
					state = resp.status_message,
					address = resp.ext_address
				};
				Peers.Add(info);
			}

        }
    }
}