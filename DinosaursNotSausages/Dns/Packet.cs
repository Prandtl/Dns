using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

namespace DinosaursNotSausages.Dns
{
	class Packet
	{
		private Header header;
		private List<Question> questions;
		private List<AnswerRR> answers;
		private List<AuthorityRR> authorities;
		private List<AdditionalRR> additionals;

		public Packet(byte[] data)
		{
			var r = new Reader(data);
			header = new Header(r);
			for (int i = 0; i < header.QuestionCount; i++)
			{
				questions.Add(new Question(r));
			}
			for (int i = 0; i < header.AnswerCount; i++)
			{
				answers.Add(new AnswerRR(r));
			}
			for (int i = 0; i < header.AuthorityCount; i++)
			{
				authorities.Add(new AuthorityRR(r));
			}
			for (int i = 0; i < header.AdditionalCount; i++)
			{
				additionals.Add(new AdditionalRR(r));
			}
		}

		public Packet(Header header)
		{
			this.header = header;
		}

		public void AddQuestion(Question q)
		{
			questions.Add(q);
			header.QuestionCount++;
		}

		public void AddAnswer(AnswerRR a)
		{
			answers.Add(a);
			header.AnswerCount++;
		}

		public void AddAuthority(AuthorityRR a)
		{
			authorities.Add(a);
			header.AuthorityCount++;
		}

		public void AddAdditional(AdditionalRR a)
		{
			additionals.Add(a);
			header.AdditionalCount++;
		}

		public byte[] Data 
		{
			get
			{
				List<byte> data =new List<byte>();
				data.AddRange(header.Data);
				for (int i = 0; i < header.QuestionCount; i++)
				{
					data.AddRange(questions[i].Data);
				}
				for (int i = 0; i < header.AnswerCount; i++)
				{
					data.AddRange(answers[i].Data);
				}
				for (int i = 0; i < header.AuthorityCount; i++)
				{
					data.AddRange(authorities[i].Data);
				}
				for (int i = 0; i < header.AdditionalCount; i++)
				{
					data.AddRange(additionals[i].Data);
				}
				return data.ToArray();
			}
		}
		
	}
}
