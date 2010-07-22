using System;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Specs.Oaz.SpecFlowHelpers.Moq
{
	[TestFixture]
	public class RegexSpike
	{
		[Test]
		public void FunctionCallStyle ()
		{	
			var m0 = Regex.Match( "aa(bb,cc,dd,,ee)", "(.*)\\(" );
			Assert.That( m0.Groups[1].Value, Is.EqualTo("aa") );
			
			var m1 = Regex.Matches( "aa(bb,cc,dd,,ee)", "[,\\(]([^,\\)]*)" );
			Assert.That( m1.Count, Is.EqualTo(5) );
			Assert.That( m1[0].Groups[1].Value, Is.EqualTo("bb") );
			Assert.That( m1[1].Groups[1].Value, Is.EqualTo("cc") );
			Assert.That( m1[2].Groups[1].Value, Is.EqualTo("dd") );
			Assert.That( m1[3].Groups[1].Value, Is.EqualTo("") );
			Assert.That( m1[4].Groups[1].Value, Is.EqualTo("ee") );
		}
		
		[Test]
		public void FunctionCallStyleWithResult()
		{	
			var m0 = Regex.Match( "aa(bb,cc,dd,,ee)=zz", "(.*)\\(" );
			Assert.That( m0.Groups[1].Value, Is.EqualTo("aa") );
			
			var m1 = Regex.Matches( "aa(bb,cc,dd,,ee)=zz", "[,\\(]([^,\\)]*)" );
			Assert.That( m1.Count, Is.EqualTo(5) );
			Assert.That( m1[0].Groups[1].Value, Is.EqualTo("bb") );
			Assert.That( m1[1].Groups[1].Value, Is.EqualTo("cc") );
			Assert.That( m1[2].Groups[1].Value, Is.EqualTo("dd") );
			Assert.That( m1[3].Groups[1].Value, Is.EqualTo("") );
			Assert.That( m1[4].Groups[1].Value, Is.EqualTo("ee") );
			
			var m2 = Regex.Match( "aa(bb,cc,dd,,ee)=zz", "=(.*)" );
			Assert.That( m2.Groups[1].Value, Is.EqualTo("zz") );;
		}
		
		[Test]
		public void SpacesStyle ()
		{	
			var m0 = Regex.Match( "aa bb cc dd ee", "^(.*?) " );
			Assert.That( m0.Groups[1].Value, Is.EqualTo("aa") );
			
			var m1 = Regex.Matches( "aa bb cc dd ee", " ([^ ]+)" );
			Assert.That( m1.Count, Is.EqualTo(4) );
			Assert.That( m1[0].Groups[1].Value, Is.EqualTo("bb") );
			Assert.That( m1[1].Groups[1].Value, Is.EqualTo("cc") );
			Assert.That( m1[2].Groups[1].Value, Is.EqualTo("dd") );
			Assert.That( m1[3].Groups[1].Value, Is.EqualTo("ee") );
		}
		
		[Test]
		public void SpacesStyleWithResult ()
		{	
			var m0 = Regex.Match( "aa bb cc dd ee => zz", "^(.*?) " );
			Assert.That( m0.Groups[1].Value, Is.EqualTo("aa") );
			
			var m1 = Regex.Matches( "aa bb cc dd ee => zz", " ([^ ]+)" );
			Assert.That( m1.Count, Is.EqualTo(6) );
			Assert.That( m1[0].Groups[1].Value, Is.EqualTo("bb") );
			Assert.That( m1[1].Groups[1].Value, Is.EqualTo("cc") );
			Assert.That( m1[2].Groups[1].Value, Is.EqualTo("dd") );
			Assert.That( m1[3].Groups[1].Value, Is.EqualTo("ee") );
			
			var m2 = Regex.Match( "aa bb cc dd ee => zz", "=> *(.*)" );
			Assert.That( m2.Groups[1].Value, Is.EqualTo("zz") );;
		}
	}
}

