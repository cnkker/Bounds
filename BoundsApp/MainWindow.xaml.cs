using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using BoundsApp.Biz.Core.Repositorys;
using BoundsApp.Biz.Entity;
using BoundsApp.Biz.Persistence.Repositorys;
using BoundsApp.Biz.Utils;
using MahApps.Metro.Controls;

namespace BoundsApp
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private readonly IBonusRepository _bonusRepository;
        private readonly ISetRepository _setRepository;
        private readonly string _musicPath;
        private const string TOTAL_BOUNDS = "奖品总计：";
        private const string LAVE_BOUNDS = "剩余次数：";
        private readonly MediaPlayer _mediaPlayer;
        private int _currPage = 0;
        private int? _totalPage = 0;
        private int _click = 0;
        private int? _totalCount = 0;
        public MainWindow()
        {
            InitializeComponent();
            _bonusRepository = new BonusRepository(App.BonusFactory);
            _setRepository = new SetRepository(App.SetFactory);
            _musicPath = _setRepository.GetOne().Music;
            _mediaPlayer=new MediaPlayer();
        }


        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {

            this.LoadMusic();
            this.LoadBounds();
        }


        private void LoadBounds()
        {

            var mainContainer = (Grid)this.Content;
            var element = mainContainer.Children;
            var lstElement = element.Cast<FrameworkElement>().ToList();
            var lstControl = lstElement.OfType<Control>();
            var buttons = lstControl.Where(p => (p is Button)).Cast<Button>().ToList();
            var items = _bonusRepository.GetAll();
            _totalPage = _setRepository.GetOne()?.Page;
            var lists = items as IList<Bonus> ?? items.ToList();
            lists.Shuffle();
            for (var i = 0; i < buttons.Count; i++)
            {
                buttons[i].Background = Brushes.LightGray;
                buttons[i].Content = string.Empty;
                buttons[i].Tag = string.IsNullOrWhiteSpace(lists[i].Name) ? null : lists[i].Name;
            }

            var count = lists.Count(p => !string.IsNullOrWhiteSpace(p.Name));
            var totalCount = count * _totalPage; //总共

            _totalCount = totalCount - (count * _currPage); //剩余

            this.LblTotal.Dispatcher?.Invoke(DispatcherPriority.Normal,
                new Action(() => {   this.LblTotal.Content= string.Concat(TOTAL_BOUNDS, totalCount); }));

            this.LoadLave();
        }

        private void LoadLave()
        {
            this.LblLave.Dispatcher?.Invoke(DispatcherPriority.Normal,
                new Action(() => { this.LblLave.Content = string.Concat(LAVE_BOUNDS, (_totalCount--)); }));
        }

        private void LoadMusic()
        {
            if (string.IsNullOrWhiteSpace(_musicPath)) return;
            _mediaPlayer.Dispatcher?.Invoke(new Action(() =>
            {
                _mediaPlayer.Open(new Uri(_musicPath));
                _mediaPlayer.MediaEnded += (sender, args) =>
                {
                    _mediaPlayer.Open(new Uri(_musicPath));
                    _mediaPlayer.Play();
                };
                _mediaPlayer.MediaFailed += (sender, args) =>
                {
                    MessageBox.Show("选择的可能不是音乐文件", "播放失败", MessageBoxButton.OK, MessageBoxImage.Error);
                };
                _mediaPlayer.Play();
            }));
           
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
          
            var btn = sender as Button;
            if (btn == null) return;
            if (!string.IsNullOrWhiteSpace(btn.Content?.ToString()))
            {
                return;
            }
            _click++;
          //  btn.Background = Brushes.BurlyWood;
            if (btn.Tag == null)
            {
                btn.Dispatcher?.Invoke(DispatcherPriority.Normal,
                    new Action(() =>
                    {
                        btn.Foreground = Brushes.Black;
                        btn.Content = "未中奖";
                    }));
            }
            else
            {
                this.LoadLave();
                btn.Dispatcher?.Invoke(DispatcherPriority.Normal,
                    new Action(() =>
                    {
                        btn.Foreground = Brushes.DarkRed;
                        btn.Content = btn.Tag;
                    }));
            }
            if (_click % 9 != 0) return;
            _currPage++;
            if (_currPage < _totalPage)
            {
                var result = MessageBox.Show("第一轮抽奖完成，是否开启下一轮？", "询问", MessageBoxButton.YesNo,
                    MessageBoxImage.Question, MessageBoxResult.Yes);
                if (result == MessageBoxResult.Yes)
                {
                    this.LoadBounds();
                }
            }
            else
            {
                MessageBox.Show("所有抽奖都已完成", "通知", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void MainMetroWindow_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MainMetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 弹窗提示是否确定要退出
            var result = MessageBox.Show("您确定要退出吗？", "询问", MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
            if (result == MessageBoxResult.Cancel)
            {
                e.Cancel = true; // 中断点击事件
            }
        }

        private void LblNext_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (_click % 9 != 0)
            {
                MessageBox.Show("抽奖未完成，完成后可以手动开启下一轮", "通知", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                if (!(_currPage < _totalPage))
                {
                    MessageBox.Show("所有抽奖都已完成", "通知", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                var result = MessageBox.Show("第一轮抽奖完成，是否开启下一轮？", "询问", MessageBoxButton.YesNo,
                    MessageBoxImage.Question, MessageBoxResult.Yes);
                if (result == MessageBoxResult.Yes)
                {
                    this.LoadBounds();
                }
            }
        }

        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LblReset_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _currPage = 0;
            _totalPage = 0;
            _click = 0;
            _totalCount = 0;
            this.LoadBounds();
        }
    }
}
