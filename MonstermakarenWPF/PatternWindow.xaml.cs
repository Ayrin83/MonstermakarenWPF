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
    /// Interaction logic for PatternWindow.xaml
    /// </summary>
    public partial class PatternWindow : Window
    {

        private const int MARGIN = 20;

        private int _numHorizontal;
        private int _numVertical;
        private double _distanceX;
        private double _distanceY;
        private Stitch[,] _stitches;

        public PatternWindow()
        {
            InitializeComponent();
        }

       

        public void drawRectangular(int numHorizontal, int numVertical)
        {
            const int MARGIN = 20;

            _numHorizontal = numHorizontal;
            _numVertical = numVertical;

            _distanceX = (myCanvas.Width - 3 * MARGIN) / numHorizontal;
            _distanceY = (myCanvas.Height - 3 * MARGIN) / numVertical;

            _stitches = new Stitch[numHorizontal, numVertical];

            Point topLeft, topRight, bottomLeft, bottomRight;
            
            // Create a path to draw a geometry with.
            Path myPath = new Path();
            myPath.Stroke = Brushes.Black;
            myPath.StrokeThickness = 1;

            // Create a StreamGeometry to use to specify myPath.
            StreamGeometry geometry = new StreamGeometry();
            geometry.FillRule = FillRule.EvenOdd;

            using (StreamGeometryContext sgc = geometry.Open())
            {
                for (int x = 0; x < numHorizontal; x++)
                {
                    for (int y = 0; y < numVertical; y++)
                    {
                        topLeft = new Point(MARGIN + _distanceX * x, MARGIN + _distanceY * y);
                        topRight = new Point(MARGIN + _distanceX * (x + 1), MARGIN + _distanceY * y);
                        bottomRight = new Point(MARGIN + _distanceX * (x + 1), MARGIN + _distanceY * (y + 1));
                        bottomLeft = new Point(MARGIN + _distanceX * x, MARGIN + _distanceY * (y + 1));

                        sgc.BeginFigure(topLeft, true, true);
                        sgc.LineTo(topRight, true, false);
                        sgc.LineTo(bottomRight, true, false);
                        sgc.LineTo(bottomLeft, true, false);

                        _stitches[x, y] = new Stitch(topLeft, topRight, bottomLeft, bottomRight);
                    }
                }
                
                sgc.Close();
            }

            myPath.Data = geometry;
            myCanvas.Children.Add(myPath);          
        }

        private void myCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
           
            Point hit = e.GetPosition(myCanvas);
            
            double X = (hit.X - MARGIN) / _distanceX;
            int x = (int)Math.Floor(X);

            double Y = (hit.Y - MARGIN) / _distanceY;
            int y = (int)Math.Floor(Y);

            Logger.Log("Hit! " + hit.ToString() + " x: " + x +  " y: " + y);
        }

        //public void drawRectangular_X(int numHorizontal, int numVertical)
        //{
        //    //List<Point> verticalPoints = new List<Point>(numHorizontal + 1);
        //    const int MARGIN = 20;
        //    double distanceX = (myCanvas.Width - 3 * MARGIN) / numHorizontal;
        //    double distanceY = (myCanvas.Height - 3 * MARGIN) / numVertical;

        //    Rectangle myRectangle = new Rectangle();
        //    myRectangle.Width = distanceX;
        //    myRectangle.Height = distanceY;
        //    myRectangle.Stroke = Brushes.Black;

        //    myRectangle.

        //    for (int x = 0; x < numHorizontal; x++)
        //    {
        //        for (int y = 0; y < numVertical; y++)
        //        {


        //            // Create a path to draw a geometry with.
        //            Path myPath = new Path();
        //    myPath.Stroke = Brushes.Black;
        //    myPath.StrokeThickness = 1;


        //    myCanvas.Children.Add(myPath);

        //}
    }

}
