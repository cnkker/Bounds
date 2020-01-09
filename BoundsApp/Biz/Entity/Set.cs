using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoundsApp.Biz.Entity
{
    public class Set : EntityBase
    {
        /// <summary>
        /// 抽奖次数
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 音乐位置
        /// </summary>
        public string Music { get; set; }
    }
}
