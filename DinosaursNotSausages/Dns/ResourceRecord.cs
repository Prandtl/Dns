using System;
using System.Collections.Generic;
using DinosaursNotSausages.Dns.Records;

namespace DinosaursNotSausages.Dns
{
	class ResourceRecord
	{
		public string Name { get; private set; }
		public Type RrType { get; private set; }
		public Class RrClass { get; private set; }
		public uint TTL { get; private set; }

		public IRecord Record { get; private set; }

		public DateTime ArrivalTime { get; private set; }

		public ResourceRecord(Reader reader)
		{
			Name = reader.ReadDomain();
			RrType = (Type)reader.ReadUshort();
			RrClass = (Class)reader.ReadUshort();
			TTL = reader.ReadUInt();
			var dataLength = reader.ReadUshort();
			Record = reader.ReadRecord(RrType, dataLength);

		}

		public byte[] Data
		{
			get
			{
				List<byte> data = new List<byte>();
				data.AddRange(Writer.WriteName(Name));
				data.AddRange(Writer.WriteShort((ushort)RrType));
				data.AddRange(Writer.WriteShort((ushort)RrClass));
				data.AddRange(Writer.WriteUint(TTL));
				data.AddRange(Record.GetData());
				return data.ToArray();
			}
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
