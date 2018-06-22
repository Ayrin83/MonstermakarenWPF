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


        public int[,] type1Array;
        public int[,] type2Array;
        public int[,] typeNoneArray;
        public int[,] typeDefaultArray;
        public ButtonType selectedButtonType = ButtonType.TYPE1;

        public TypeSelector()
        {
            InitializeComponent();
            selectedButtonType = ButtonType.NONE;

            type1Array = new int[5, 5] { { 0, 0, 0, 0, 0 }, { 0, 1, 0, 0, 0 }, { 0, 0, 1, 0, 0 }, { 0, 0, 0, 1, 0 }, { 0, 0, 0, 0, 0 } };
            type2Array = new int[5, 5] { { 0, 0, 0, 0, 0 }, { 0, 1, 1, 1, 0 }, { 0, 1, 1, 1, 0 }, { 0, 1, 1, 1, 0 }, { 0, 0, 0, 0, 0 } };

            typeNoneArray = new int[5, 5] { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } };

            typeDefaultArray = new int[5, 5] { { 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1 } };

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
