using System;
using System.Collections.Generic;
using System.Text;

namespace DinosaursNotSausages.Dns
{
	class Header
	{
		//transaction id
		public ushort TransactionId { get; set; }
		//flags
		public ushort Flags { get; set; }
		#region flags
		public bool QR
		{
			get
			{
				return Reader.GetBits(Flags, 15, 1) == 1;
			}
			set
			{
				Flags = Reader.SetBits(Flags, 15, 1, value);
			}
		}
		public OPCode OpCode
		{
			get
			{
				return (OPCode)Reader.GetBits(Flags, 11, 4);
			}
			set
			{
				Flags = Reader.SetBits(Flags, 11, 4, (ushort)value);
			}
		}
		public bool Authority
		{
			get
			{
				return Reader.GetBits(Flags, 10, 1) == 1;
			}
			set
			{
				Flags = Reader.SetBits(Flags, 10, 1, value);
			}
		}
		public bool Truncation
		{
			get
			{
				return Reader.GetBits(Flags, 9, 1) == 1;
			}
			set
			{
				Flags = Reader.SetBits(Flags, 9, 1, value);
			}
		}
		public bool RecursionDesired
		{
			get
			{
				return Reader.GetBits(Flags, 8, 1) == 1;
			}
			set
			{
				Flags = Reader.SetBits(Flags, 8, 1, value);
			}
		}
		public bool RecursionAvaliable
		{
			get
			{
				return Reader.GetBits(Flags, 7, 1) == 1;
			}
			set
			{
				Flags = Reader.SetBits(Flags, 7, 1, value);
			}
		}
		public ushort Z
		{
			get
			{
				return Reader.GetBits(Flags, 4, 3);
			}
			set
			{
				Flags = Reader.SetBits(Flags, 4, 3, value);
			}
		}
		public RCode Result
		{
			get
			{
				return (RCode)Reader.GetBits(Flags, 0, 4);
			}
			set
			{
				Flags = Reader.SetBits(Flags, 0, 4, (ushort)value);
			}
		}
		#endregion
		//question rr count
		public ushort QuestionCount { get; set; }
		//answer rr count
		public ushort AnswerCount { get; set; }
		//authority rr count
		public ushort AuthorityCount { get; set; }
		//additional rr count
		public ushort AdditionalCount { get; set; }


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

		public Header(bool qr)
		{
			var r = new Random();
			TransactionId = (ushort)r.Next(ushort.MaxValue);
			QR = qr;
			OpCode = 0;
			Authority = false;
			Truncation = false;
			RecursionAvaliable = false;
			RecursionDesired = true;
		}

		public string GetHumanReadableForm()
		{
			var sb = new StringBuilder();
			sb.AppendLine("transaction id: " + TransactionId);
			sb.AppendLine(QR ? "response" : "query");
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
		public byte[] Data
		{
			get
			{
				List<byte> data = new List<byte>();
				data.AddRange(Writer.WriteShort(TransactionId));
				data.AddRange(Writer.WriteShort(Flags));
				data.AddRange(Writer.WriteShort(QuestionCount));
				data.AddRange(Writer.WriteShort(AnswerCount));
				data.AddRange(Writer.WriteShort(AuthorityCount));
				data.AddRange(Writer.WriteShort(AdditionalCount));
				return data.ToArray();
			}
		}

		private void ReadFlags()
		{
			Flags = reader.ReadUshort();
		}
	}
}
