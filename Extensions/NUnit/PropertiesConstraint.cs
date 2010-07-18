using System;
using NUnit.Framework.Constraints;
using System.Reflection;

namespace Oaz.SpecFlowHelpers.NUnit
{
	public class PropertiesAreEquivalentConstraint : Constraint
	{
		public object Expected {get;set;}
		private string _expectedMessage;
		private string _actualMessage;
		
	    public override bool Matches( object actual )
		{
            foreach (PropertyInfo property in Expected.GetType().GetProperties())
            {
                object expectedValue = property.GetValue(Expected, null);
                object actualValue = property.GetValue(actual, null);

				if(expectedValue.Equals(actualValue))
					continue;
				
				_expectedMessage = string.Format(
					"{0} for property {1} in {2}",
				    expectedValue,
				    property.Name,
				    actual.GetType().Name
				);
				_actualMessage = string.Format(
					"{0}",
				    actualValue
				);
				return false;
            }
			return true;
		}
		
	    public override void WriteDescriptionTo( MessageWriter writer )
		{
			writer.Write(_expectedMessage);
		}
		
		public override void WriteActualValueTo( MessageWriter writer )
		{
			writer.Write(_actualMessage);
		}
	}
	
	public static class Properties
	{
		public static PropertiesAreEquivalentConstraint PropertiesEquivalentTo(this ConstraintExpression constraint, object obj)
		{
			return new PropertiesAreEquivalentConstraint() { Expected = obj };
		}
	}
}

