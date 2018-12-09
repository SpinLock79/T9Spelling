using NUnit.Framework;
using System;
using System.ComponentModel;
using T9Spelling.Infrastructure.Converters;
using T9Spelling.Infrastructure.Models;

namespace NUnit.Tests
{
	[TestFixture]
	public class InputLineConverterTestClass
	{
		private TypeConverter _converter;
		[SetUp]
		protected void Setup()
		{
			_converter = TypeDescriptor.GetConverter(typeof(InputLineModel));			
		}

		[Test]
		public void InvalidType()
		{
			Assert.Catch<NullReferenceException>(() => _converter.ConvertFrom(null));
			Assert.Catch<ArgumentException>(()=>_converter.ConvertFrom(""));
		}

		[Test]
		public void CheckConversion()
		{
			var c = _converter as InputLineConverter;
			Assert.AreEqual(c.ConvertChar('d', ' '), "3");		
		}

		[Test]
		public void CheckForTheSameKeyBefore()
		{
			var c = _converter as InputLineConverter;
			Assert.AreEqual(c.ConvertChar('b', '2'), " 22");
		}

		[Test]
		public void CheckForSpaceCharacter()
		{
			var c = _converter as InputLineConverter;
			Assert.AreEqual(c.ConvertChar(' ', ' '), "0");
		}
	}
}
