using System;
using NUnit.Framework;
using WilsonORWrapper.Logging;

namespace WilsonORWrapper.Tests
{
	[TestFixture]
	public class EventLoggerTests : AbstractLoggerTests
	{
		public override ILoggerFactory GetLoggerFactory()
		{
			return new EventLoggerFactory();
		}
	}
}
