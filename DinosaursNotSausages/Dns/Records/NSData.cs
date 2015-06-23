namespace DinosaursNotSausages.Dns.Records
{
	class NSData:IRecord
	{
		public string nsName { get; private set; }
		
		public NSData(Reader reader)
		{
			reader.ReadDomain();
		}

		public override string ToString()
		{
			return nsName;
		}
	}
}
