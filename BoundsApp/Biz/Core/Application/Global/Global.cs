using System;

namespace BoundsApp.Biz.Core.Application.Global
{
    public class Global
    {
        /// <summary>
        /// Connection String for LiteDb 
        /// 
        /// Insert here the path where the db file should be stored
        /// 
        /// </summary>
        public static readonly string ConnectionString = $"{AppDomain.CurrentDomain.BaseDirectory}/data.db";
    }
}