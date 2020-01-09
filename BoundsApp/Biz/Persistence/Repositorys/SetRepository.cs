using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoundsApp.Biz.Core.Application.Global;
using BoundsApp.Biz.Core.Application.LiteDb;
using BoundsApp.Biz.Core.Repositorys;
using BoundsApp.Biz.Entity;

namespace BoundsApp.Biz.Persistence.Repositorys
{
    public class SetRepository : Repository<Set>, ISetRepository
    {
        private readonly IFactory<IDb<Set>> _db;
        public SetRepository(IFactory<IDb<Set>> db) : base(db)
        {
            _db = db;
        }

        public void CreateSet(Set set)
        {
            var model = _db.Get().Collection().FindOne(p => true);
            if (model == null)
            {
                model = set;
                base.Create(model);
            }
            else
            {
                model.Page = set.Page;
                model.Music = set.Music;
                base.Update(model);
            }
        }
    }
}
