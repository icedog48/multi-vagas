using Model.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Common
{
    public enum SituacaoRegistroEnum
    {
        ATIVO = 1,
        INATIVO = 2
    }

    public abstract class LogicalExclusionEntity : Entity, ILogicalExclusionEntity
    {
        public LogicalExclusionEntity()
        {
            this.SituacaoRegistro = SituacaoRegistroEnum.ATIVO;
        }

        public virtual SituacaoRegistroEnum SituacaoRegistro { get; set; }
    }
}
