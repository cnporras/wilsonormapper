using System;
using System.Data;
using System.Text;
using Wilson.ORMapper;
using WilsonORWrapper.Providers;

namespace WilsonORWrapper.Logging
{
	/// <summary>
	/// Formats O/R mapper commands in a format useful for logging.
	/// </summary>
	public class ORMapperInterceptor : IInterceptCommand
	{
		static private readonly char[] lineEnds = new char[] { '\r', '\n' };

		/// <summary>
		/// Initializes the class.
		/// </summary>
		public ORMapperInterceptor()
		{
		}

		/// <summary>
		/// Parses the command information provided on construction
		/// to a human-readable format.
		/// </summary>
		/// <returns></returns>
		public static string ParseCommand(CommandInfo commandInfo, IDbCommand dbCommand)
		{
			StringBuilder message = new StringBuilder();
			message.Append(commandInfo.ToString());

			if (dbCommand != null)
			{
				message.Append(": ").Append(dbCommand.CommandText.TrimEnd(lineEnds));

				for (int index = 0; index < dbCommand.Parameters.Count; index++)
				{
					IDbDataParameter parameter = dbCommand.Parameters[index] as IDbDataParameter;
					if (parameter != null)
					{
						message.Append(parameter.ParameterName).Append(" = ").Append(parameter.Value).Append(", ");
					}
				}

				if (dbCommand.Parameters.Count > 0)
				{
					message.Length -= 2;
				}
			}

			return message.ToString();
		}

		#region IInterceptCommand Members
		/// <summary>
		/// Implements the <see cref="IInterceptCommand"/> method for O/R mapper command logging.
		/// </summary>
		/// <param name="transactionId">The transaction ID.</param>
		/// <param name="entityType">The entity type.</param>
		/// <param name="commandInfo">The command info.</param>
		/// <param name="dbCommand">The database command parameters.</param>
		public void InterceptCommand(Guid transactionId, Type entityType, CommandInfo commandInfo, IDbCommand dbCommand)
		{
			if (LogProvider.Logger.IsDebugEnabled)
			{
				LogProvider.Logger.Debug(ParseCommand(commandInfo, dbCommand));
			}
		}
		#endregion
	}
}
