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
