using Common;
using Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfUI
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        public GameField gameField;
        
        public int RowCount
        {
            get
            {
                return gameField.Size.Row;
            }
        }
        public int ColumnCount
        {
            get
            {
                return gameField.Size.Column;
            }
        }

        const int gridSize = 10;

        private ObservableCollection<CellsGrid> cells = new ObservableCollection<CellsGrid>();
        public ObservableCollection<CellsGrid> Cells
        {
            get { return cells; }
            set
            {
                cells = value;
                OnPropertyChanged("Cells");
            }
        }
                
        public MainViewModel()
        {
            gameField = new GameField(10, 10, 10);
            gameField.GenerateMines();
            cells = new ObservableCollection<CellsGrid>();
            Fill();
        }

        public void Fill()
        {
            Cells.Clear();
            for (var row = 0; row < gameField.Size.Row; row++)
            {
                for (var column = 0; column < gameField.Size.Column; column++)
                {
                    Cells.Add(new CellsGrid(gameField[row, column]));                    
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }

    class CellsGrid
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
                    return GetObjImage(@"..\..\Explosion.png");
                case CellStatus.Mark:
                    return GetObjImage(@"..\..\Flag.png");
                case CellStatus.MarkMine:
                    Background = (SolidColorBrush)bc.ConvertFrom("#58e669");
                    return GetObjImage(@"..\..\BombMark.png");
                case CellStatus.Mine:
                    return GetObjImage(@"..\..\Bomb.png");
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
