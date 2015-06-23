using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DinosaursNotSausages.Dns.Records;

namespace DinosaursNotSausages.Dns
{
	class ResourceRecord
	{
		public string Name { get; private set; }
		public Type RrType { get; private set; }
		public Class RrClass { get; private set; }
		public uint TTL { get; private set; }
		public DateTime ArrivalTime { get; private set; }

		public Record Record { get; private set; }

		public ResourceRecord(Reader reader)
		{
			Name = reader.ReadDomain();
			RrType = (Type) reader.ReadUshort();
			RrClass = (Class) reader.ReadUshort();
			TTL = reader.ReadUInt();
			var dataLength = reader.ReadUshort();
			Record = reader.ReadRecord(RrType,dataLength);

		}
	}

	class AnswerRR : ResourceRecord
	{
		public AnswerRR(Reader reader)
			: base(reader)
		{
		}
	}

	class AuthorityRR : ResourceRecord
	{
		public AuthorityRR(Reader reader)
			: base(reader)
		{
		}
	}

	class AdditionalRR : ResourceRecord
	{
		public AdditionalRR(Reader reader)
			: base(reader)
		{
		}
	}
}
