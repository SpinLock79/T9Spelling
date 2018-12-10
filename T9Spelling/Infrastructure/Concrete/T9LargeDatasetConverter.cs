using T9Spelling.Infrastructure.Abstract;

namespace T9Spelling.Infrastructure.Concrete
{
	class T9LargeDatasetConverter: T9Converter
	{
		public T9LargeDatasetConverter(string[] lines): base (lines, 1, 1000)
		{			
		}

		/// <summary>
		/// Convert method for a Large dataset
		/// </summary>
		/// <returns>Output result array</returns>
		public override string[] Convert()
		{
			return ConvertLines();
		}
	}
}
