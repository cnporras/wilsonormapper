using System;
using System.Runtime.Serialization;

namespace WilsonORWrapper
{
	/// <summary>
	/// Exception thrown when the mappings file can not be read.
	/// </summary>
	[Serializable]
	public class CacheProviderException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CacheProviderException"/> class.
		/// </summary>
		public CacheProviderException()
			: base()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CacheProviderException"/> class.
		/// </summary>
		/// <param name="message">The message that describes the error. </param>
		public CacheProviderException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CacheProviderException"/> class.
		/// </summary>
		/// <param name="innerException">
		/// The exception that is the cause of the current exception. If the innerException parameter 
		/// is not a null reference, the current exception is raised in a catch block that handles 
		/// the inner exception.
		/// </param>
		public CacheProviderException(Exception innerException)
			: base(innerException.Message, innerException)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CacheProviderException"/> class.
		/// </summary>
		/// <param name="message">The message that describes the error. </param>
		/// <param name="innerException">
		/// The exception that is the cause of the current exception. If the innerException parameter 
		/// is not a null reference, the current exception is raised in a catch block that handles 
		/// the inner exception.
		/// </param>
		public CacheProviderException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CacheProviderException"/> class
		/// with serialized data.
		/// </summary>
		/// <param name="info">
		/// The <see cref="SerializationInfo"/> that holds the serialized object 
		/// data about the exception being thrown.
		/// </param>
		/// <param name="context">
		/// The <see cref="StreamingContext"/> that contains contextual information about the source or destination.
		/// </param>
		protected CacheProviderException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
