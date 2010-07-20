using System;
namespace Oaz.SpecFlowHelpers
{
	public static class Tools
	{
		public static U HandleExceptionInstance<U>(Func<U> execute) where U:class
		{
			Instance.Is<Exception>(null);
			try
			{
				return execute();
			}
			catch(Exception e)
			{
				Instance.Is<Exception>(e);
				return null;
			}
		}
		
		public static T ConvertTo<T>(object val)
		{
			return (T) Convert.ChangeType(val, typeof(T));
		}
		
		public static void Check(bool condition, string message, params object[] args)
		{
			if( !condition )
				throw new ApplicationException(string.Format(message, args));
		}
	}
}

