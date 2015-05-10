using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Common
{
    public abstract class Entity
    {
        public virtual int Id { get; set; }

        public virtual bool IsNew() { return this.Id == 0; }

        public virtual void ClearId() 
        {
            this.Id = 0;
        }
    }
}
