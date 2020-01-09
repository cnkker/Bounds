using System;
using System.Collections.Generic;
using BoundsApp.Biz.Core.Application.Global;
using BoundsApp.Biz.Core.Application.LiteDb;
using BoundsApp.Biz.Core.Repositorys;
using BoundsApp.Biz.Entity;
using BoundsApp.Biz.Persistence.Repositorys;

namespace BoundsApp.Biz.Persistence.Repositorys
{
    public class BonusRepository : Repository<Bonus>, IBonusRepository
    {
        private readonly IFactory<IDb<Bonus>> _db;
        public BonusRepository(IFactory<IDb<Bonus>> db) : base(db)
        {
            _db = db;
        }

      


        //public IEnumerable<Foo> GetByName(string name) => GetAll()?.Where(i => i.Name == name);
        public IEnumerable<Bonus> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public void CreateBonus(IList<Bonus> listBonus)
        {
            base.DeleteAll();
            base.Create(listBonus);
        }
    }
}