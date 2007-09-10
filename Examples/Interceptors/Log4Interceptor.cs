using System;
using System.Data;
using Wilson.ORMapper;

[assembly: log4net.Config.DOMConfigurator(Watch=true)]

namespace Wilson.ORMapper.Demo
{
	public class Log4Interceptor : IInterceptCommand
	{
		private log4net.ILog log = null;

		public Log4Interceptor() {
			this.log = log4net.LogManager.GetLogger(typeof(Log4Interceptor));
		}

		public void InterceptCommand(Guid transactionId, Type entityType, CommandInfo commandInfo, IDbCommand dbCommand) {
			string message = transactionId.ToString() + " - " + commandInfo.ToString();
			if (dbCommand != null) {
				message += ": " + dbCommand.CommandText;
				for (int index = 0; index < dbCommand.Parameters.Count; index++) {
					if (index == 0) message += "\r\n  ";
					IDbDataParameter parameter = dbCommand.Parameters[index] as IDbDataParameter;
					message += parameter.ParameterName + " = " + parameter.Value + ", ";
				}
			}
			this.log.Info(message);
		}
	}
}
