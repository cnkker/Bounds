using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiteDB;

namespace BoundsApp.Biz.Entity
{
    public class EntityBase
    {
        private Guid _guid;

        /// <summary>
        /// Guid
        /// </summary>
        [BsonId(true)]
        public Guid Id
        {
            get => _guid == Guid.Empty ? Guid.NewGuid() : _guid;
            set => _guid = value;
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifiedTime { get; set; }
    }
}
