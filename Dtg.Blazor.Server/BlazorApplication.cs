using System;
using DevExpress.EntityFrameworkCore.Security;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor;
using DevExpress.ExpressApp.Security;
using Dtg.Module.BusinessObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
 
namespace Dtg.Blazor.Server {
    public partial class BlazorApplication : DevExpress.ExpressApp.Blazor.BlazorApplication {
        public BlazorApplication() {
            InitializeComponent();
        }
        protected override void OnSetupStarted() {
            base.OnSetupStarted();
            IConfiguration configuration = ServiceProvider.GetRequiredService<IConfiguration>();
            if(configuration.GetConnectionString("ConnectionString") != null) {
                ConnectionString = HandyFunctions.GetConnectionString();
               
                // ConnectionString = configuration.GetConnectionString("ConnectionString");
            }
#if EASYTEST
            if(configuration.GetConnectionString("EasyTestConnectionString") != null) {
                ConnectionString = configuration.GetConnectionString("EasyTestConnectionString");
            }
#endif
#if DEBUG
            if(System.Diagnostics.Debugger.IsAttached && CheckCompatibilityType == CheckCompatibilityType.DatabaseSchema) {
                DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways;
            }
#endif
        }
        protected override void CreateDefaultObjectSpaceProvider(CreateCustomObjectSpaceProviderEventArgs args) {
            var dbFactory = ServiceProvider.GetService<IDbContextFactory<DtgEFCoreDbContext>>();
            SecuredEFCoreObjectSpaceProvider efCoreObjectSpaceProvider = new((ISelectDataSecurityProvider)Security, dbFactory, TypesInfo);
            args.ObjectSpaceProviders.Add(efCoreObjectSpaceProvider);
            args.ObjectSpaceProviders.Add(new NonPersistentObjectSpaceProvider(TypesInfo, null));
        }
        private void DtgBlazorApplication_DatabaseVersionMismatch(object sender, DatabaseVersionMismatchEventArgs e) {
#if EASYTEST
            e.Updater.Update();
            e.Handled = true;
#else
            if(System.Diagnostics.Debugger.IsAttached) {
                e.Updater.Update();
                e.Handled = true;
            }
            else {
                string message = "The application cannot connect to the specified database, " +
                    "because the database doesn't exist, its version is older " +
                    "than that of the application or its schema does not match " +
                    "the ORM data model structure. To avoid this error, use one " +
                    "of the solutions from the https://www.devexpress.com/kb=T367835 KB Article.";

                if(e.CompatibilityError != null && e.CompatibilityError.Exception != null) {
                    message += "\r\n\r\nInner exception: " + e.CompatibilityError.Exception.Message;
                }
                throw new InvalidOperationException(message);
            }
#endif
        }
    }
}
