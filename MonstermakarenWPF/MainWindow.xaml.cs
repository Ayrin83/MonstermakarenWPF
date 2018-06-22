using System.Windows;

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
            patternwindow.drawRectangular(5, 10);
            //patternwindow.drawRectangular(Convert.ToInt32(textBoxNumTotalHorizontalSt), Convert.ToInt32(textBoxNumTotalVerticalSt));
        }
    }
}
