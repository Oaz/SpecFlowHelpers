using System;
using System.Linq;
using TechTalk.SpecFlow;
using NUnit.Framework;
using Oaz.SpecFlowHelpers;
using System.Collections.Generic;

namespace Specs.Oaz.SpecFlowHelpers
{
	class Stuff
	{
		public string AString { get; set; }
		public int AnInt { get; set; }
		public double ADouble { get; set; }
		public DateTime ADateTime { get; set; }
	}

	[Binding]
	public class TablesSteps
	{
		[Given(@"I have Stuff defined as following")]
		public void GivenIHaveStuffDefinedAsFollowing (Table table)
		{
			Instance.Is (table.As<Stuff> ());
		}

		[Given(@"I have lots of Stuff defined as following")]
		public void GivenIHaveLotsOfStuffDefinedAsFollowing (Table table)
		{
			Instance.Is (table.AsEnumerable<Stuff> ());
		}

		[Then(@"my Stuff values are as following")]
		public void ThenMyStuffValuesAreAsFollowing (Table table)
		{
			Assert.That (Instance.Of<Stuff> (), Is.Not.Null);
			Assert.That (Instance.Of<Stuff> (), Is.EqualTo (table.As<Stuff> ()).Using (Properties.StrictComparison));
		}

		[Then(@"my Stuff values contains the following")]
		public void ThenMyStuffValuesContainsTheFollowing (Table table)
		{
			Assert.That (Instance.Of<Stuff> (), Is.EqualTo (table).Using (Properties.LooseComparison));
		}

		[Then(@"my Stuff values are NOT as following")]
		public void ThenMyStuffValuesAreNotAsFollowing (Table table)
		{
			Assert.That (Instance.Of<Stuff> (), Is.Not.EqualTo (table.As<Stuff> ()).Using (Properties.StrictComparison));
		}

		[Then(@"my Stuff values does NOT contain the following")]
		public void ThenMyStuffValuesDoesNotContainTheFollowing (Table table)
		{
			Assert.That (Instance.Of<Stuff> (), Is.Not.EqualTo (table).Using (Properties.LooseComparison));
		}

		[Then(@"all my Stuff values are strictly equal to the following")]
		public void ThenAllMyStuffValuesAreStrictlyEqualToFollowing (Table table)
		{
			Assert.That (Instance.Of<IEnumerable<Stuff>> (), Is.EqualTo (table.AsEnumerable<Stuff> ()).Using (Properties.StrictComparison));
		}

		[Then(@"all my Stuff values are NOT strictly equal to the following")]
		public void ThenAllMyStuffValuesAreNotStrictlyEqualToFollowing (Table table)
		{
			Assert.That (Instance.Of<IEnumerable<Stuff>> (), Is.Not.EqualTo (table.AsEnumerable<Stuff> ()).Using (Properties.StrictComparison));
		}

		[Then(@"all my Stuff values are strictly equivalent to the following")]
		public void ThenAllMyStuffValuesAreStrictlyEquivalentToFollowing (Table table)
		{
			Assert.That (Instance.Of<IEnumerable<Stuff>> (), Is.EquivalentTo (table.AsEnumerable<Stuff> ()).Using (Properties.StrictComparison));
		}

		[Then(@"all my Stuff values are NOT strictly equivalent to the following")]
		public void ThenAllMyStuffValuesAreNotStrictlyEquivalentToFollowing (Table table)
		{
			Assert.That (Instance.Of<IEnumerable<Stuff>> (), Is.Not.EquivalentTo (table.AsEnumerable<Stuff> ()).Using (Properties.StrictComparison));
		}

		[Then(@"all my Stuff values are loosely equal to the following")]
		public void ThenAllMyStuffValuesAreLooselyEqualToFollowing (Table table)
		{
			Assert.That (Instance.Of<IEnumerable<Stuff>> (), Is.EqualTo (table.AsEnumerable()).Using (Properties.LooseComparison));
		}

		[Then(@"all my Stuff values are NOT loosely equal to the following")]
		public void ThenAllMyStuffValuesAreNotLooselyEqualToFollowing (Table table)
		{
			Assert.That (Instance.Of<IEnumerable<Stuff>> (), Is.Not.EqualTo (table.AsEnumerable()).Using (Properties.LooseComparison));
		}

		[Then(@"all my Stuff values are loosely equivalent to the following")]
		public void ThenAllMyStuffValuesAreLooselyEquivalentToFollowing (Table table)
		{
			Assert.That (Instance.Of<IEnumerable<Stuff>> (), Is.EquivalentTo (table.AsEnumerable()).Using (Properties.LooseComparison));
		}

		[Then(@"all my Stuff values are NOT loosely equivalent to the following")]
		public void ThenAllMyStuffValuesAreNotLooselyEquivalentToFollowing (Table table)
		{
			Assert.That (Instance.Of<IEnumerable<Stuff>> (), Is.Not.EquivalentTo (table.AsEnumerable()).Using (Properties.LooseComparison));
		}

		[Then(@"I get an exception '(.*)'")]
		public void ThenIGetAnException (string message)
		{
			Assert.That (Instance.Of<Exception> (), Is.Not.Null);
			Assert.That (Instance.Of<Exception> ().Message, Contains.Substring (message));
		}

		[Then(@"I do not get any exception")]
		public void ThenIDoNotGetAnException ()
		{
			Assert.That (Instance.Of<Exception> (), Is.Null);
		}
	}
}

