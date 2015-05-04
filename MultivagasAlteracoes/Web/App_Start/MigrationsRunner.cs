using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using FluentMigrator;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Processors;
using System.Configuration;
using StructureMap;
using Migrations._201502;

namespace Web.App_Start
{
    public class MigrationsRunner
    {
        public static void Run(IContainer settingsContainer) 
        {
            var connectionString = settingsContainer.GetInstance<string>("DefaultConnection");

            Announcer announcer = new TextWriterAnnouncer(s => System.Diagnostics.Debug.WriteLine(s));
            announcer.ShowSql = true;

            Assembly assembly = typeof(CreateTableEstacionamento).Assembly;

            IRunnerContext migrationContext = new RunnerContext(announcer);

            var options = new ProcessorOptions
            {
                PreviewOnly = false,  // set to true to see the SQL
                Timeout = 60
            };

            try
            {
                var factory = settingsContainer.GetInstance<MigrationProcessorFactory>(); //new FluentMigrator.Runner.Processors.SQLite.SQLiteProcessorFactory();
                var processor = factory.Create(connectionString, announcer, options);

                var runner = new MigrationRunner(assembly, migrationContext, processor);
                runner.MigrateUp(true);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("connectionString: " + connectionString, ex);
            }
            
        }
    }
}