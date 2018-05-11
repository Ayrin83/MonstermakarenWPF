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
using System.Windows.Shapes;

namespace MonstermakarenWPF
{
    /// <summary>
    /// Interaction logic for TypeSelector.xaml
    /// </summary>
    public partial class TypeSelector : Window
    {
        public enum ButtonType
        {
            NONE,
            TYPE1,
            TYPE2
        };

        public ButtonType selectedButtonType = ButtonType.TYPE1;

        public TypeSelector()
        {
            InitializeComponent();
        }

        private void ButtonType1_Click(object sender, RoutedEventArgs e)
        {
            selectedButtonType = ButtonType.TYPE1;
        }

        private void ButtonType2_Click(object sender, RoutedEventArgs e)
        {
            selectedButtonType = ButtonType.TYPE2;
        }
    }
}
