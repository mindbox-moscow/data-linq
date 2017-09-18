namespace System.Data.Common
{
	internal class DbProviderFactoryConfigSection
	{
		private readonly Type factType;
		private readonly string name;
		private readonly string invariantName;
		private readonly string description;
		private readonly string assemblyQualifiedName;

		public DbProviderFactoryConfigSection(Type FactoryType, string FactoryName, string FactoryDescription)
		{
			try
			{
				factType = FactoryType;
				name = FactoryName;
				invariantName = factType.Namespace;
				description = FactoryDescription;
				assemblyQualifiedName = factType.AssemblyQualifiedName;
			}
			catch
			{
				factType = null;
				name = string.Empty;
				invariantName = string.Empty;
				description = string.Empty;
				assemblyQualifiedName = string.Empty;
			}
		}

		public DbProviderFactoryConfigSection(string FactoryName, string FactoryInvariantName, string FactoryDescription, string FactoryAssemblyQualifiedName)
		{
			factType = null;
			name = FactoryName;
			invariantName = FactoryInvariantName;
			description = FactoryDescription;
			assemblyQualifiedName = FactoryAssemblyQualifiedName;
		}

		public bool IsNull()
		{
			return factType == null && invariantName == string.Empty;
		}

		public string Name
		{
			get
			{
				return name;
			}
		}

		public string InvariantName
		{
			get
			{
				return invariantName;
			}
		}

		public string Description
		{
			get
			{
				return description;
			}
		}

		public string AssemblyQualifiedName
		{
			get
			{
				return assemblyQualifiedName;
			}
		}
	}
}
