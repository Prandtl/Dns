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

		public bool GetBit(byte inputByte, int numberOfBit)
		{
			ushort firstOne = 128;
			int shift = numberOfBit % 8;
			return (inputByte & (firstOne >> shift)) != 0;
		}


	}
}
