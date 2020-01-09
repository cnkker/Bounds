using System.Collections.Generic;
using BoundsApp.Biz.Entity;

namespace BoundsApp.Biz.Core.Repositorys
{
    public interface IBonusRepository : IRepository<Bonus>
    {
        IEnumerable<Bonus> GetByName(string name);

        void CreateBonus(IList<Bonus> listBonus);
    }
}