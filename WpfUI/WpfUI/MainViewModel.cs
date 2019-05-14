using Algorithm;
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
using System.Windows.Input;
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

        private int countMine = 10;
        public int CountMine
        {
            get { return countMine; }
            set
            {
                countMine = value;
                OnPropertyChanged("CountMine");
            }
        }

        private Сell current;
        public Сell Current
        {
            get { return current; }
            set
            {
                current = value;
                OnPropertyChanged("Current");
            }
        }

        public string Status
        {
            get
            {
                return gameField.Status.ToString();
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
            NewGame();
        }

        public void NewGame()
        {
            gameField = new GameField(10, 10, CountMine);
            gameField.GenerateMines();
            cells.Clear();
            OnPropertyChanged("Status");
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

        public void Button_PreviewMouseDown(Button btn, MouseButton e)
        {
            var cell = (Сell)btn.Tag;
            var mark = false;
            Сell value;

            if (e == MouseButton.Right) mark = true;
            if (e == MouseButton.Middle)
            {
                value = TestAlgorithm.GetСhoice(gameField.VisibleСells, gameField.Marks,
                    gameField.Size, gameField.CountMines);
            }
            else
            {
                value = new Сell(cell.Row, cell.Column, mark);
            }
            gameField.OpenCell(value);
            if (gameField.Status == GameStatus.Play)
            {
                Current = value;
            }
            else
            {
                OnPropertyChanged("Status");
            }
            Fill();
        }
    }
}
