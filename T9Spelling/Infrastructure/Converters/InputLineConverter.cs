using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using T9Spelling.Infrastructure.Models;

namespace T9Spelling.Infrastructure.Converters
{
	public class InputLineConverter: TypeConverter
	{
		/// <summary>
		/// Returns whether this converter can convert an object of InputLineModel to string that represents T9 code
		/// </summary>
		/// <param name="context"></param>
		/// <param name="sourceType"></param>
		/// <returns></returns>
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(InputLineModel);
		}

		/// <summary>
		/// Converts the given InputLineModel to string that represents T9 code
		/// </summary>
		/// <param name="context"></param>
		/// <param name="culture"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public override object ConvertFrom(ITypeDescriptorContext context,
		 CultureInfo culture,
		 object value)
		{
			if (value == null)
			{
				NullReferenceException nullReferenceException = new NullReferenceException("InputLineModel cannot be a null value");
				throw nullReferenceException;
			}

			var result = new StringBuilder();
			if (!CanConvertFrom(value.GetType())) throw new ArgumentException($"The '{value}' value cannot be converted!");

			var l = (value as InputLineModel).Item;
			var prev = '+';
			var i = 0;

			while (i < l.Length)
			{
				var c = l[i];
				var convStr = ConvertChar(c, prev);
				result.Append(convStr);
				prev = convStr.Last();
				i++;
			}
			return result.ToString();
		}

		/// <summary>
		/// Converts chars to their code strings
		/// </summary>
		/// <param name="c">Character to convert</param>
		/// <param name="last">Previous one</param>
		/// <returns>String that's a T9 code of the input character</returns>
		public string ConvertChar(char c, char last)
		{			
			var code = (int)c;
			//Returns zero if it is a space character
			if (code == 32) return "0";			

			var key = 0;
			var count = 1;
			//If the character is on the first 2 rows of the phone keyboard
			if (code < 'p')
			{
				var shift = code - 'a';
				key = shift / 3 + 2;
				count = shift % 3 + 1;
			}
			//otherwise
			else if (code >= 'p' && code<='s')
			{
				key = 7;
				count = code - 'p' + 1;
			}
			else if (code >= 't' && code <= 'v')
			{
				key = 8;
				count = code - 't' + 1;
			}
			else if (code >= 'w' && code <= 'z')
			{
				key = 9;
				count = code - 'w' + 1;
			}
			
			var sb = new StringBuilder();			
			sb.Insert(0, key.ToString(), count);
			if (key.ToString() == last.ToString())
			{
				sb.Insert(0, " ");
			}
			return sb.ToString();
		}
	}
}
