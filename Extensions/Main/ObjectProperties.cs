using System;
using System.Collections;
namespace Oaz.SpecFlowHelpers
{
	public static class Properties
	{
		public static IEqualityComparer StrictComparison
		{
            get
            {
                return new PropertiesComparer() {IsStrict=true};
            }
		}
		
		public static IEqualityComparer LooseComparison
		{
            get
            {
                return new PropertiesComparer() {IsStrict=false};
            }
		}
	}
	
	public class PropertiesComparer : IEqualityComparer
	{
		public bool IsStrict {
			get;
			set;
		}
		
		public new bool Equals (object x, object y)
		{
			var xValues = x.PropertiesAsStringDictionary();
			var yValues = y.PropertiesAsStringDictionary();
			if(IsStrict && xValues.Count != yValues.Count)
				return false;
			if( xValues.Count < yValues.Count )
			{
				var tmp = xValues;
				xValues = yValues;
				yValues = tmp;
			}
            foreach (var yValue in yValues)
            {
				if(xValues[yValue.Key] != yValue.Value)
					return false;
            }
			return true;
		}

		public int GetHashCode (object obj)
		{
			throw new NotImplementedException ();
		}

	}
}

