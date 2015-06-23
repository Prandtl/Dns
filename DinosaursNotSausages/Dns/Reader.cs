using System.Linq;
using System.Text;

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

		public Reader(byte[] data, int pointer)
		{
			this.data = data;
			this.pointer = pointer < data.Length ?
								pointer :
								data.Length - 1;
		}

		public Reader Copy()
		{
			return new Reader(data, pointer);
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
				bits[i - startPosition + 1] = GetBit(inputByte, i + 1);
			}
			var res = 0;
			bits = bits.Reverse().ToArray();
			for (int i = 0; i < bits.Length; i++)
			{
				res += bits[i] ? 1 << i : 0;
			}
			return (byte)res;
		}

		public char ReadChar()
		{
			return (char)ReadByte();
		}

		public byte Peek()
		{
			return data[pointer];
		}

		public void SetPosition(int position)
		{
			pointer = position;
		}

		public string ReadDomain()
		{
			StringBuilder domain = new StringBuilder();
			int length = 0;

			// get  the length of the first label
			while ((length = ReadByte()) != 0)
			{
				// top 2 bits set denotes domain name compression and to reference elsewhere
				if ((length & 0xc0) == 0xc0)
				{
					// work out the existing domain name, copy this pointer
					Reader newReader = Copy();

					// and move it to where specified here
					newReader.SetPosition((length & 0x3f) << 8 | ReadByte());

					// repeat call recursively
					domain.Append(newReader.ReadDomain());
					return domain.ToString();
				}

				// if not using compression, copy a char at a time to the domain name
				while (length > 0)
				{
					domain.Append(ReadChar());
					length--;
				}

				// if size of next label isn't null (end of domain name) add a period ready for next label
				if (Peek() != 0) domain.Append('.');
			}

			// and return
			return domain.ToString();
		}
	}
}
