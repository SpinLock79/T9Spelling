using T9Spelling.Infrastructure.Abstract;

namespace T9Spelling.Infrastructure.Concrete
{
	public class LargeDatasetConverterCreator : ConverterCreator
	{
		public override T9Converter Create(string[] lines)
		{
			return new T9LargeDatasetConverter(lines);
		}
	}
}
