using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace BoundsApp.Biz.Utils
{
    public static class Utility
    {
        private static StringBuilder _sbListControls;
        private static readonly Random Rng = new Random();

        public static StringBuilder GetVisualTreeInfo(Visual element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            _sbListControls = new StringBuilder();

            GetControlsList(element, 0);

            return _sbListControls;
        }

        private static void GetControlsList(Visual control, int level)
        {
            if (control == null) throw new ArgumentNullException(nameof(control));
            const int indent = 4;
            var childNumber = VisualTreeHelper.GetChildrenCount(control);

            for (int i = 0; i <= childNumber - 1; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(control, i);

                _sbListControls.Append(new string(' ', level * indent));
                _sbListControls.Append(v.GetType());
                _sbListControls.Append(Environment.NewLine);

                if (VisualTreeHelper.GetChildrenCount(v) > 0)
                {
                    GetControlsList(v, level + 1);
                }
            }
        }

        public static Tuple<int, int> GetLocation(string tag)
        {
            var arr = tag.Split(',');
            var x = int.Parse(arr[0]);
            var y = int.Parse(arr[1]);
            return new Tuple<int, int>(x, y);

        }


        public static void Shuffle<T>(this IList<T> list)
        {
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = Rng.Next(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
