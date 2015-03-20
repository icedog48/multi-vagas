using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Interfaces
{
    public interface IService<T> where T : Entity
    {
        T GetById(int id);

        IEnumerable<T> GetAll();
    }
}
