using System.Collections.Generic;

namespace DinosaursNotSausages.Dns
{
	class Question
	{
		public string Name { get; private set; }
		public QType Type { get; private set; }
		public QClass Class { get; private set; }

		public Question(Reader reader)
		{
			Name = reader.ReadDomain();
			Type = (QType) reader.ReadUshort();
			Class = (QClass) reader.ReadUshort();
		}

		public Question(string name,QType type, QClass qClass)
		{
			Name = name;
			Type = type;
			Class = qClass;
		}

		public byte[] Data
		{
			get
			{
				List<byte> data = new List<byte>();
				data.AddRange(Writer.WriteName(Name));
				data.AddRange(Writer.WriteShort((ushort)Type));
				data.AddRange(Writer.WriteShort((ushort)Class));
				return data.ToArray();
			}
		}

		public override string ToString()
		{
			return string.Format("{0,-32}\t{1}\t{2}", Name, Class, Type);
		}
	}
}
