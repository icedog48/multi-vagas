using FluentNHibernate.Mapping;
using Model;
using Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Nhibernate.Mapping
{
    public class FuncionarioMap : LogicalExclusionEntityMap<Funcionario>
    {
        public FuncionarioMap()
        {
            Map(x => x.CEP);
            Map(x => x.Logradouro);
            Map(x => x.Bairro);            
            Map(x => x.Cidade);
            Map(x => x.UF);

            Map(x => x.Matricula);
            Map(x => x.Nome);
            Map(x => x.CPF);
            Map(x => x.Salario);
            Map(x => x.Obs);
            Map(x => x.DataAdmissao);
            Map(x => x.HoraInicio);
            Map(x => x.HoraSaida);
            Map(x => x.Telefone);

            References(x => x.Usuario);

            References(x => x.Estacionamento);
        }
    }
}
