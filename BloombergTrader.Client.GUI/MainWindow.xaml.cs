using Autofac;
using BloombergTrader.Client.Services;
using BloombergTrader.Client.ViewModels;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
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
using ReactiveUI;
using MaterialDesignThemes.Wpf;

namespace BloombergTrader.Client.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        
        public MainWindow(IMainViewModel viewModel) : this()
        {
            DataContext = viewModel;
        }
        public MainWindow()
        {
            InitializeComponent();            
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleFlyout(0);
        }

        public void ToggleFlyout(int index)
        {
            var flyout = Flyouts.Items[index] as Flyout;
            if (flyout == null)
                return;
            flyout.IsOpen = !flyout.IsOpen;
        }
    }
 
}
