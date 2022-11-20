namespace PbpScraper
{
	public class Pbp
	{
		public readonly static string[] Filenames = new string[8] {
			"PARAM.SFO",
			"ICON0.PNG",
			"ICON1.PMF",
			"PIC0.PNG",
			"PIC1.PNG",
			"SND0.AT3",
			"DATA.PSP",
			"DATA.PSAR"
			};
		public byte[][] Files = new byte[6][];
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
		public PbpHeader Header;
		public Pbp(Stream stream) : this(new BinaryReader(stream)) { }
		public Pbp(BinaryReader binaryReader)
		{
			Header = new PbpHeader(binaryReader);
			for (int fileNumber = 0; fileNumber < Files.Length - 1; fileNumber++)
			{
				binaryReader.BaseStream.Seek(Header.Offsets[fileNumber], SeekOrigin.Begin);
				Files[fileNumber] = binaryReader.ReadBytes(Header.Offsets[fileNumber + 1] - Header.Offsets[fileNumber]);
			}
		}
		public void SaveFiles(string path = "")
		{
			if (!string.IsNullOrEmpty(path))
				Directory.CreateDirectory(path);
			for (int fileNumber = 0; fileNumber < Files.Length; fileNumber++)
				if (Files[fileNumber] is not null)
					File.WriteAllBytes(Path.Combine(path, Filenames[fileNumber]), Files[fileNumber]);
		}
	}
}
