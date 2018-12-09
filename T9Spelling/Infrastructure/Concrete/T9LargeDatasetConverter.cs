using T9Spelling.Infrastructure.Abstract;

namespace T9Spelling.Infrastructure.Concrete
{
	class T9LargeDatasetConverter: T9Converter
	{
		public T9LargeDatasetConverter(string[] lines): base (lines, 1, 1000)
		{			
		}

		public override string[] Convert()
		{
			return ConvertLines();
		}
	}
}
