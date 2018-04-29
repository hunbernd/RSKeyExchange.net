using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RSKeyExchange.RetroShareAPI;

namespace RSKeyExchange.Pages
{
    public class KeysModel : PageModel
    {
		private RetroShareHTTPClient _client;
		public IList<Location> Peers { get; private set; }

		public KeysModel(RetroShareHTTPClient rsapi)
		{
			_client = rsapi;
		}

		public async Task OnGetAsync()
        {
			Peers = await _client.GetActivePeers();
        }
    }
}