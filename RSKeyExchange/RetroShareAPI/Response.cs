using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSKeyExchange.RetroShareAPI
{
	public class Response<DataType>
	{
		public Response() { }

		public string returncode;
		public string debug_msg;
		public DataType data;
		public int? statetoken = null;

		public override string ToString()
		{
			return $"code: {returncode}, debugmsg: {debug_msg}, data: {data}";
		}
	}
}
