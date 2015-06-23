using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

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

		private byte[] WriteName(string src)
		{
			if (!src.EndsWith("."))
				src += ".";

			if (src == ".")
				return new byte[1];

			StringBuilder sb = new StringBuilder();
			int intI, intJ, intLen = src.Length;
			sb.Append('\0');
			for (intI = 0, intJ = 0; intI < intLen; intI++, intJ++)
			{
				sb.Append(src[intI]);
				if (src[intI] == '.')
				{
					sb[intI - intJ] = (char)(intJ & 0xff);
					intJ = -1;
				}
			}
			sb[sb.Length - 1] = '\0';
			return System.Text.Encoding.ASCII.GetBytes(sb.ToString());
		}

		public byte[] Data
		{
			get
			{
				List<byte> data = new List<byte>();
				data.AddRange(WriteName(Name));
				data.AddRange(WriteShort((ushort)Type));
				data.AddRange(WriteShort((ushort)Class));
				return data.ToArray();
			}
		}

		private byte[] WriteShort(ushort sValue)
		{
			return BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)sValue));
		}


		public override string ToString()
		{
			return string.Format("{0,-32}\t{1}\t{2}", Name, Class, Type);
		}
	}
}
