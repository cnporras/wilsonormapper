using System;
using NUnit.Framework;
using WilsonORWrapper.Logging;
using WilsonORWrapper.Logging.NLog;

namespace WilsonORWrapper.Tests
{
	[TestFixture]
	public class NLogLoggerTests : AbstractLoggerTests
	{
		public override ILoggerFactory GetLoggerFactory()
		{
			return new NLogFactory();
		}
	}
}
