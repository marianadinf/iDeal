using System;
using System.IO;
using UIT.iDeal.Common.Helpers;
using UIT.iDeal.Infrastructure.Configuration.Flavours;

namespace UIT.iDeal.Infrastructure.Configuration.Extensions
{
    public static class ConfigurationExtensions
    {
        //public static IPersistenceConfigurer GetPersistenceConfigurer(this IEnvironmentConfiguration environmentConfiguration, ProjectFlavour? projectFlavour)
        //{
        //    var databaseFlavour = environmentConfiguration.DatabaseFlavour(projectFlavour.Value);
        //    var databaseServer = environmentConfiguration.DatabaseServer(projectFlavour.Value);
        //    var connectionString = environmentConfiguration.ConnectionString(projectFlavour.Value);

        //    switch (databaseFlavour)
        //    {
        //        case DatabaseFlavour.SQLServer2008FromWebConfig:

        //            return MsSqlConfiguration
        //                .MsSql2008
        //                .ConnectionString(connectionString)
        //                .ShowSql()
        //                .FormatSql()
        //                .AdoNetBatchSize(100);

        //        case DatabaseFlavour.SQLServer2008:

        //            if (databaseServer == null)
        //            {
        //                return MsSqlConfiguration
        //                    .MsSql2008
        //                    .ShowSql()
        //                    .FormatSql()
        //                    .AdoNetBatchSize(100);
        //            }

        //            return MsSqlConfiguration
        //                .MsSql2008
        //                .ConnectionString(connectionString)
        //                .ShowSql()
        //                .FormatSql()
        //                .AdoNetBatchSize(100);

        //        case DatabaseFlavour.SQLiteFileSystem:

        //            var dbPath = Path.Combine(FileHelpers.TempFolderPath, "Test.db");

        //            return SQLiteConfiguration
        //                .Standard.UsingFile(dbPath)
        //                .FormatSql()
        //                .AdoNetBatchSize(100);

        //        case DatabaseFlavour.SQLiteInMemory:

        //            var sqLiteConfiguration = SQLiteConfiguration
        //                .Standard.InMemory()
        //                .Raw(Environment.ConnectionProvider, typeof(DatabaseConnectionProviderThatCachesTheConnection).AssemblyQualifiedName)
        //                .FormatSql()
        //                .AdoNetBatchSize(100);

        //            return sqLiteConfiguration;

        //        default:

        //            throw new NotImplementedException();
        //    }
        //}
    }
}
