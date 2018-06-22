using MaxMind.GeoIP2;
using MaxMind.GeoIP2.Responses;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RSKeyExchange
{
    public class GeoIp : IDisposable
    {
		private DatabaseReader reader;

		public GeoIp(IConfiguration config)
		{
			reader = new DatabaseReader("GeoLite2-City.mmdb");
		}

		public void Dispose()
		{
			reader.Dispose();
		}

		public CityResponse GetCity(string IP)
		{
			IPAddress _ip;
			if(IPAddress.TryParse(IP, out _ip))
				try {
					return reader.City(_ip);
				}catch (Exception ex) {
					return null;
				}
			else
				return null;
		}
	}
}
