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

namespace PictureSplitter
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        private MainViewModel _mainViewModel;

        public MainView()
        {
            InitializeComponent();
            _mainViewModel = new MainViewModel(this);
            DataContext = _mainViewModel;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            _mainViewModel.NextPart();
        }
    }
}