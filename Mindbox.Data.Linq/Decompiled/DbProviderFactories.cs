using System.Configuration;
using System.Data.SqlClient;
using System.Reflection;

namespace System.Data.Common
{
	/// <summary>Represents a set of static methods for creating one or more instances of <see cref="T:System.Data.Common.DbProviderFactory" /> classes.</summary>
	public static class DbProviderFactories
	{
		private static object _lockobj = new object();
		private const string AssemblyQualifiedName = "AssemblyQualifiedName";
		private const string Instance = "Instance";
		private const string InvariantName = "InvariantName";
		private const string Name = "Name";
		private const string Description = "Description";
		private static ConnectionState _initState;
		private static DataTable _providerTable;

		/// <summary>Returns an instance of a <see cref="T:System.Data.Common.DbProviderFactory" />.</summary>
		/// <param name="providerInvariantName">Invariant name of a provider.</param>
		/// <returns>An instance of a <see cref="T:System.Data.Common.DbProviderFactory" /> for a specified provider name.</returns>
		public static DbProviderFactory GetFactory(string providerInvariantName)
		{
			ADP.CheckArgumentLength(providerInvariantName, nameof(providerInvariantName));
			DataTable providerTable = GetProviderTable();
			if (providerTable != null)
			{
				DataRow providerRow = providerTable.Rows.Find((object)providerInvariantName);
				if (providerRow != null)
					return GetFactory(providerRow);
			}
			throw ADP.ConfigProviderNotFound();
		}

		/// <summary>Returns an instance of a <see cref="T:System.Data.Common.DbProviderFactory" />.</summary>
		/// <param name="providerRow">
		/// <see cref="T:System.Data.DataRow" /> containing the provider's configuration information.</param>
		/// <returns>An instance of a <see cref="T:System.Data.Common.DbProviderFactory" /> for a specified <see cref="T:System.Data.DataRow" />.</returns>
		public static DbProviderFactory GetFactory(DataRow providerRow)
		{
			ADP.CheckArgumentNull((object)providerRow, nameof(providerRow));
			DataColumn column = providerRow.Table.Columns["AssemblyQualifiedName"];
			if (column != null)
			{
				string str = providerRow[column] as string;
				if (!ADP.IsEmpty(str))
				{
					Type type = Type.GetType(str);
					if ((Type)null != type)
					{
						FieldInfo field = type.GetField("Instance", BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public);
						if ((FieldInfo)null != field && field.FieldType.IsSubclassOf(typeof(DbProviderFactory)))
						{
							object obj = field.GetValue((object)null);
							if (obj != null)
								return (DbProviderFactory)obj;
						}
						throw ADP.ConfigProviderInvalid();
					}
					throw ADP.ConfigProviderNotInstalled();
				}
			}
			throw ADP.ConfigProviderMissing();
		}

		private static DataTable IncludeFrameworkFactoryClasses(DataTable configDataTable)
		{
			DataTable providerDataTable = DbProviderFactoriesConfigurationHandler.CreateProviderDataTable();
			DbProviderFactoryConfigSection factoryConfigSection =
				new DbProviderFactoryConfigSection(typeof(SqlClientFactory), "SqlClient Data Provider",
					".Net Framework Data Provider for SqlServer");

			if (!factoryConfigSection.IsNull())
			{
				DataRow row = providerDataTable.NewRow();
				row["Name"] = (object)factoryConfigSection.Name;
				row["InvariantName"] = (object)factoryConfigSection.InvariantName;
				row["Description"] = (object)factoryConfigSection.Description;
				row["AssemblyQualifiedName"] = (object)factoryConfigSection.AssemblyQualifiedName;
				providerDataTable.Rows.Add(row);
			}

			int index1 = 0;
			while (configDataTable != null)
			{
				if (index1 < configDataTable.Rows.Count)
				{
					try
					{
						bool flag = false;
						if (configDataTable.Rows[index1]["AssemblyQualifiedName"].ToString().ToLowerInvariant().Contains("System.Data.OracleClient".ToString().ToLowerInvariant()))
						{
							Type type = Type.GetType(configDataTable.Rows[index1]["AssemblyQualifiedName"].ToString());
							if (type != (Type)null)
							{
								FieldInfo field = type.GetField("Instance", BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public);
								if ((FieldInfo)null != field && field.FieldType.IsSubclassOf(typeof(DbProviderFactory)) && field.GetValue((object)null) != null)
									flag = true;
							}
						}
						else
							flag = true;
						if (flag)
							providerDataTable.Rows.Add(configDataTable.Rows[index1].ItemArray);
					}
					catch (ConstraintException ex)
					{
					}
					++index1;
				}
				else
					break;
			}
			return providerDataTable;
		}

		private static DataTable GetProviderTable()
		{
			Initialize();
			return _providerTable;
		}

		private static void Initialize()
		{
			if (ConnectionState.Open == _initState)
				return;
			lock (_lockobj)
			{
				switch (_initState)
				{
					case ConnectionState.Closed:
						_initState = ConnectionState.Connecting;
						try
						{
							DataSet section = PrivilegedConfigurationManager.GetSection("system.data") as DataSet;
							_providerTable = section != null
								? IncludeFrameworkFactoryClasses(section.Tables[nameof(DbProviderFactories)])
								: IncludeFrameworkFactoryClasses((DataTable)null);
							break;
						}
						finally
						{
							_initState = ConnectionState.Open;
						}
				}
			}
		}
	}
}
