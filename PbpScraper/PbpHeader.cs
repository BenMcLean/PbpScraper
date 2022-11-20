namespace PbpScraper
{
	public class PbpHeader
	{
		public static readonly char[] CorrectSignature = new char[4] { (char)0x00, 'P', 'B', 'P' };
		public char[] Signature = new char[4];
		public int Version = 0;
		public int[] Offsets = new int[8];
		public PbpHeader(Stream stream) : this(new BinaryReader(stream)) { }
		public PbpHeader(BinaryReader binaryReader)
		{
			for (int i = 0; i < CorrectSignature.Length; i++)
				if ((Signature[i] = binaryReader.ReadChar()) != CorrectSignature[i])
					throw new InvalidDataException("Invalid PBP signature!");
			Version = binaryReader.ReadInt32();
			for (int i = 0; i < Offsets.Length; i++)
				Offsets[i] = binaryReader.ReadInt32();
		}
	}
}
