using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DinosaursNotSausages.Dns
{
	class Resolver
	{
		private bool udpConnection = true;
		private int timeOut = 1;
		private int port = 53;

		public void Listen()
		{
			if (udpConnection)
			{
				UdpListen();
			}
			//todo:tcpListen
		}

		public void SetTimeOut(int seconds)
		{
			timeOut = seconds;
		}

		private void UdpListen()
		{
			Task.Run(async () =>
					{
						using (var udpClient = new UdpClient(53))
						{
							while (true)
							{
								var receivedResults = await udpClient.ReceiveAsync();

								
							}
						}
					});

		}

	}
}
