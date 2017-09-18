using System.Configuration;
using System.Text;

namespace System.Data.Common
{
    internal static class ADP
	{
		internal static string BuildQuotedString(string quotePrefix, string quoteSuffix, string unQuotedString)
		{
			var stringBuilder = new StringBuilder();
			if (!string.IsNullOrEmpty(quotePrefix))
				stringBuilder.Append(quotePrefix);
			if (!string.IsNullOrEmpty(quoteSuffix))
			{
				stringBuilder.Append(unQuotedString.Replace(quoteSuffix, quoteSuffix + quoteSuffix));
				stringBuilder.Append(quoteSuffix);
			}
			else
				stringBuilder.Append(unQuotedString);
			return stringBuilder.ToString();
		}

		internal static ArgumentException InvalidPrefixSuffix()
		{
			var argumentException = new ArgumentException("Specified QuotePrefix and QuoteSuffix values do not match.");
			TraceExceptionAsReturnValue(argumentException);
			return argumentException;
		}

		internal static ArgumentNullException ArgumentNull(string parameter)
		{
			var argumentNullException = new ArgumentNullException(parameter);
			TraceExceptionAsReturnValue(argumentNullException);
			return argumentNullException;
		}

		internal static void TraceExceptionAsReturnValue(Exception e)
		{
			// TODO: too much of decompile
			// ADP.TraceException("<comm.ADP.TraceException|ERR|THROW> '%ls'\n", e);
		}

		internal static InvalidOperationException ConfigProviderInvalid()
		{
			return InvalidOperation(nameof (ConfigProviderInvalid));
		}

		internal static ConfigurationException ConfigProviderNotInstalled()
		{
			return Configuration(nameof(ConfigProviderNotInstalled));
		}

		internal static ConfigurationException ConfigProviderMissing()
		{
			return Configuration(nameof(ConfigProviderMissing));
		}

		internal static ConfigurationException Configuration(string message)
		{
			var configurationException = (ConfigurationException)new ConfigurationErrorsException(message);
			TraceExceptionAsReturnValue(configurationException);
			return configurationException;
		}

		internal static ArgumentException ConfigProviderNotFound()
		{
			return Argument(nameof(ConfigProviderNotFound));
		}

		internal static ArgumentException Argument(string error)
		{
			var argumentException = new ArgumentException(error);
			TraceExceptionAsReturnValue(argumentException);
			return argumentException;
		}

		internal static InvalidOperationException InvalidOperation(string error)
		{
			var operationException = new InvalidOperationException(error);
			TraceExceptionAsReturnValue(operationException);
			return operationException;
		}

		internal static void CheckArgumentLength(string value, string parameterName)
		{
			CheckArgumentNull(value, parameterName);
			if (value.Length == 0)
				throw ADP.Argument($"ADP_EmptyString {parameterName}");
		}

		internal static void CheckArgumentNull(object value, string parameterName)
		{
			if (value == null)
				throw ArgumentNull(parameterName);
		}

		internal static bool IsEmpty(string str)
		{
			if (str != null)
				return str.Length == 0;
			return true;
		}
	}
}
