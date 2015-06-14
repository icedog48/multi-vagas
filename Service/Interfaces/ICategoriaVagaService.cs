using Model;
using Service.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ICategoriaVagaService : IMultiVagasCRUDService<CategoriaVaga>
    {
        void Add(CategoriaVaga categoria, int vagas);

        IEnumerable<Vaga> VagasDisponiveis(int categoriaId);

        Vaga GetVagaById(int id);

        void ReservarVaga(Reserva reserva);

        IList<CategoriaVaga> GetByEstacionamento(Estacionamento estacionamento);
    }
}
