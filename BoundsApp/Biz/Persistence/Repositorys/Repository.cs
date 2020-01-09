using System;
using System.Collections.Generic;
using System.Linq;
using BoundsApp.Biz.Core.Application.Global;
using BoundsApp.Biz.Core.Application.LiteDb;
using BoundsApp.Biz.Core.Repositorys;
using LiteDB;
using BoundsApp.Biz.Entity;

namespace BoundsApp.Biz.Persistence.Repositorys
{
    public abstract class Repository<T> : IRepository<T>
    {
        public IFactory<IDb<T>> Db { get; }
        private readonly string _collectionName;

        protected Repository(IFactory<IDb<T>> db, string collectionName = null)
        {
            Db = db;
            _collectionName = collectionName;
        }

        public virtual IEnumerable<T> GetAll()
        {
            using (var db = Db.Get())
            {
                return db.Collection(_collectionName).FindAll();
            }
        }

        public virtual T GetById(int? id)
        {
            if (!id.HasValue) return default(T);
            using (var db = Db.Get())
            {
                return db.Collection(_collectionName).FindById(id);
            }
        }

        public T GetOne()
        {
            using (var db= Db.Get())
            {
                return db.Collection(_collectionName).FindOne(Query.All());
            }
        }


        public virtual Guid Create(T model)
        {
            if (model == null) return Guid.Empty;
            using (var db = Db.Get())
            {
                return db.Collection(_collectionName).Insert(model).AsGuid;
            }
        }

        public int Create(IEnumerable<T> models)
        {
            if (models == null) return -1;
            var data = models as IList<T> ?? models.ToList();
            using (var db = Db.Get())
            {
                return db.Collection(_collectionName).Insert(data);
            }
        }

        public virtual bool Update(T entity)
        {
            using (var db = Db.Get())
            {
                return db.Collection(_collectionName).Update(entity);
            }
        }

        public virtual bool Delete(int id)
        {
            using (var db = Db.Get())
            {
                return db.Collection(_collectionName).Delete(id);
            }
        }
        public virtual int DeleteAll()
        {
            using (var db = Db.Get())
            {
                return db.Collection(_collectionName).Delete(Query.All());
            }
        }
    }
}