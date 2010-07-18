using System;
using System.Linq;
using TechTalk.SpecFlow;
using NUnit.Framework;
using Oaz.SpecFlowHelpers;
using Oaz.SpecFlowHelpers.NUnit;
using System.Collections.Generic;

namespace Specs.Oaz.SpecFlowHelpers
{
	class Stuff
	{
		public string  AString   {get;set;}
		public int     AnInt     {get;set;}
		public double  ADouble   {get;set;}
		public DateTime ADateTime {get;set;}
	}
	
	[Binding]
	public class TablesSteps
	{
        [Given(@"I have Stuff defined as following")]
        public void GivenIHaveStuffDefinedAsFollowing(Table table)
        {
			try
			{				
				Instance.Is( table.As<Stuff>() );
			}
			catch(Exception e)
			{
				Instance.Is(e);
			}
        }
		
        [Given(@"I have lots of Stuff defined as following")]
        public void GivenIHaveLotsOfStuffDefinedAsFollowing(Table table)
        {
			try
			{				
				Instance.Is( table.AsEnumerable<Stuff>() );
			}
			catch(Exception e)
			{
				Instance.Is(e);
			}
        }
		
        [Then(@"my Stuff values are as following")]
        public void ThenMyStuffValuesAreAsFollowing(Table table)
        {
 			try
			{				
            	Assert.That( Instance.Of<Stuff>(), Has.All.PropertiesEquivalentTo(table.As<Stuff>()) );
			}
			catch(Exception e)
			{
				Instance.Is(e);
			}
       }
		
        [Then(@"all my Stuff values are as following")]
        public void ThenAllMyStuffValuesAreAsFollowing(Table table)
        {
			var actual = Instance.Of<IEnumerable<Stuff>>();
			var expected = table.AsEnumerable<Stuff>();
			var actualRows = actual.GetEnumerator();
			var expectedRows = expected.GetEnumerator();
			int rowcount = 0;
			while(actualRows.MoveNext() && expectedRows.MoveNext())
			{
           		Assert.That( actualRows.Current, Has.All.PropertiesEquivalentTo(expectedRows.Current), string.Format("At row {0}", rowcount) );
				rowcount++;
			}
            Assert.That( actual.Count(), Is.EqualTo(rowcount), "More rows than expected" );
            Assert.That( expected.Count(), Is.EqualTo(rowcount), "Less rows than expected" );
       }
		
        [Then(@"I get an exception '(.*)'")]
        public void ThenIGetAnException(string message)
        {
            Assert.That( Instance.Of<Exception>().Message, Is.EqualTo(message.Replace("\\n","\n")) );
        }
		
        [Then(@"I do not get any exception")]
        public void ThenIDoNotGetAnException()
        {
            Assert.That( Instance.Of<Exception>(), Is.Null );
        }
	}
}

