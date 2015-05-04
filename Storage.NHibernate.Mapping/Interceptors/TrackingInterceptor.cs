using System.Reflection;
using Model;

using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

using NHibernate;
using NHibernate.Cfg;

using System.Configuration;
using System.Web;
using System;
using System.IO;

using Storage;
using Storage.NHibernate;

using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using Storage.Nhibernate.Mapping.Conventions;
using Storage.Nhibernate.Mapping.Interceptors;
using NHibernate.SqlCommand;

namespace Storage.Nhibernate.Mapping.Interceptors
{

    public class TrackingInterceptor : EmptyInterceptor
    {
        public override SqlString OnPrepareStatement(SqlString sql)
        {
            Console.WriteLine(sql.ToString());

            return base.OnPrepareStatement(sql);
        }
    }

}
