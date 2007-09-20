using System;
using NUnit.Framework;
using WilsonORWrapper.Logging;
using WilsonORWrapper.Logging.Log4Net;

namespace WilsonORWrapper.Tests
{
	[TestFixture]
	public class Log4NetLoggerTests : AbstractLoggerTests
	{
		public override ILoggerFactory GetLoggerFactory()
		{
			return new Log4NetFactory();
		}
	}
}
