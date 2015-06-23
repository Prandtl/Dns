using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		public override string ToString()
		{
			return string.Format("{0,-32}\t{1}\t{2}", Name, Class, Type);
		}
	}
}
