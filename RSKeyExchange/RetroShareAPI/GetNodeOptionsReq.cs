using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSKeyExchange.RetroShareAPI
{
    public class GetNodeOptionsReq
    {
		public string peer_id;
		public GetNodeOptionsReq(string peer_id) => this.peer_id = peer_id;
	}
}
