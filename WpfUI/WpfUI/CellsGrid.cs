using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfUI
{
    internal class CellsGrid
    {
        public Сell Tag { set; get; }
        public object Content { set; get; }
        public SolidColorBrush Background { set; get; }
        static BrushConverter bc = new BrushConverter();

        public CellsGrid(Сell tag)
        {
            Tag = tag;
            Content = setContent(tag);
        }

        private object setContent(Сell cell)
        {
            switch (cell.Status)
            {
                case CellStatus.Explosion:
                    Background = (SolidColorBrush)bc.ConvertFrom("#e50000");
                    return GetObjImage(@".\Images\Explosion.png");
                case CellStatus.Mark:
                    return GetObjImage(@".\Images\Flag.png");
                case CellStatus.MarkMine:
                    Background = (SolidColorBrush)bc.ConvertFrom("#58e669");
                    return GetObjImage(@".\Images\BombMark.png");
                case CellStatus.Mine:
                    return GetObjImage(@".\Images\Bomb.png");
                case CellStatus.Number:
                    Background = (SolidColorBrush)bc.ConvertFrom("#FFFACD");
                    return cell.Value;
                case CellStatus.Open:
                    Background = (SolidColorBrush)bc.ConvertFrom("#FFFACD");
                    return string.Empty;
                default:
                    Background = (SolidColorBrush)bc.ConvertFrom("#58e669");
                    return string.Empty;
            }
        }

        object GetObjImage(string url)
        {
            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Horizontal;
            Image flagImage = new Image();
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(url, UriKind.Relative);
            bi.EndInit();
            flagImage.Source = bi;
            sp.Children.Add(flagImage);

            return sp;
        }

        public override string ToString()
        {
            return Content.ToString();
        }
    }
}
