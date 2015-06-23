using DinosaursNotSausages.Dns;

namespace DinosaursNotSausages
{
	class Program
	{
		static void Main()
		{
			var resolver = new Resolver();
			resolver.Listen();
			while (true)
			{

			}
		}
	}
}
