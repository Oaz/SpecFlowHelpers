using System;
using TechTalk.SpecFlow;
using Oaz.SpecFlowHelpers;
using Oaz.SpecFlowHelpers.Moq;
using NUnit.Framework;

namespace Specs.Oaz.SpecFlowHelpers.Moq
{
	public interface Stuff
	{
		string AString { get; }
		int AnInt { get; }
		double ADouble { get; }
		DateTime ADateTime { get; }
	}
	
	[Binding]
	public class TablesSteps
	{
        [When(@"I do nothing")]
        public void WhenIDoNothing()
        {
        }

		[Then(@"I do not get any exception")]
		public void ThenIDoNotGetAnException ()
		{
			Assert.That (Instance.Of<Exception> (), Is.Null);
		}
		
		[Given(@"I have Stuff defined as following")]
		public void GivenIHaveStuffDefinedAsFollowing (Table table)
		{
			Instance.Is (table.AsMock<Stuff> ());
		}

		[Then(@"my Stuff values are as following")]
		public void ThenMyStuffValuesAreAsFollowing (Table table)
		{
			Assert.That (Instance.Of<Stuff> (), Is.Not.Null);
			Assert.That (Instance.Of<Stuff> (), Is.EqualTo (table.AsMock<Stuff> ()).Using (Properties.StrictComparison));
		}
	}
}

