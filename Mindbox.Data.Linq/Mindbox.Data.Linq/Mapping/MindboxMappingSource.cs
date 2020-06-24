using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;

namespace Mindbox.Data.Linq.Mapping
{
	/// <summary>
	/// A mapping source that uses attributes on the context to create the mapping model.
	/// </summary>
	public class MindboxMappingSource : AttributeMappingSource
	{
		public MindboxMappingSource(
			MindboxMappingConfiguration configuration,
			Dictionary<string, bool> databaseMigratedColumns)
		{
			if (configuration == null)
				throw new ArgumentNullException(nameof(configuration));

			if (!configuration.IsFrozen)
				configuration.Freeze();

			Configuration = configuration;
			DatabaseMigratedColumns = databaseMigratedColumns;
		}

		internal MindboxMappingConfiguration Configuration { get; }
		public Dictionary<string, bool> DatabaseMigratedColumns { get; }

		protected override MetaModel CreateModel(Type dataContextType)
		{
			if (dataContextType == null)
				throw new ArgumentNullException("dataContextType");

			return new MindboxMetaModel(this, dataContextType);
		}
	}
}