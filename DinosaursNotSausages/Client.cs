using System;
using System.Net;

namespace DinosaursNotSausages
{
	class Client
	{
		private IPEndPoint dns;

		public Client(IPEndPoint dns)
		{
			this.dns = dns;
		}

	}
}
