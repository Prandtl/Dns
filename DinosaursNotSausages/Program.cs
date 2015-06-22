using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using DinosaursNotSausages.Dns;

namespace DinosaursNotSausages
{
	class Program
	{
		static void Main(string[] args)
		{
			var resolver = new Resolver();
			resolver.Listen();
			while (true)
			{
				
			}
		}
	}
}
