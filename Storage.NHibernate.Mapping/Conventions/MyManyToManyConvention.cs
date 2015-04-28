using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Nhibernate.Mapping.Conventions
{

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
}
