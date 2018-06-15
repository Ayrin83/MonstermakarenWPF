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

namespace MonstermakarenWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public PatternWindow patternwindow;

        public MainWindow()
        {
            InitializeComponent();

            patternwindow = new PatternWindow();

        }

        private void buttonRectangular_Click(object sender, RoutedEventArgs e)
        {
            Logger.Log("buttonRectangular_Click");
            patternwindow.Show();
            patternwindow.drawRectangular(10, 5);
            //patternwindow.drawRectangular(Convert.ToInt32(textBoxNumTotalHorizontalSt), Convert.ToInt32(textBoxNumTotalVerticalSt));
        }
    }
}
