using System.Collections.Generic;
using System.Reflection;
using NextORM.Core;
using NextORM.SqlServer;

namespace RawBencher.Benchers
{
	/// <summary>
	/// Specific bencher for NextORM (https://github.com/AlexeyShirshov/nextorm), doing a no-change tracking, whole entity fetch.
	/// </summary>
	public class NextOrmBencher : FetchOnlyBencherBase<NextOrmSalesOrderHeader>
	{
		#region Member declarations
		private readonly DataContextBuilder _contextBuilder;
		private readonly IDataContext _dataContext;
		#endregion


		/// <summary>
		/// Initializes a new instance of the <see cref="NextOrmBencher"/> class.
		/// </summary>
		/// <param name="connectionString">The connection string to use.</param>
		public NextOrmBencher(string connectionString)
			: base(e => e.SalesOrderId, usesChangeTracking: false, usesCaching: false)
		{
			_contextBuilder = new DataContextBuilder().UseSqlServer(connectionString);
			_dataContext = _contextBuilder.CreateDataContext();
		}


		/// <summary>
		/// Fetches the individual element
		/// </summary>
		/// <param name="key">The key of the element to fetch.</param>
		/// <returns>The fetched element, or null if not found</returns>
		public override NextOrmSalesOrderHeader FetchIndividual(int key)
		{
			// The positional parameter is bound at execution time, so the cached query plan is reused for every key
			// instead of a new SQL text per key (a captured local would be rendered as an inlined literal).
			return _dataContext.From<NextOrmSalesOrderHeader>()
							   .Where(e => e.SalesOrderId == SqlFunctions.Parameter<int>(0))
							   .FirstOrDefault(key);
		}


		/// <summary>
		/// Fetches the complete set of elements and returns this set as an IEnumerable.
		/// </summary>
		/// <returns>the set fetched</returns>
		public override IEnumerable<NextOrmSalesOrderHeader> FetchSet()
		{
			// Without an explicit projection the builder selects every mapped column of the entity.
			return _dataContext.From<NextOrmSalesOrderHeader>().ToList();
		}


		/// <summary>
		/// Creates the name of the framework this bencher is for. Use the overload which accepts a format string and a type to create a name based on a
		/// specific version
		/// </summary>
		/// <returns>the framework name.</returns>
		protected override string CreateFrameworkNameImpl()
		{
			// BencherUtils.GetVersion returns AssemblyVersion (1.0.9.0), which drops the prerelease suffix. NextORM ships
			// only prereleases, so the informational version ('1.0.9-rc3+<commit>') is the only one that identifies the build.
			var informationalVersion = typeof(DataContextBuilder).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
			var version = string.IsNullOrEmpty(informationalVersion) ? BencherUtils.GetVersion(typeof(DataContextBuilder)).ToString() : informationalVersion.Split('+')[0];
			return "NextORM v" + version;
		}
	}
}
