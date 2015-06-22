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
		private bool recursive;
		private byte result;

		//question rr count
		private ushort questionCount;
		//answer rr count
		private ushort answerCount;
		//authority rr count
		private ushort authorityCount;
		//additional rr count
		private ushort additionalCount;


	}
}
