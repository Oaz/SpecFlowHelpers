using System;
using NUnit.Framework;
using Oaz.SpecFlowHelpers;
namespace Specs.Oaz.SpecFlowHelpers
{
	[TestFixture]
	public class Syntaxes
	{
		class X
		{
			public void JustDoIt(string s1, int i1, string s2, int i2) {}
			public int JustDoItAgain(string s1, int i1, string s2) { return 0; }
			public int GetIt() { return 0; }
			public void DoIt() {}
		}
		
		[Test]
		public void NaturalWithNoReturnValue ()
		{
			var cmd = With.Syntax.Natural.Command<X>("just do it \"foo foo\" 3 \"bar bar\" 8");
			Assert.That( cmd.Method.DeclaringType, Is.EqualTo(typeof(X)) );
			Assert.That( cmd.Method.Name, Is.EqualTo("JustDoIt") );
			Assert.That( cmd.Parameters.Count, Is.EqualTo(4) );
			Assert.That( cmd.Parameters[0], Is.EqualTo("foo foo") );
			Assert.That( cmd.Parameters[1], Is.EqualTo(3) );
			Assert.That( cmd.Parameters[2], Is.EqualTo("bar bar") );
			Assert.That( cmd.Parameters[3], Is.EqualTo(8) );
		}
		
		[Test]
		public void NaturalWithReturnValue ()
		{
			var cmd = With.Syntax.Natural.Command<X>("just do it again \"foo foo\" 3 \"bar bar\" => 25");
			Assert.That( cmd.Method.DeclaringType, Is.EqualTo(typeof(X)) );
			Assert.That( cmd.Method.Name, Is.EqualTo("JustDoItAgain") );
			Assert.That( cmd.Parameters.Count, Is.EqualTo(3) );
			Assert.That( cmd.Parameters[0], Is.EqualTo("foo foo") );
			Assert.That( cmd.Parameters[1], Is.EqualTo(3) );
			Assert.That( cmd.Parameters[2], Is.EqualTo("bar bar") );
			Assert.That( cmd.Returns, Is.EqualTo(25) );
		}
		
		[Test]
		public void NaturalWithUnsetReturnValue ()
		{
			var cmd = With.Syntax.Natural.Command<X>("just do it again \"foo foo\" 3 \"bar bar\"");
			Assert.That( cmd.Method.DeclaringType, Is.EqualTo(typeof(X)) );
			Assert.That( cmd.Method.Name, Is.EqualTo("JustDoItAgain") );
			Assert.That( cmd.Parameters.Count, Is.EqualTo(3) );
			Assert.That( cmd.Parameters[0], Is.EqualTo("foo foo") );
			Assert.That( cmd.Parameters[1], Is.EqualTo(3) );
			Assert.That( cmd.Parameters[2], Is.EqualTo("bar bar") );
		}
		
		[Test]
		public void NaturalWithNoParameterAndReturnValue ()
		{
			var cmd = With.Syntax.Natural.Command<X>("get it => 25");
			Assert.That( cmd.Method.DeclaringType, Is.EqualTo(typeof(X)) );
			Assert.That( cmd.Method.Name, Is.EqualTo("GetIt") );
			Assert.That( cmd.Parameters.Count, Is.EqualTo(0) );
			Assert.That( cmd.Returns, Is.EqualTo(25) );
		}
		
		[Test]
		public void NaturalWithNoParameterAndUnsetReturnValue ()
		{
			var cmd = With.Syntax.Natural.Command<X>("get it");
			Assert.That( cmd.Method.DeclaringType, Is.EqualTo(typeof(X)) );
			Assert.That( cmd.Method.Name, Is.EqualTo("GetIt") );
			Assert.That( cmd.Parameters.Count, Is.EqualTo(0) );
		}
		
		[Test]
		public void NaturalWithNoParameterAndNoReturnValue ()
		{
			var cmd = With.Syntax.Natural.Command<X>("do it");
			Assert.That( cmd.Method.DeclaringType, Is.EqualTo(typeof(X)) );
			Assert.That( cmd.Method.Name, Is.EqualTo("DoIt") );
			Assert.That( cmd.Parameters.Count, Is.EqualTo(0) );
		}
	}
}

