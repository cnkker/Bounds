using System.Windows;
using BoundsApp.Biz.Entity;
using BoundsApp.Biz.Persistence.Application.LiteDb;

namespace BoundsApp
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public static DbFactory<Bonus> BonusFactory = new DbFactory<Bonus>(DbFactory<Bonus>.Configuration.Production);
        public static DbFactory<Set> SetFactory = new DbFactory<Set>(DbFactory<Set>.Configuration.Production);
    }
}
