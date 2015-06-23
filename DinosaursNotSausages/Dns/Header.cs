using System.Text;

namespace DinosaursNotSausages.Dns
{
	class Header
	{
		//transaction id
		public ushort TransactionId { get; private set; }
		//flags
		public bool Request { get; private set; }
		public byte OpCode { get; private set; }
		public bool Authority { get; private set; }
		public bool Truncation { get; private set; }
		public bool RecursionDesired { get; private set; }
		public bool RecursionAvaliable { get; private set; }
		public byte Result { get; private set; }
		//question rr count
		public ushort QuestionCount { get; private set; }
		//answer rr count
		public ushort AnswerCount { get; private set; }
		//authority rr count
		public ushort AuthorityCount { get; private set; }
		//additional rr count
		public ushort AdditionalCount { get; private set; }

		private Reader reader;

		public Header(Reader reader)
		{
			this.reader = reader;
			TransactionId = reader.ReadUshort();
			ReadFlags();
			QuestionCount = reader.ReadUshort();
			AnswerCount = reader.ReadUshort();
			AuthorityCount = reader.ReadUshort();
			AdditionalCount = reader.ReadUshort();
		}

		public string GetHumanReadableForm()
		{
			var sb = new StringBuilder();
			sb.AppendLine("transaction id: " + TransactionId);
			sb.AppendLine(Request ? "response" : "query");
			sb.AppendLine("operation code: " + OpCode);
			sb.AppendLine("authority: " + Authority);
			sb.AppendLine("truncation: " + Truncation);
			sb.AppendLine("recursion desired: " + RecursionDesired);
			sb.AppendLine("recursion avaliable: " + RecursionAvaliable);
			sb.AppendLine("result code: " + Result);
			sb.AppendLine("question count: " + QuestionCount);
			sb.AppendLine("answer count: " + AnswerCount);
			sb.AppendLine("authority count: " + AuthorityCount);
			sb.AppendLine("additional count: " + AdditionalCount);
			return sb.ToString();
		}

		private void ReadFlags()
		{
			var firstFlags = reader.ReadByte();
			var secondFlags = reader.ReadByte();

			Request = Reader.GetBit(firstFlags, 1);

			OpCode = ReadOpcode(firstFlags);

			Authority = Reader.GetBit(firstFlags, 6);
			Truncation = Reader.GetBit(firstFlags, 7);
			RecursionDesired = Reader.GetBit(firstFlags, 8);
			RecursionAvaliable = Reader.GetBit(secondFlags, 1);

			Result = ReadResult(secondFlags);

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
