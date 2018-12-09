using System;
using System.IO;
using NUnit.Framework;
using T9Spelling.Infrastructure.Abstract;
using T9Spelling.Infrastructure.Concrete;

namespace NUnit.Tests
{
	[TestFixture]
	public class T9ConverterTestClass
	{
		private ConverterCreator _creator;
		[SetUp]
		protected void SetUp()
		{
			_creator = new SmallDatasetConverterCreator();
		}

		[Test]		
		public void LineArrayIsNull_Throws()
		{					
			Assert.Catch<ArgumentNullException>(() => _creator.Create(null));
		}

		[Test]
		public void InvalidCaseCount_Throws()
		{			
			var lines = new string[] { "1", "a", "b" };
			Assert.Catch<InvalidDataException> (() => _creator.Create(lines));
		}

		[Test]
		public void CheckForMaxLength()
		{
			var lines = new string[] { "1", "aaaaaaaaaaaaaaaa" };
			var converter = _creator.Create(lines);
			var output = converter.Convert();
			Assert.AreEqual(output, new string[] { "" });
		}

		[Test]
		public void CheckForYesWord()
		{			
			var lines = new string[] { "1", "yes" };
			var converter = _creator.Create(lines);
			var output = converter.Convert();			
			Assert.AreEqual(output, new string[] { "999337777" });
		}

		[Test]
		public void CheckForPauseKey()
		{			
			var lines = new string[] { "2", "hi", "no" };
			var converter = _creator.Create(lines);
			var output = converter.Convert();
			Assert.AreEqual(output, new string[] { "44 444", "66 666" });
		}
	}
}
