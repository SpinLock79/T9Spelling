namespace T9Spelling.Infrastructure.Abstract
{
	/// <summary>
	///  Abstract ConverterCreator
	/// </summary>
	public abstract class ConverterCreator
	{		
		public abstract T9Converter Create(string[] lines);
	}
}
