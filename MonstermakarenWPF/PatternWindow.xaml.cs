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

        //public void drawRectangular(int numHorizontal, int numVertical)
        //{
        //    //List<Point> verticalPoints = new List<Point>(numHorizontal + 1);
        //    const int MARGIN = 20;
        //    double distanceX = (myCanvas.Width - 3*MARGIN) / numHorizontal;
        //    double distanceY = (myCanvas.Height - 3*MARGIN) / numVertical;
            
        //    // Create a path to draw a geometry with.
        //    Path myPath = new Path();
        //    myPath.Stroke = Brushes.Black;
        //    myPath.StrokeThickness = 1;

        //    // Create a StreamGeometry to use to specify myPath.
        //    StreamGeometry geometry = new StreamGeometry();
        //    geometry.FillRule = FillRule.EvenOdd;

        //    using (StreamGeometryContext sgc = geometry.Open())
        //    {
        //        for (int x = 0; x < numHorizontal; x++)
        //        {
        //            for (int y = 0; y < numVertical; y++)
        //            {
        //                sgc.BeginFigure(new Point(MARGIN + distanceX * x, MARGIN + distanceY * y), true, true);
        //                sgc.LineTo(new Point(MARGIN + distanceX * (x + 1), MARGIN + distanceY * y), true, false);
        //                sgc.LineTo(new Point(MARGIN + distanceX * (x + 1), MARGIN + distanceY * (y + 1)), true, false);
        //                sgc.LineTo(new Point(MARGIN + distanceX * x, MARGIN + distanceY * (y + 1)), true, false);

        //                //verticalPoints[x + y * x] = new Point(10 + distanceX * x, 10 + distanceY * );
        //            }
        //        }
        //        //sgc.BeginFigure(new Point(10, 10), true, true);
        //        //sgc.LineTo(new Point(10, 100), true, false);
        //        //sgc.LineTo(new Point(100, 100), true, false);
        //        //sgc.LineTo(new Point(100, 10), true, false);
        //        sgc.Close();
        //    }
        //    myPath.Data = geometry;
        //    myCanvas.Children.Add(myPath);
            
        //}

        public void drawRectangular(int numHorizontal, int numVertical)
        {
            WriteableBitmap writeablebitmap = new WriteableBitmap((int)this.ActualHeight, (int)this.ActualWidth, 96, 96, PixelFormats.Bgr32, null);

            myImage.Source = writeablebitmap;

            myImage.Stretch = Stretch.None;
            myImage.HorizontalAlignment = HorizontalAlignment.Left;
            myImage.VerticalAlignment = VerticalAlignment.Top;

            const int MARGIN = 20;
            int numPixelsInRow = (int)myImage.Width - 3 * MARGIN;
            double distanceX = (myImage.Width - 3 * MARGIN) / numHorizontal;
            double distanceY = (myImage.Height - 3 * MARGIN) / numVertical;

            int distX_int = (int)Math.Floor(distanceX);
            int distY_int = (int)Math.Floor(distanceY);

            Int32 white = 0x00ffffff; // RGB
            Int32 black = 0x00000000; // RGB

            int pBackbuffer = (int)writeablebitmap.BackBuffer;
            //pBackbuffer += MARGIN * writeablebitmap.BackBufferStride; // jump MARGIN number of rows

            int startOfRow;
 
            //pBackbuffer += MARGIN * 4; // Jump MARGIN number of columns 

            writeablebitmap.Lock();

            unsafe
            {
                for (int y = 0; y < myImage.Height; y++)
                {
                    startOfRow = pBackbuffer;

                    if (y == MARGIN || y > MARGIN && (y - MARGIN) % distY_int == 0) // Horisontal line
                    {
                        for (int x = 0; x < myImage.Width; x++)
                        {
                            if (x > MARGIN && x - MARGIN < distX_int*numHorizontal)
                            {
                                *((int*)pBackbuffer) = black;
                            }
                            else
                            {
                                *((int*)pBackbuffer) = white;
                            }
                            pBackbuffer += 4;
                        }
                    }
                    else
                    {
                        for (int x = 0; x < myImage.Width; x++)
                        {
                            if (y >= MARGIN && y - MARGIN <= distY_int * numVertical && x >= MARGIN && x - MARGIN <= distX_int * numHorizontal && (x - MARGIN) % distX_int == 0)
                            {
                                *((int*)pBackbuffer) = black;
                            }
                            else
                            {
                                *((int*)pBackbuffer) = white;
                            }
                            pBackbuffer += 4;
                        }
                    }                 

                    //backMargin = writeablebitmap.BackBufferStride - (pBackbuffer - startOfRow);
                    //pBackbuffer += backMargin;
                   
                }                               
            }

            writeablebitmap.AddDirtyRect(new Int32Rect(0, 0, (int)this.ActualWidth, (int)this.ActualHeight));
            writeablebitmap.Unlock();
        }


        private void myImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
           
            Point hit = e.GetPosition(myImage);

            Logger.Log("Hit! " + hit.ToString());
        }
    }
}
