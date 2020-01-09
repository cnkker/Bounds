using System;
using LiteDB;

namespace BoundsApp.Biz.Core.Application.LiteDb
{
    public interface IDb<T> : IDisposable
    {
        LiteCollection<T> Collection(string collectionName = null);
    }
}