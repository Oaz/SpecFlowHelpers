using System;
using System.Linq;

using TechTalk.SpecFlow;
using NUnit.Framework;
using Oaz.SpecFlowHelpers;
using Oaz.SpecFlowHelpers.Doubles;
using System.Reflection;

namespace Specs.Oaz.SpecFlowHelpers.Doubles
{
	[Binding]
	public class GeneralSteps
	{
        [When(@"I do nothing")]
        public void WhenIDoNothing()
        {
        }
		
		[Then(@"I get an exception: (.*)")]
		public void ThenIGetAnException (string message)
		{
			Assert.True (Instance.Of<Exception> ().Exists);
			Assert.That (Instance.Of<Exception> ().Object.Message, Contains.Substring (message));
		}

		[Then(@"I do not get any exception")]
		public void ThenIDoNotGetAnException ()
		{
			Assert.False (Instance.Of<Exception> ().Exists);
		}
		
		
		public interface IHaveDoubles
		{
			string GetName();
			int GetValue();
		}
		
        [Given(@"I have a test double named '(.*)'")]
        public void GivenIHaveATestDoubleNamedForType(string name)
        {
			Instance.Of<IHaveDoubles>().Named(name).IsTestDouble();
        }
		
        [Given(@"behaviour of method '(.*)' for '(.*)' returns '(.*)'")]
        public void GivenBehaviourForReturns(string methodName, string instanceName, string retVal)
        {
            Instance.Of<IHaveDoubles>().Named(instanceName).Setup(
			  cmd =>
			  {
				if( cmd.Method.Name == methodName )
					return Convert.ChangeType(retVal, cmd.Method.ReturnType);
				return Behaviour.NotImplemented;
			  }
			);                                            
        }
		
        [Given(@"behaviour for '(.*)' is defined as following")]
        public void GivenBehaviourForFooIsDefinedAsFollowing(string instanceName, Table table)
        {
            Instance.Of<IHaveDoubles>().Named(instanceName).Setup( table.AsCommandBehaviour<IHaveDoubles>(With.Syntax.Natural) );
        }
		
		Type _actualType;
		object _actual;
		
        [When(@"I call method '(.*)' on '(.*)'")]
        public void WhenICallMethodOnFoo(string methodName, string instanceName)
        {
			var method = typeof(IHaveDoubles).GetMethod(methodName);
			_actualType = method.ReturnType;
			
			try
			{                        
				_actual = method.Invoke(
	            	Instance.Of<IHaveDoubles>().Named(instanceName).Object,
				    null
				);
			}
			catch(TargetInvocationException e)
			{
				Instance.Of<Exception>().Is(e.InnerException);
			}
        }
		
        [Then(@"I get the value '(.*)'")]
        public void ThenIGetTheValue(string expectedValue)
        {
            Assert.That( _actual, Is.EqualTo(Convert.ChangeType(expectedValue, _actualType)) );
        }
		
        [Then(@"the value type is '(.*)'")]
        public void ThenTheValueTypeIs(string typeName)
        {
            Assert.That( _actualType.ToString(), Is.EqualTo(typeName) );
        }
	}
}

