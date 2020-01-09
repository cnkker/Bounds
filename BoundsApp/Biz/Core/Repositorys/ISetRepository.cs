using System.Collections.Generic;
using BoundsApp.Biz.Entity;

namespace BoundsApp.Biz.Core.Repositorys
{
    public interface ISetRepository : IRepository<Set>
    {
        void CreateSet(Set set);
    }
}