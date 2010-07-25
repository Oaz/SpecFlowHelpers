using System;
using NUnit.Framework;
using Oaz.SpecFlowHelpers;
namespace Specs.Oaz.SpecFlowHelpers
{
	[TestFixture]
	public class BasicTests
	{
		[Test]
		public void CapitalizeString ()
		{
			Assert.That( "boat".Capitalize(), Is.EqualTo("Boat") );
			Assert.That( "BOAT".Capitalize(), Is.EqualTo("Boat") );
			Assert.That( "bOaT".Capitalize(), Is.EqualTo("Boat") );
			Assert.That( "BoAt".Capitalize(), Is.EqualTo("Boat") );
		}
		
		[Test]
		public void StringToMethodName ()
		{
			Assert.That( "call me".AsMethodName(), Is.EqualTo("CallMe") );
			Assert.That( "call me again".AsMethodName(), Is.EqualTo("CallMeAgain") );
			Assert.That( "CALL ME NOW".AsMethodName(), Is.EqualTo("CallMeNow") );
		}
	}
}

