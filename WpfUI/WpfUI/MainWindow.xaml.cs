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
            viewModel.Button_PreviewMouseDown((Button)sender, e.ChangedButton);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.NewGame();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            viewModel.Button_PreviewMouseDown((Button)sender, MouseButton.Middle);            
        }
    }
}
