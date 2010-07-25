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
	}
	
	[Binding]
	public class VerificationSteps
	{
        [Given(@"a verifiable receiver")]
        public void GivenAVerifiableReceiver()
        {
            Instance.Of<Receiver>().IsVerifiable();
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
		
        private static CommandSyntax GetSyntax (string syntaxName)
		{
			var syntaxProperty = With.Syntax.GetType().GetProperty(syntaxName);
			return syntaxProperty.GetValue(With.Syntax, null) as CommandSyntax;
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

