using System.Collections.Generic;

namespace DinosaursNotSausages.Dns.Records
{
	interface IRecord
	{
		byte[] GetData();
	}
}
