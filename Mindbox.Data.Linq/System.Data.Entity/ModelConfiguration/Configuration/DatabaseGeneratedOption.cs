
namespace System.Data.Entity.ModelConfiguration.Configuration
{
	//
	// Summary:
	//     Represents the pattern used to generate values for a property in the database.
	public enum DatabaseGeneratedOption
	{
		//
		// Summary:
		//     The database does not generate values.
		None = 0,
		//
		// Summary:
		//     The database generates a value when a row is inserted.
		Identity = 1,
		//
		// Summary:
		//     The database generates a value when a row is inserted or updated.
		Computed = 2
	}
}
