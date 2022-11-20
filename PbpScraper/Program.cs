using System.IO;

namespace PbpScraper
{
	public class Program
	{
		public static void Main(string[] args)
		{
			if (args.Length < 1)
			{
				Console.WriteLine("Usage: Specify PBP file.");
				return;
			}
			Pbp pbp;
			using (FileStream fileStream = new FileStream(
				path: args[0],
				mode: FileMode.Open))
				pbp = new Pbp(fileStream);
			pbp.SaveFiles();
		}
	}
}
