namespace DinosaursNotSausages.Dns
{
	class Header
	{
		//transaction id
		private ushort transactionId;

		//flags
		private bool request;
		private byte opCode;
		private bool authority;
		private bool truncation;
		private bool recursionDesired;
		private bool recursionAvaliable;
		private byte result;

		//question rr count
		private ushort questionCount;
		//answer rr count
		private ushort answerCount;
		//authority rr count
		private ushort authorityCount;
		//additional rr count
		private ushort additionalCount;


		private Reader reader;

		public Header(Reader reader)
		{
			this.reader = reader;
			transactionId = reader.ReadUshort();
			ReadFlags();
			questionCount = reader.ReadUshort();
			answerCount = reader.ReadUshort();
			authorityCount = reader.ReadUshort();
			additionalCount = reader.ReadUshort();

		}

		private void ReadFlags()
		{
			var firstFlags = reader.ReadByte();
			var secondFlags = reader.ReadByte();

			request = Reader.GetBit(firstFlags, 1);
			authority = Reader.GetBit(firstFlags, 6);
			truncation = Reader.GetBit(firstFlags, 7);

			opCode = ReadOpcode(firstFlags);

			recursionDesired = Reader.GetBit(firstFlags, 8);
			recursionAvaliable = Reader.GetBit(secondFlags, 1);

			result = ReadResult(secondFlags);

		}

		private byte ReadOpcode(byte flagsOne)
		{
			return Reader.ReadNumberInByte(flagsOne, 2, 5);
		}

		private byte ReadResult(byte flagsTwo)
		{
			return Reader.ReadNumberInByte(flagsTwo, 5, 8);
		}

	}
}
