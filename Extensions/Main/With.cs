using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Oaz.SpecFlowHelpers
{
	public static class With
	{
		static With()
		{
			Syntax = new SyntaxFactory();
		}
		
		public static SyntaxFactory Syntax { get; private set; }
	}
	
	public class SyntaxFactory
	{
		public SyntaxFactory ()
		{
			Natural = new CommandSyntax ()
			{
				MethodRegex = new Regex("^(.*?) [\"0-9]"),
				ParameterRegex = new Regex(" (\"(.*?)\"|[0-9]+)"),
				MethodNameBuilder = s=>s.AsMethodName()
			};
			
			Functions = new CommandSyntax ()
			{
				MethodRegex = new Regex("(.*)\\("),
				ParameterRegex = new Regex("[,\\(]([^,\\)]*)")
			};

		}

		public CommandSyntax Natural { get; private set; }
		public CommandSyntax Functions { get; private set; }
	}
}

