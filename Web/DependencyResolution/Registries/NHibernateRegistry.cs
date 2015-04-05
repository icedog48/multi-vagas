// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultRegistry.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Web.DependencyResolution.Registries
{
    using System.Reflection;
    using Model;

    using FluentNHibernate.Automapping;
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;        

    using NHibernate;
    using NHibernate.Cfg;

    using StructureMap;
    using StructureMap.Web;
    using StructureMap.Configuration.DSL;
    using StructureMap.Graph;
    using System.Configuration;
    using System.Web;
    using System;
    using System.IO;
    using Web.App_Start;
    using Storage;
    using Storage.NHibernate;
    using FluentMigrator.Runner.Processors;
    using FluentMigrator.Runner.Processors.SqlServer;
    using FluentNHibernate.Conventions;
    using FluentNHibernate.Conventions.Instances;
    using Model.Common;

    public class AutoMappingCfg : DefaultAutomappingConfiguration 
    {
        public override bool ShouldMap(System.Type type)
        {
            return type.BaseType.Equals(typeof(Entity)) || type.BaseType.Equals(typeof(LogicalExclusionEntity));
        }
    }

    public class NHibernateSettingsRegistry : Registry 
    {
        public NHibernateSettingsRegistry()
        {
            
#if DEBUG
            var connectionString = ConfigurationManager.ConnectionStrings["LocalConnection"].ConnectionString;
#else
            var connectionString = ConfigurationManager.ConnectionStrings["multivagas_db"].ConnectionString;
#endif

            For<string>().Add<string>(connectionString).Named("DefaultConnection");

            For<MigrationProcessorFactory>().Use<SqlServer2008ProcessorFactory>();

            For<IPersistenceConfigurer>().Use<FluentNHibernate.Cfg.Db.MsSqlConfiguration>(FluentNHibernate.Cfg.Db.MsSqlConfiguration.MsSql2008.ConnectionString(connectionString));
        }
    }

    public class TrackingInterceptor : EmptyInterceptor
    {
        public override NHibernate.SqlCommand.SqlString OnPrepareStatement(NHibernate.SqlCommand.SqlString sql)
        {
            Console.WriteLine(sql.ToString());

            return base.OnPrepareStatement(sql);
        }
    }

    public class MyManyToManyConvention : IHasManyToManyConvention
    {
        public void Apply(IManyToManyCollectionInstance instance)
        {
            var firstName = instance.EntityType.Name;
            var secondName = instance.ChildType.Name;

            if (StringComparer.OrdinalIgnoreCase.Compare(firstName, secondName) > 0)
            {
                instance.Table(string.Format("{0}{1}", secondName, firstName));
                instance.Inverse();
            }
            else
            {
                instance.Table(string.Format("{0}{1}", firstName, secondName));
                instance.Not.Inverse();
            }

            instance.Cascade.All();
        }
    }

    public class PropertyConvention : IPropertyConvention
    {
        public void Apply(IPropertyInstance instance)
        {
            if (instance.Property.PropertyType == typeof(TimeSpan))
                instance.CustomType("TimeAsTimeSpan");
        }
    }

    public class NHibernateRegistry : Registry
    {
        #region Constructors and Destructors

        public NHibernateRegistry(IContainer settingsContainer)
        {
            var autoMappingCfg = new AutoMappingCfg();

            var databaseCfg = settingsContainer.GetInstance<IPersistenceConfigurer>(); //SQLiteConfiguration.Standard.ConnectionString(connectionString);            

            var sesstionFactory = Fluently.Configure()
                                             .Database(databaseCfg)
                                             .Mappings(m =>
                                             {
                                                 var automapping = AutoMap.Assemblies(autoMappingCfg, typeof(Estacionamento).Assembly)
                                                                                .Conventions.Add<MyManyToManyConvention>(new MyManyToManyConvention())
                                                                                .Conventions.Add<PropertyConvention>(new PropertyConvention())
                                                                                .IgnoreBase<Entity>()
                                                                                .IgnoreBase<LogicalExclusionEntity>();
                                                 
                                                 m.AutoMappings.Add(automapping);
                                             })
                                             .ExposeConfiguration(config => config.SetInterceptor(new TrackingInterceptor()))
                                             .ExposeConfiguration(cfg => cfg.Properties.Add("current_session_context_class", "thread"))
                                             .BuildSessionFactory();
            


            For<ISessionFactory>().Singleton().Use(sesstionFactory);
            
            For<ISession>().HybridHttpOrThreadLocalScoped().Use(context => context.GetInstance<ISessionFactory>().OpenSession(new TrackingInterceptor()));

            For(typeof(IRepository<>)).Use(typeof(NHibernateRepository<>));
        }

        #endregion
    }
}