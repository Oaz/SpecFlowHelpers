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

		[Then(@"all my Stuff values are as following")]
		public void ThenAllMyStuffValuesAreAsFollowing (Table table)
		{
			var actual = Instance.Of<IEnumerable<Stuff>> ();
			var expected = table.AsEnumerable<Stuff> ();
			var actualRows = actual.GetEnumerator ();
			var expectedRows = expected.GetEnumerator ();
			int rowcount = 0;
			while (actualRows.MoveNext () && expectedRows.MoveNext ()) {
				//Assert.That( actualRows.Current, Has.All.PropertiesEquivalentTo(expectedRows.Current), string.Format("At row {0}", rowcount) );
				rowcount++;
			}
			Assert.That (actual.Count (), Is.EqualTo (rowcount), "More rows than expected");
			Assert.That (expected.Count (), Is.EqualTo (rowcount), "Less rows than expected");
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

