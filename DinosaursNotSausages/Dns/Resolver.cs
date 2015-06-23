using System;
using System.Collections.Generic;
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
								Callback(receivedResults.Buffer);
							}
						}
					});

		}

		private void Callback(byte[] data)
		{
			var reader = new Reader(data);
			var header = new Header(reader);
			var questions = new List<Question>();
			for (int i = 0; i < header.QuestionCount; i++)
			{
				questions.Add(new Question(reader));
			}
			for (int i = 0; i < header.AnswerCount; i++)
			{

			}
			for (int i = 0; i < header.AuthorityCount; i++)
			{

			}
			for (int i = 0; i < header.AdditionalCount; i++)
			{

			}
			Console.WriteLine(header.GetHumanReadableForm());
			foreach (var question in questions)
			{
				Console.WriteLine(question.ToString());
			}
			Console.WriteLine("----------------------------------------");
		}


	}
}
