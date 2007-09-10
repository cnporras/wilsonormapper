using System;
using System.Data;
using System.IO;
using Wilson.ORMapper;

namespace Wilson.ORMapper.Demo
{
	public class FileInterceptor : IInterceptCommand, IDisposable
	{
		private StreamWriter file = null;
		private readonly object syncRoot = new object();

		public FileInterceptor() {
			string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DemoLogFile.txt");
			this.file = new StreamWriter(fileName, true);
		}

		public void InterceptCommand(Guid transactionId, Type entityType, CommandInfo commandInfo, IDbCommand dbCommand) {
			string message = DateTime.Now.ToString() + ": " + transactionId.ToString() + " - " + commandInfo.ToString();
			if (dbCommand != null) {
				message += ": " + dbCommand.CommandText;
				for (int index = 0; index < dbCommand.Parameters.Count; index++) {
					if (index == 0) message += "\r\n  ";
					IDbDataParameter parameter = dbCommand.Parameters[index] as IDbDataParameter;
					message += parameter.ParameterName + " = " + parameter.Value + ", ";
				}
			}
			lock (this.syncRoot) {
				file.WriteLine(message);
				file.Flush();
			}
		}

		public void Dispose() {
			if (this.file != null) {
				this.file.Close();
			}
			GC.SuppressFinalize(this);
		}

		~FileInterceptor() {
			try {
				this.Dispose();
			}
			catch {
				// Do nothing. This happens when the underlying handle has already been released in a GC cycle.
			}
		}
	}
}
