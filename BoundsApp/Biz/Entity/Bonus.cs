using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoundsApp.Biz.Entity
{
    public class Bonus: EntityBase
    {
        /// <summary>
        /// 奖品名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 坐标
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// 坐标
        /// </summary>
        public int Y { get; set; }

      
    }
}
