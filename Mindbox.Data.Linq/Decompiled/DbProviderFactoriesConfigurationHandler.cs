using System.Configuration;
using System.Globalization;
using System.Xml;

namespace System.Data.Common
{
	/// <summary>This type supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
	public class DbProviderFactoriesConfigurationHandler
	{
		internal const string sectionName = "system.data";
		internal const string providerGroup = "DbProviderFactories";
		internal const string odbcProviderName = "Odbc Data Provider";
		internal const string odbcProviderDescription = ".Net Framework Data Provider for Odbc";
		internal const string oledbProviderName = "OleDb Data Provider";
		internal const string oledbProviderDescription = ".Net Framework Data Provider for OleDb";
		internal const string oracleclientProviderName = "OracleClient Data Provider";
		internal const string oracleclientProviderNamespace = "System.Data.OracleClient";
		internal const string oracleclientProviderDescription = ".Net Framework Data Provider for Oracle";
		internal const string sqlclientProviderName = "SqlClient Data Provider";
		internal const string sqlclientProviderDescription = ".Net Framework Data Provider for SqlServer";
		internal const string sqlclientPartialAssemblyQualifiedName = "System.Data.SqlClient.SqlClientFactory, System.Data,";
		internal const string oracleclientPartialAssemblyQualifiedName = "System.Data.OracleClient.OracleClientFactory, System.Data.OracleClient,";

		internal static DataTable CreateProviderDataTable()
		{
			DataColumn dataColumn1 = new DataColumn("Name", typeof(string));
			dataColumn1.ReadOnly = true;
			DataColumn dataColumn2 = new DataColumn("Description", typeof(string));
			dataColumn2.ReadOnly = true;
			DataColumn dataColumn3 = new DataColumn("InvariantName", typeof(string));
			dataColumn3.ReadOnly = true;
			DataColumn dataColumn4 = new DataColumn("AssemblyQualifiedName", typeof(string));
			dataColumn4.ReadOnly = true;
			DataColumn[] dataColumnArray = new DataColumn[1]
			{
				dataColumn3
			};
			DataColumn[] columns = new DataColumn[4]
			{
				dataColumn1,
				dataColumn2,
				dataColumn3,
				dataColumn4
			};
			DataTable dataTable = new DataTable("DbProviderFactories");
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.AddRange(columns);
			dataTable.PrimaryKey = dataColumnArray;
			return dataTable;
		}
	}
}
