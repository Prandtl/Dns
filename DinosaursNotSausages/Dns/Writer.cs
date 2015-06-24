using System;
using System.Net;
using System.Text;

namespace DinosaursNotSausages.Dns
{
	static class Writer
	{
		public static byte[] WriteName(string src)
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
			return Encoding.ASCII.GetBytes(sb.ToString());
		}

		public static byte[] WriteShort(ushort sValue)
		{
			return BitConverter.GetBytes(IPAddress.HostToNetworkOrder((ushort)sValue));
		}

		public static byte[] WriteUint(uint value)
		{
			return BitConverter.GetBytes(IPAddress.HostToNetworkOrder((uint)value));
		}
	}
}
