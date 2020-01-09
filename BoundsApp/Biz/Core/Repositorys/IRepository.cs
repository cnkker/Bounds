using System;
using System.Collections.Generic;
using BoundsApp.Biz.Core.Application.Global;
using BoundsApp.Biz.Core.Application.LiteDb;

namespace BoundsApp.Biz.Core.Repositorys
{
    public interface IRepository<T> : ILiteDbRepository<T>, IReadRepository<T>, IWriteRepository<T>
    {
    }

    public interface ILiteDbRepository<T>
    {
        IFactory<IDb<T>> Db { get; }
    }

    public interface IReadRepository<out T>
    {
        IEnumerable<T> GetAll();
        T GetById(int? id);

        T GetOne();
    }

    public interface IWriteRepository<in T>
    {
        Guid Create(T model);

        int Create(IEnumerable<T> models);


        bool Update(T entity);
        bool Delete(int id);
        int DeleteAll();
    }
}