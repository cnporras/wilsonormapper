using System;
using NUnit.Framework;
using WilsonORWrapper.Logging;

namespace WilsonORWrapper.Tests
{
	[TestFixture]
	public class NullLoggerTests : AbstractLoggerTests
	{
		public override ILoggerFactory GetLoggerFactory()
		{
			return new NullLoggerFactory();
		}
	}
}
