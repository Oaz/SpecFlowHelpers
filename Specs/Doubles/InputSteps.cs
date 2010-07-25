using System;

using TechTalk.SpecFlow;
using NUnit.Framework;
using Oaz.SpecFlowHelpers;
using Oaz.SpecFlowHelpers.Doubles;

namespace Specs.Oaz.SpecFlowHelpers.Doubles
{
	public interface Stuff
	{
		string AString { get; }
		int AnInt { get; }
		double ADouble { get; }
		DateTime ADateTime { get; }
	}
	
	[Binding]
	public class InputSteps
	{
        [When(@"I do nothing")]
        public void WhenIDoNothing()
        {
        }

		[Then(@"I get an exception '(.*)'")]
		public void ThenIGetAnException (string message)
		{
			Assert.That (Instance.Of<Exception> ().Object, Is.Not.Null);
			Assert.That (Instance.Of<Exception> ().Object.Message, Contains.Substring (message));
		}

		[Then(@"I do not get any exception")]
		public void ThenIDoNotGetAnException ()
		{
			Assert.That (Instance.Of<Exception> ().Object, Is.Null);
		}
		
		//===============================
		
		[Given(@"I have Stuff defined as following")]
		public void GivenIHaveStuffDefinedAsFollowing (Table table)
		{
			Instance.Of<Stuff>().Is (table.AsDouble<Stuff> ());
		}

		[Then(@"my Stuff values are as following")]
		public void ThenMyStuffValuesAreAsFollowing (Table table)
		{
			Assert.That (Instance.Of<Stuff> ().Object, Is.Not.Null);
			Assert.That (Instance.Of<Stuff> ().Object, Is.EqualTo (table.AsDouble<Stuff> ()).Using (Properties.StrictComparison));
		}
		
		[Then(@"my Stuff values are NOT as following")]
		public void ThenMyStuffValuesAreNotAsFollowing (Table table)
		{
			Assert.That (Instance.Of<Stuff> ().Object, Is.Not.Null);
			Assert.That (Instance.Of<Stuff> ().Object, Is.Not.EqualTo (table.AsDouble<Stuff> ()).Using (Properties.StrictComparison));
		}

		[Then(@"my Stuff values contains the following")]
		public void ThenMyStuffValuesContainsTheFollowing (Table table)
		{
			Assert.That (Instance.Of<Stuff> ().Object, Is.Not.Null);
			Assert.That (Instance.Of<Stuff> ().Object, Is.EqualTo (table).Using (Properties.LooseComparison));
		}

		[Then(@"my Stuff values does NOT contain the following")]
		public void ThenMyStuffValuesDoesNotContainTheFollowing (Table table)
		{
			Assert.That (Instance.Of<Stuff> ().Object, Is.Not.Null);
			Assert.That (Instance.Of<Stuff> ().Object, Is.Not.EqualTo (table).Using (Properties.LooseComparison));
		}
		
		//===============================
	}
}

