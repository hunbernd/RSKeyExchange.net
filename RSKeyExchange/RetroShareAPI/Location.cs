using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSKeyExchange.RetroShareAPI
{
	public class Location
	{
		public Location()
		{
		}

		public string avatar_address;
		public string chat_id;
		public string custom_state_string;
		public IList<Group> groups;
		public bool is_hidden_node;
		public bool is_online;
		public long last_contact;
		public string location;
		public string name;
		public string nodeType;
		public string peer_id;
		public string pgp_id;
		public string state_string;
		public double unread_msgs;

		public DateTimeOffset LastContact
		{
			get
			{
				return DateTimeOffset.FromUnixTimeSeconds(last_contact);
			}
		}
	}
}
