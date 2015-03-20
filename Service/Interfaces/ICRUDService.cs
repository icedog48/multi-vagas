﻿using Model;
using Service.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Interfaces
{
    public interface ICRUDService<T> : IService<T> where T : Entity
    {
        void Add(T obj);

        void Update(T obj);

        void Remove(T obj);

        IEnumerable<T> GetByFilter(IFilter<T> filter);
    }
}
