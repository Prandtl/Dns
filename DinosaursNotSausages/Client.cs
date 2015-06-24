using System;
using System.Net;
using System.Net.Sockets;
using DinosaursNotSausages.Dns;

namespace DinosaursNotSausages
{
	class Client
	{
		private IPEndPoint dns;
		private UdpClient client;
		public Client(IPEndPoint dns, UdpClient client)
		{
			this.dns = dns;
			this.client = client;
		}

		public void Resolve(string address)
		{
			var header = new Header(false);
			var packet = new Packet(header);
			packet.AddQuestion(new Question(address, QType.A, QClass.ANY));

		}

	}
}
