using System;

using TechTalk.SpecFlow;
using Oaz.SpecFlowHelpers;
using Oaz.SpecFlowHelpers.Doubles;
using NUnit.Framework;
using System.Linq;
using System.Linq.Expressions;

namespace Specs.Oaz.SpecFlowHelpers.Doubles
{
	
	public interface Receiver
	{
		void DoSomething(string name);
		void PutTwoNumbers(int n1, int n2);
		int ComputeTheMagicNumberFrom(int val);
	}
	
	[Binding]
	public class VerificationSteps
	{
		[Then(@"I get an exception: (.*)")]
		public void ThenIGetAnException (string message)
		{
			Assert.That (Instance.Of<Exception> ().Object, Is.Not.Null);
			Assert.That (Instance.Of<Exception> ().Object.Message, Contains.Substring (message));
		}
		
        [Given(@"a spy receiver without behaviour")]
        public void GivenAVerifiableReceiver()
        {
            Instance.Of<Receiver>().IsSpy();
			Assert.That( Instance.Of<Receiver>().Object, Is.Not.Null, "No instance of receiver created" );
			Assert.That( Instance.Of<Exception>().Object, Is.Null );
        }
		
        [Given(@"a spy receiver with the following behaviour")]
        public void GivenTheSpyReceiverImplementsTheFollowingBehaviour(Table table)
        {
            Instance.Of<Receiver>().IsSpy( table.AsSpyBehaviour<Receiver>(With.Syntax.Natural) );
			Assert.That( Instance.Of<Receiver>().Object, Is.Not.Null, "No instance of receiver created" );
			Assert.That( Instance.Of<Exception>().Object, Is.Null );
        }

        [When(@"I do something with name '(.*)'")]
        public void WhenIDoSomething(string name)
        {
            Instance.Of<Receiver>().Object.DoSomething(name);
        }
		
        [When(@"I put two numbers \[([0-9]+),([0-9]+)]")]
        public void WhenIPutTwoNumbers(int n1, int n2)
        {
            Instance.Of<Receiver>().Object.PutTwoNumbers(n1,n2);
        }
		
        [When(@"I compute the magic number from ([0-9]+) and put both numbers")]
        public void WhenIComputeTheMagicNumberFromAndPutBothNumbers(int val)
        {
            int magic = Instance.Of<Receiver>().Object.ComputeTheMagicNumberFrom(val);
            Instance.Of<Receiver>().Object.PutTwoNumbers(val,magic);
        }
		
        private static ICreateCommands GetSyntax (string syntaxName)
		{
			var syntaxProperty = With.Syntax.GetType().GetProperty(syntaxName);
			return syntaxProperty.GetValue(With.Syntax, null) as ICreateCommands;
		}

        [Then(@"the receiver got in (.*) syntax the following commands")]
        public void ThenTheReceiverGotTheFollowingCommands(string syntax, Table table)
        {
 			Assert.That (Instance.Of<Receiver> ().Commands(), Is.EqualTo (table.AsCommands<Receiver> (GetSyntax (syntax))));
        }

        [Then(@"the receiver did not get in (.*) syntax the following commands")]
        public void ThenTheReceiverDidNotGetTheFollowingCommands(string syntax, Table table)
        {
 			Assert.That (Instance.Of<Receiver> ().Commands(), Is.Not.EqualTo (table.AsCommands<Receiver> (GetSyntax (syntax))));
        }
	}
}

