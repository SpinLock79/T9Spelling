using System;
using System.IO;
using System.Linq;
using System.Reflection;
using T9Spelling.Infrastructure.Abstract;
using T9Spelling.Infrastructure.Concrete;

namespace T9Spelling
{
	class Program
	{
		private const string SmallFilePath = @"Files/C-small-practice";
		private const string LargeFilePath = @"Files/C-large-practice";		

		static void Main(string[] args)
		{
			try
			{
				Console.Write("Small dataset processing");
				ConverterCreator creator = new SmallDatasetConverterCreator();
				var lines = GetFileLines(SmallFilePath);
				ConvertAndOutput(creator, lines, SmallFilePath);
				Console.WriteLine("- OK");

				Console.Write("Large dataset processing");
				creator = new LargeDatasetConverterCreator();
				lines = GetFileLines(LargeFilePath);
				ConvertAndOutput(creator, lines, LargeFilePath);
				Console.WriteLine("- OK");

			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
			}
			Console.ReadKey();
		}

		/// <summary>
		/// Gets lines from a text file
		/// </summary>
		/// <param name="path">Relative path to a file</param>
		/// <returns>Lines of the text file</returns>
		static string[] GetFileLines(string path)
		{
			var realPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $"{path}.in");
			return File.ReadAllLines(realPath);
		}

		/// <summary>
		/// Runs convertation of text lines to their T9 key codes and outputs the codes to Console
		/// </summary>
		/// <param name="creator">Creator of T9Converter</param>
		/// <param name="lines">Lines of a text file</param>
		/// <param name="path">Relative path to a file</param>
		static void ConvertAndOutput(ConverterCreator creator, string[] lines, string path)
		{
			var converter = creator.Create(lines);
			var outputLines = converter.Convert();
			if (outputLines!=null && outputLines.Length > 0)
			{
				var realPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $"{path}.out");
				var outputArray = outputLines.Select((x, i) => $"Case #{i + 1}:{x}").ToArray();
				File.WriteAllLines(realPath, outputArray);
			}			
		}
		
	}
}
