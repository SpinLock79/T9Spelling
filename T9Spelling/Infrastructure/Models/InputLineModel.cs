using System.ComponentModel;
using System.Text.RegularExpressions;
using T9Spelling.Infrastructure.Converters;

namespace T9Spelling.Infrastructure.Models
{
	[TypeConverter(typeof(InputLineConverter))]
	public class InputLineModel
	{
		readonly Regex expression = new Regex("^[a-z ]+$");
		public InputLineModel(string line)
		{
			Line = expression.IsMatch(line) ? line : string.Empty;
		}

		public string Line { get; }
	}
}
