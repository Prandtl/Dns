using System.Collections.Generic;

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
			header.QuestionCount;
		}

		public void AddAnswer(AnswerRR a)
		{
			answers.Add(a);
		}

		public void AddAuthority(AuthorityRR a)
		{
			authorities.Add(a);
		}

		public void AddAdditional(AdditionalRR a)
		{
			additionals.Add(a);
		}

		
	}
}
