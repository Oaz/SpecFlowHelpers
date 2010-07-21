using System;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Specs.Oaz.SpecFlowHelpers.Moq
{
	[TestFixture]
	public class RegexSpike
	{
		[Test]
		public void TestCase ()
		{	
			var m0 = Regex.Match( "aa(bb,cc,dd,,ee)", "(.*)\\(" );
			Assert.That( m0.Groups[1].Value, Is.EqualTo("aa") );
			
			var m1 = Regex.Matches( "aa(bb,cc,dd,,ee)", "[,\\(]([^,\\)]*)" );
			Assert.That( m1[0].Groups[1].Value, Is.EqualTo("bb") );
			Assert.That( m1[1].Groups[1].Value, Is.EqualTo("cc") );
			Assert.That( m1[2].Groups[1].Value, Is.EqualTo("dd") );
			Assert.That( m1[3].Groups[1].Value, Is.EqualTo("") );
			Assert.That( m1[4].Groups[1].Value, Is.EqualTo("ee") );
		}
	}
}

