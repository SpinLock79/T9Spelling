using T9Spelling.Infrastructure.Abstract;

namespace T9Spelling.Infrastructure.Concrete
{
	class T9SmallDatasetConverter: T9Converter
	{
		public T9SmallDatasetConverter(string[] lines): base (lines, 1, 15)
		{			
		}

		public override string[] Convert()
		{
			return ConvertLines();
		}
	}
}
