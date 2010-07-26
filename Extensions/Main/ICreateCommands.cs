using System;
namespace Oaz.SpecFlowHelpers
{
	public interface ICreateCommands
	{
		Command<T> Command<T>(string text);
	}
}

