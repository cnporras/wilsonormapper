using System;
using NUnit.Framework;
using WilsonORWrapper.Logging;
using WilsonORWrapper.Logging.NLog;
using WilsonORWrapper.Logging.Log4Net;

namespace WilsonORWrapper.Tests
{
	[TestFixture]
	public class LoggerFactoryTests
	{
		[Test]
		public void NullLoggerTests()
		{
			ILoggerFactory factory = new NullLoggerFactory();
			Assert.IsNotNull(factory);
			ILogger logger = factory.CreateLogger();
			Assert.IsNotNull(logger);
		}

		[Test]
		public void NLogLoggerTests()
		{
			ILoggerFactory factory = new NLogFactory();
			Assert.IsNotNull(factory);
			ILogger logger = factory.CreateLogger();
			Assert.IsNotNull(logger);
		}

		[Test]
		public void Log4NetLoggerTests()
		{
			ILoggerFactory factory = new Log4NetFactory();
			Assert.IsNotNull(factory);
			ILogger logger = factory.CreateLogger();
			Assert.IsNotNull(logger);
		}
	}
}
