using System.Linq;

namespace DinosaursNotSausages.Dns
{
	class Reader
	{
		private byte[] data;
		private int pointer;

		public Reader(byte[] data)
		{
			this.data = data;
			pointer = 0;
		}

		public byte ReadByte()
		{
			var result = pointer <= data.Length ?
							data[pointer] :
							(byte)0;
			pointer++;
			return result;
		}

		public ushort ReadUshort()
		{
			return (ushort)(ReadByte() << 8 | ReadByte());
		}

		public static bool GetBit(byte inputByte, int numberOfBit)
		{
			ushort firstOne = 128;
			int shift = (numberOfBit - 1) % 8;
			return (inputByte & (firstOne >> shift)) != 0;
		}

		public static byte ReadNumberInByte(byte inputByte, int startPosition, int endPosition)
		{
			if (endPosition < startPosition)
				return 0;
			var bits = new bool[endPosition - startPosition + 1];
			for (int i = startPosition - 1; i < endPosition; i++)
			{
				bits[i-startPosition+1] = GetBit(inputByte, i + 1);
			}
			var res = 0;
			bits = bits.Reverse().ToArray();
			for (int i = 0; i <bits.Length; i++)
			{
				res += bits[i] ? 1 << i : 0;
			}
			return (byte)res;
		}

	}
}
