using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Algorithm;
using Common;
using Core;
using MahApps.Metro.Controls;

namespace WpfUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private MainViewModel viewModel;

        public MainWindow()
        {
            viewModel = new MainViewModel();
            DataContext = viewModel;
            InitializeComponent();
        }

        private void Button_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var btn = (Button)sender;
            var cell = (Сell)btn.Tag;
            var mark = false;
            Сell value;

            if (e.ChangedButton == MouseButton.Right) mark = true;
            if (e.ChangedButton == MouseButton.Middle)
            {
                value = TestAlgorithm.GetСhoice(viewModel.gameField.VisibleСells, viewModel.gameField.Marks,
                    viewModel.gameField.Size, viewModel.gameField.CountMines);
            }
            else
            {                
                value = new Сell(cell.Row, cell.Column, mark);
            }
            viewModel.gameField.OpenCell(value);
            if (viewModel.gameField.Status == GameStatus.Play)
            {
                lblChose.Text = value.ToString();
            }
            lblStatus.Text = viewModel.gameField.Status.ToString();
            viewModel.Fill();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel = new MainViewModel();
            DataContext = viewModel;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Сell value = TestAlgorithm.GetСhoice(viewModel.gameField.VisibleСells, viewModel.gameField.Marks,
                    viewModel.gameField.Size, viewModel.gameField.CountMines);
            viewModel.gameField.OpenCell(value);
            if (viewModel.gameField.Status == GameStatus.Play)
            {
                lblChose.Text = value.ToString();
            }
            lblStatus.Text = viewModel.gameField.Status.ToString();
            viewModel.Fill();
        }
    }
}
