namespace DinosaursNotSausages.Dns.Records
{
	class NSData:IRecord
	{
		public string NsName { get; private set; }
		
		public NSData(Reader reader)
		{
			NsName = reader.ReadDomain();
		}

		public override string ToString()
		{
			return NsName;
		}

		public byte[] GetData()
		{
			return Writer.WriteName(NsName);
		}
	}
}
