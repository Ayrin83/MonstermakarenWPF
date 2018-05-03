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
        public PatternWindow()
        {
            InitializeComponent();
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

        public void drawRectangular(int numHorizontal, int numVertical)
        {
            //List<Point> verticalPoints = new List<Point>(numHorizontal + 1);
            const int MARGIN = 20;
            double distanceX = (myCanvas.Width - 3*MARGIN) / numHorizontal;
            double distanceY = (myCanvas.Height - 3*MARGIN) / numVertical;
            
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
                        sgc.BeginFigure(new Point(MARGIN + distanceX * x, MARGIN + distanceY * y), true, true);
                        sgc.LineTo(new Point(MARGIN + distanceX * (x + 1), MARGIN + distanceY * y), true, false);
                        sgc.LineTo(new Point(MARGIN + distanceX * (x + 1), MARGIN + distanceY * (y + 1)), true, false);
                        sgc.LineTo(new Point(MARGIN + distanceX * x, MARGIN + distanceY * (y + 1)), true, false);

                        //verticalPoints[x + y * x] = new Point(10 + distanceX * x, 10 + distanceY * );
                    }
                }
                //sgc.BeginFigure(new Point(10, 10), true, true);
                //sgc.LineTo(new Point(10, 100), true, false);
                //sgc.LineTo(new Point(100, 100), true, false);
                //sgc.LineTo(new Point(100, 10), true, false);
                sgc.Close();
            }
            myPath.Data = geometry;
            myCanvas.Children.Add(myPath);
            
        }

        private void myCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
           
            Point hit = e.GetPosition(myCanvas);

            Logger.Log("Hit! " + hit.ToString());
        }
    }
}
