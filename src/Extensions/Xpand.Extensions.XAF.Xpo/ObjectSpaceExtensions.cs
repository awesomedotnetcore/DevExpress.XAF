﻿using System.Diagnostics;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Helpers;
using JetBrains.Annotations;

namespace Xpand.Extensions.XAF.Xpo{
    public static class ObjectSpaceExtensions{
        [DebuggerStepThrough]
        public static UnitOfWork UnitOfWork(this IObjectSpace objectSpace) => (UnitOfWork) ((XPObjectSpace) objectSpace).Session;
        [PublicAPI]
        public static void DeleteAllData(this IObjectSpaceProvider objectSpaceProvider){
            using (var objectSpace = objectSpaceProvider.CreateUpdatingObjectSpace(true)){
                var dbConnection = ((ConnectionProviderSql) ((BaseDataLayer) ((XPObjectSpace) objectSpace).Session.DataLayer).ConnectionProvider).Connection;
                using (var dbCommand = dbConnection.CreateCommand()){
                    dbCommand.CommandText = @"
        EXEC sp_MSForEachTable ""ALTER TABLE ? NOCHECK CONSTRAINT all""
        EXEC sp_MSForEachTable ""DELETE FROM ?""
        exec sp_MSForEachTable ""ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all""
";
                    dbCommand.ExecuteNonQuery();
                }
                objectSpaceProvider.UpdateSchema();
            }
        }

    }
}