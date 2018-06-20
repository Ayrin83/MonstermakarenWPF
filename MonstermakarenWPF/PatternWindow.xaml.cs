﻿using System;
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

        private int _distX_int; // if _distX_int < _distY_int -> _distY_int = _distX_int 
        private int _distY_int; // if _distY_int < _distX_int -> _distX_int = _distY_int 
        private Stitch[,] _stitches;

        private TypeSelector typeSelector;

        public PatternWindow()
        {
            InitializeComponent();

            typeSelector = new TypeSelector();
            typeSelector.Show();
        }

   
        public void drawRectangular(int numHorizontal, int numVertical)
        {
            _numHorizontal = numHorizontal;
            _numVertical = numVertical;

            int numPixelsInRow = (int)myImage.Width - 3 * MARGIN;
            _distanceX = (myImage.Width - 3 * MARGIN) / numHorizontal;
            _distanceY = (myImage.Height - 3 * MARGIN) / numVertical;

            _distX_int = (int)Math.Floor(_distanceX);
            _distY_int = (int)Math.Floor(_distanceY);

            if (_distX_int > _distY_int)
                _distX_int = _distY_int;
            else
                _distY_int = _distX_int;

            int pixelsX = _distX_int * numHorizontal;
            int pixelsY = _distY_int * numVertical;

            _stitches = new Stitch[_numHorizontal, _numVertical];

            WriteableBitmap writeablebitmap = new WriteableBitmap((int)this.ActualHeight, (int)this.ActualWidth, 96, 96, PixelFormats.Bgr32, null);

            myImage.Source = writeablebitmap;

            myImage.Stretch = Stretch.None;
            myImage.HorizontalAlignment = HorizontalAlignment.Left;
            myImage.VerticalAlignment = VerticalAlignment.Top;

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

                    if ( y >= MARGIN && y <= pixelsY + MARGIN && (y - MARGIN) % _distY_int == 0) // Horisontal line
                    {
                        for (int x = 0; x < myImage.Width; x++)
                        {
                            if (x > MARGIN && x - MARGIN < _distX_int*numHorizontal)
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
                            if (y >= MARGIN && y - MARGIN <= _distY_int * numVertical && x >= MARGIN && x - MARGIN <= _distX_int * numHorizontal && (x - MARGIN) % _distX_int == 0)
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
                }                               
            }

            writeablebitmap.AddDirtyRect(new Int32Rect(0, 0, (int)this.ActualWidth, (int)this.ActualHeight));
            writeablebitmap.Unlock();
        }


        private void myImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {          
            Point hit = e.GetPosition(myImage);

            double X = (hit.X - MARGIN) / _distX_int;
            int x = (int)Math.Floor(X);

            double Y = (hit.Y - MARGIN) / _distY_int;
            int y = (int)Math.Floor(Y);

            Logger.Log("Hit! " + hit.ToString() + " x: " + x + " y: " + y);

            if (x < 0 || x > _numHorizontal)
            {
                Logger.Log("Invalid x-index (" + x + ")");
                return;
            }
            if (y < 0 || y > _numVertical)
            {
                Logger.Log("Invalid y-index (" + y + ")");
                return;
            }


            TypeSelector.ButtonType buttonType;
            try
            {
                buttonType = typeSelector.selectedButtonType;
            }
            catch
            {
                buttonType = TypeSelector.ButtonType.NONE;
            }

            try
            {
                _stitches[x, y].stitchType = buttonType;
            }
            catch (Exception ex)
            {
                Logger.Log("myCanvas_MouseLeftButtonUp, exception " + ex.Message);
                try
                {
                    _stitches[x, y] = new Stitch(buttonType);
                }
                catch
                {
                    Logger.Log("Unable to create new stitch in " + x + ", " + y);
                }
                return;
            }


        }
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
}
