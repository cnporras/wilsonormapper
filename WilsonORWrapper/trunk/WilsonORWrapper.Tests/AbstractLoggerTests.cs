using System;
using NUnit.Framework;
using WilsonORWrapper.Logging;

namespace WilsonORWrapper.Tests
{
	public abstract class AbstractLoggerTests
	{
		private ILoggerFactory factory;
		private ILogger logger;

		public abstract ILoggerFactory GetLoggerFactory();

		[SetUp]
		public void SetUp()
		{
			factory = GetLoggerFactory();
			Assert.IsNotNull(factory);

			logger = factory.CreateLogger();
			Assert.IsNotNull(logger);
		}

		[Test]
		public void Debug()
		{
			logger.Debug("DEBUG");
		}

		[Test]
		public void Info()
		{
			logger.Info("INFO");
		}

		[Test]
		public void Warning()
		{
			logger.Warning("WARNING");
		}

		[Test]
		public void Error()
		{
			logger.Error("ERROR");
		}
	}
}
