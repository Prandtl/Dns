using System.Net;

namespace DinosaursNotSausages.Dns.Records
{
	class AData:Record
	{
		public IPAddress Address { get; private set; }

		public AData(Reader reader)
		{
			Address = new IPAddress(reader.ReadBytes(4));
		}

		public override string ToString()
		{
			return Address.ToString();
		}
	}
}
