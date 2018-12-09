using T9Spelling.Infrastructure.Abstract;

namespace T9Spelling.Infrastructure.Concrete
{
	public class SmallDatasetConverterCreator : ConverterCreator
	{
		public override T9Converter Create(string[] lines)
		{
			return new T9SmallDatasetConverter(lines);
		}
	}
}
