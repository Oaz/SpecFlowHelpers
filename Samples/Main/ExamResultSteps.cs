using System;
using TechTalk.SpecFlow;
using NUnit.Framework;
using Oaz.SpecFlowHelpers;

namespace Samples.Oaz.SpecFlowHelpers
{
	class ExamResults
	{
		public double Litterature { get; set; } 
		public double Mathematics { get; set; } 
		public double Economics { get; set; } 
		
		public double Average
		{
			get
			{
				return (Litterature+Mathematics+Economics) / 3.0;
			}
		}
		
		public bool IsGood
		{
			get
			{
				return Average >= 10.0;
			}
		}
	}
	
	[Binding]
	public class ExamResultSteps
	{		
        [When(@"the scores are")]
        public void WhenTheScoresAre(Table table)
        {
			Instance.Of<ExamResults>().Is( table.As<ExamResults>() );
		}

        [Then(@"I get my degree")]
        public void ThenIGetMyDegree()
        {
            Assert.True( Instance.Of<ExamResults>().Object.IsGood );
        }

        [Then(@"I do not get my degree")]
        public void ThenIDoNotGetMyDegree()
        {
            Assert.False( Instance.Of<ExamResults>().Object.IsGood );
        }
	}
}

