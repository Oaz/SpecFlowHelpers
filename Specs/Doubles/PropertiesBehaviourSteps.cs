using System;

using TechTalk.SpecFlow;
using NUnit.Framework;
using Oaz.SpecFlowHelpers;
using Oaz.SpecFlowHelpers.Doubles;
using System.Collections.Generic;

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
		[Given(@"I have Stuff defined as following")]
		public void GivenIHaveStuffDefinedAsFollowing (Table table)
		{
			Instance.Of<Stuff>().IsTestDouble().Setup(table.AsPropertiesBehaviour<Stuff> ());
		}

		[Then(@"my Stuff values are as following")]
		public void ThenMyStuffValuesAreAsFollowing (Table table)
		{
			Assert.That (Instance.Of<Stuff> ().Object, Is.Not.Null);
			Assert.That (Instance.Of<Stuff> ().Object, Is.EqualTo (table.AsTestDoubleWithProperties<Stuff> ()).Using (Properties.StrictComparison));
		}
		
		[Then(@"my Stuff values are NOT as following")]
		public void ThenMyStuffValuesAreNotAsFollowing (Table table)
		{
			Assert.That (Instance.Of<Stuff> ().Object, Is.Not.Null);
			Assert.That (Instance.Of<Stuff> ().Object, Is.Not.EqualTo (table.AsTestDoubleWithProperties<Stuff> ()).Using (Properties.StrictComparison));
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
		
		[Given(@"I have lots of Stuff defined as following")]
		public void GivenIHaveLotsOfStuffDefinedAsFollowing (Table table)
		{
			Instance.Of<IEnumerable<Stuff>> ().Is (table.AsTestDoubleWithPropertiesEnumerable<Stuff> ());
		}
		
		[Then(@"all my Stuff values are strictly equal to the following")]
		public void ThenAllMyStuffValuesAreStrictlyEqualToFollowing (Table table)
		{
			Assert.That (Instance.Of<IEnumerable<Stuff>> ().Object, Is.EqualTo (table.AsTestDoubleWithPropertiesEnumerable<Stuff> ()).Using (Properties.StrictComparison));
		}

		[Then(@"all my Stuff values are NOT strictly equal to the following")]
		public void ThenAllMyStuffValuesAreNotStrictlyEqualToFollowing (Table table)
		{
			Assert.That (Instance.Of<IEnumerable<Stuff>> ().Object, Is.Not.EqualTo (table.AsTestDoubleWithPropertiesEnumerable<Stuff> ()).Using (Properties.StrictComparison));
		}

		[Then(@"all my Stuff values are strictly equivalent to the following")]
		public void ThenAllMyStuffValuesAreStrictlyEquivalentToFollowing (Table table)
		{
			Assert.That (Instance.Of<IEnumerable<Stuff>> ().Object, Is.EquivalentTo (table.AsTestDoubleWithPropertiesEnumerable<Stuff> ()).Using (Properties.StrictComparison));
		}

		[Then(@"all my Stuff values are NOT strictly equivalent to the following")]
		public void ThenAllMyStuffValuesAreNotStrictlyEquivalentToFollowing (Table table)
		{
			Assert.That (Instance.Of<IEnumerable<Stuff>> ().Object, Is.Not.EquivalentTo (table.AsTestDoubleWithPropertiesEnumerable<Stuff> ()).Using (Properties.StrictComparison));
		}

		[Then(@"all my Stuff values are loosely equal to the following")]
		public void ThenAllMyStuffValuesAreLooselyEqualToFollowing (Table table)
		{
			Assert.That (Instance.Of<IEnumerable<Stuff>> ().Object, Is.EqualTo (table.AsEnumerable()).Using (Properties.LooseComparison));
		}

		[Then(@"all my Stuff values are NOT loosely equal to the following")]
		public void ThenAllMyStuffValuesAreNotLooselyEqualToFollowing (Table table)
		{
			Assert.That (Instance.Of<IEnumerable<Stuff>> ().Object, Is.Not.EqualTo (table.AsEnumerable()).Using (Properties.LooseComparison));
		}

		[Then(@"all my Stuff values are loosely equivalent to the following")]
		public void ThenAllMyStuffValuesAreLooselyEquivalentToFollowing (Table table)
		{
			Assert.That (Instance.Of<IEnumerable<Stuff>> ().Object, Is.EquivalentTo (table.AsEnumerable()).Using (Properties.LooseComparison));
		}

		[Then(@"all my Stuff values are NOT loosely equivalent to the following")]
		public void ThenAllMyStuffValuesAreNotLooselyEquivalentToFollowing (Table table)
		{
			Assert.That (Instance.Of<IEnumerable<Stuff>> ().Object, Is.Not.EquivalentTo (table.AsEnumerable()).Using (Properties.LooseComparison));
		}
	}
}

