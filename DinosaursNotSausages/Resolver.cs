using System.Net.Sockets;

namespace DinosaursNotSausages.Dns
{
	class Resolver
	{
		public void Run()
		{
			var sock = new Socket(AddressFamily.InterNetwork,SocketType.Dgram, ProtocolType.Udp);
		}
		
	}
}
