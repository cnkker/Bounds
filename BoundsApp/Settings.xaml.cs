using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BoundsApp;
using BoundsApp.Biz.Core.Repositorys;
using BoundsApp.Biz.Entity;
using BoundsApp.Biz.Persistence.Repositorys;
using BoundsApp.Biz.Persistence.Repositorys;
using BoundsApp.Biz.Utils;
using MahApps.Metro.Controls;
using Microsoft.Win32;

namespace BoundsApp
{
    /// <summary>
    /// Settingsl.xaml 的交互逻辑
    /// </summary>
    public partial class Settings : MetroWindow
    {
        private readonly IBonusRepository _bonusRepository;
        private readonly ISetRepository _setRepository;
        public Settings()
        {
            InitializeComponent();
            _bonusRepository = new BonusRepository(App.BonusFactory);
            _setRepository = new SetRepository(App.SetFactory);
        }

        /// <summary>
        /// 设置奖品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnShow_Click(object sender, RoutedEventArgs e)
        {            
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
        
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSeting_Click(object sender, RoutedEventArgs e)
        {
            var mainContainer = (Grid)this.Content;
            var element = mainContainer.Children;
            var lstElement = element.Cast<FrameworkElement>().ToList();
            var lstControl = lstElement.OfType<Control>();
            var txtBoxs = lstControl.Where(p=>(p is TextBox)&& p.Tag != null).Cast<TextBox>().ToList();

            if (txtBoxs.All(p=>p.Text== string.Empty))
            {
                MessageBox.Show("至少填写一项吧", "轻轻的询问道", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            var listBonus = new List<Bonus>();
            foreach (var box in txtBoxs)
            {
                var bouns = new Bonus();
                var tag = Utility.GetLocation(box.Tag.ToString());
                bouns.Name = box.Text;
                bouns.X = tag.Item1;
                bouns.Y = tag.Item2;
                listBonus.Add(bouns);
            }
            _bonusRepository.CreateBonus(listBonus);

            var set = new Set {Page = PageNumber.Value ?? 1};
            if (!string.IsNullOrEmpty(TxtMusic.Text))
            {
                set.Music = TxtMusic.Text;
            }
            _setRepository.CreateSet(set);

            MessageBox.Show("操作成功，么么哒", "恭喜", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            
            var mainContainer = (Grid)this.Content;
            var element = mainContainer.Children;
            var lstElement = element.Cast<FrameworkElement>().ToList();
            var lstControl = lstElement.OfType<Control>();
            var textBoxes = lstControl.Where(p => (p is TextBox) && p.Tag != null).Cast<TextBox>().ToList();
            this.LoadBonus(textBoxes);
            this.LoadSets();
        }

        private void LoadBonus(IList<TextBox> textBoxes)
        {
            var items = _bonusRepository.GetAll();
            foreach (var item in items)
            {
                var tag = string.Concat(item.X, ",", item.Y);
                foreach (var box in textBoxes)
                {
                    if (tag != box.Tag.ToString() || item.Name == null) continue;
                    box.Text = item.Name.Trim();
                }
            }
        }

        private void LoadSets()
        {
            var item = _setRepository.GetOne();
            if (item == null) return;
            PageNumber.Value = item.Page;

            if (!string.IsNullOrWhiteSpace(item.Music))
            {
                TxtMusic.Text = item.Music;
            }
        }

        private void TxtFile_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == false)
            {
                return;
            }
            TxtMusic.Text = openFileDialog.FileName;
        }
    }
}
