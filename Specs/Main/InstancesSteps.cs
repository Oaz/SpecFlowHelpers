using System;
using TechTalk.SpecFlow;
using NUnit.Framework;
using Oaz.SpecFlowHelpers;

namespace Specs.Oaz.SpecFlowHelpers
{
	class Foo
	{
		public string Name {get;set;}
	}
	
	class FooBar : Foo
	{
	}
	
	class Bar
	{
		public string Name {get;set;}
	}
	
	[Binding]
	public class InstancesSteps
	{
        [Given(@"I have a unique instance of Foo")]
        public void GivenIHaveAUniqueInstanceOfFoo()
        {
			Instance.Is( new Foo() { Name="noname" } );
        }

        [Given(@"I have an instance of Foo named (.*)")]
        public void GivenIHaveAnInstanceOfFooNamed(string name)
        {
			Instance.Named(name).Is( new Foo() { Name=name } );
        }

        [Given(@"I have an instance of Bar named (.*)")]
        public void GivenIHaveAnInstanceOfBarNamed(string name)
        {
			Instance.Named(name).Is( new Bar() { Name=name } );
        }

        [When(@"I do nothing")]
        public void WhenIDoNothing()
        {
        }

        [Then(@"I have the same unique instance of Foo")]
        public void ThenIHaveTheSameUniqueInstanceOfFoo()
        {
            Assert.That( Instance.Of<Foo>().Name, Is.EqualTo("noname") );
        }

        [Then(@"I have the same instance of Foo named (.*)")]
        public void ThenIHaveTheSameInstanceOfFoo(string name)
        {
            Assert.That( Instance.Named(name).Of<Foo>().Name, Is.EqualTo(name) );
        }

        [Then(@"I have the same instance of Bar named (.*)")]
        public void ThenIHaveTheSameInstanceOfBar(string name)
        {
            Assert.That( Instance.Named(name).Of<Bar>().Name, Is.EqualTo(name) );
        }

        [Then(@"I do not have an instance of Foo named (.*)")]
        public void ThenDoNotHaveAnInstanceOfFoo(string name)
       	{
            Assert.That( Instance.Named(name).Of<Foo>(), Is.Null );
        }

        [Then(@"do not have an instance of FooBar named (.*) even if FooBar inherits from Foo")]
        public void ThenDoNotHaveAnInstanceOfFooBarNamedEvenIfFooBarInheritsFromFoo(string name)
       	{
            Assert.That( Instance.Named(name).Of<FooBar>(), Is.Null );
        }
	}
}

