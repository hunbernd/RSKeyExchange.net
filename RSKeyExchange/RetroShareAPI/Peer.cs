using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSKeyExchange.RetroShareAPI
{
	public class Peer
	{
		public Peer()
		{
		}

		public string custom_state_string;
		public bool is_own;
		public long last_contact;
		public IList<Location> locations;
		public string name;
		public string pgp_id;
		public string state_string;

		public DateTimeOffset LastContact
		{
			get
			{
				return DateTimeOffset.FromUnixTimeSeconds(last_contact);
			}
		}
	}
}
