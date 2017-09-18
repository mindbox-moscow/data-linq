using System.Data.Common;

namespace System.Data.Linq.SqlClient
{
    internal class SqlCommandBuilder
	{
		private const string QuotePrefix = "[";
		private const string QuoteSuffix = "]";

		public string QuoteIdentifier(string unquotedIdentifier)
		{
			if (unquotedIdentifier == null)
				throw ADP.ArgumentNull(nameof(unquotedIdentifier));

			ConsistentQuoteDelimiters(QuotePrefix, QuoteSuffix);
			return ADP.BuildQuotedString(QuotePrefix, QuoteSuffix, unquotedIdentifier);
		}
		
		private void ConsistentQuoteDelimiters(string quotePrefix, string quoteSuffix)
		{
			if ("\"" == quotePrefix && "\"" != quoteSuffix || "[" == quotePrefix && "]" != quoteSuffix)
				throw ADP.InvalidPrefixSuffix();
		}
	}
}
