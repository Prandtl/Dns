using System;
using System.Globalization;
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
			var reader = new Reader(new byte[] { 0, 255, 11 });
			for (int i = 0; i < 3; i++)
			{
				var next = reader.ReadByte();
				Console.WriteLine(next);
				for (int j = 0; j < 8; j++)
				{
					Console.WriteLine(reader.GetBit(next, j));
				}
				Console.WriteLine();
			}
			//var resolver = new Resolver();
			//resolver.Listen();
			//while (true)
			//{

			//}
		}
	}
}
