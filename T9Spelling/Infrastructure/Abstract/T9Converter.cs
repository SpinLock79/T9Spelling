using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using T9Spelling.Infrastructure.Interfaces;
using T9Spelling.Infrastructure.Models;

namespace T9Spelling.Infrastructure.Abstract
{
	/// <summary>
	/// Abstract T9Converter
	/// </summary>
	public abstract class T9Converter: IT9Converter
	{
		const int MinCases = 1;
		const int MaxCases = 100;

		private readonly int _minLength;
		private readonly int _maxLength;
		protected InputModel Input { get; }
		public abstract string[] Convert();
		/// <summary>
		/// T9Converter constructor
		/// </summary>
		/// <param name="lines">Input lines</param>
		/// <param name="minLength">The minimum value of a line length</param>
		/// <param name="maxLength">The maximum value of a line length</param>
		protected T9Converter(string[] lines, int minLength, int maxLength)
		{
			_minLength = minLength;
			_maxLength = maxLength;

			if (lines == null || lines.Length == 0)
			{
				throw new ArgumentNullException(nameof(lines));
			}

			var cases = int.Parse(lines[0]);
			var inputLines = lines.Skip(1)
				.Select(x => new InputLineModel(x))
				.ToArray();

			if (cases!=inputLines.Length)
			{
				throw new InvalidDataException();
			}

			Input = new InputModel
			{
				Cases = cases,
				Lines = inputLines
			};
		}

		/// <summary>
		/// Runs ConvertFrom method of a TypeConverter for each line
		/// </summary>
		/// <returns>Output result array</returns>
		protected string[] ConvertLines()
		{
			var output = new List<string>();
			if (Input.Cases < MinCases || Input.Cases > MaxCases)
			{
				throw new InvalidDataException();
			}

			var c = TypeDescriptor.GetConverter(typeof(InputLineModel));
			foreach (var l in Input.Lines)
			{
				//Adds an empty string if it's not possible to convert the value to its T9 code representation
				var str = string.Empty;
				if (l.Item.Length>=_minLength && l.Item.Length <= _maxLength)
				{
					var s = (string)c.ConvertFrom(l);
					if (s != null)
					{
						str = s;
					}					
				}
				output.Add(str);
			}
			return output.ToArray();
		}
	}
}
