using System.Net;

namespace DinosaursNotSausages.Dns.Records
{
	class AData:IRecord
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
