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

        private const Int32 WHITE = 0x00ffffff; // RGB
        private const Int32 BLACK = 0x00000000; // RGB


        private int _numHorizontal;
        private int _numVertical;
        private double _distanceX;
        private double _distanceY;

        private int _numPixelsInStitch_X; // if _distX_int < _distY_int -> _distY_int = _distX_int 
        private int _numPixelsInStitch_Y; // if _distY_int < _distX_int -> _distX_int = _distY_int 
        private Stitch[,] _stitches;

        private TypeSelector typeSelector;

        private WriteableBitmap _writeableBitmap;

        int _pixelsX;
        int _pixelsY;

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

            _numPixelsInStitch_X = (int)Math.Floor(_distanceX);
            _numPixelsInStitch_Y = (int)Math.Floor(_distanceY);

            if (_numPixelsInStitch_X > _numPixelsInStitch_Y)
                _numPixelsInStitch_X = _numPixelsInStitch_Y;
            else
                _numPixelsInStitch_Y = _numPixelsInStitch_X;

            _pixelsX = _numPixelsInStitch_X * numHorizontal;
            _pixelsY = _numPixelsInStitch_Y * numVertical;

            _stitches = new Stitch[_numHorizontal, _numVertical];

            _writeableBitmap = new WriteableBitmap((int)this.ActualHeight, (int)this.ActualWidth, 96, 96, PixelFormats.Bgr32, null);

            myImage.Source = _writeableBitmap;

            myImage.Stretch = Stretch.None;
            myImage.HorizontalAlignment = HorizontalAlignment.Left;
            myImage.VerticalAlignment = VerticalAlignment.Top;

            

            int pBackbuffer = (int)_writeableBitmap.BackBuffer;
            //pBackbuffer += MARGIN * writeablebitmap.BackBufferStride; // jump MARGIN number of rows

            //pBackbuffer += MARGIN * 4; // Jump MARGIN number of columns 

            _writeableBitmap.Lock();

            unsafe
            {
                for (int y = 0; y < myImage.Height; y++)
                {
                    if ( y >= MARGIN && y <= _pixelsY + MARGIN && (y - MARGIN) % _numPixelsInStitch_Y == 0) // Horisontal line
                    {
                        for (int x = 0; x < myImage.Width; x++)
                        {
                            if (x > MARGIN && x - MARGIN < _numPixelsInStitch_X * numHorizontal)
                            {
                                *((int*)pBackbuffer) = BLACK;
                            }
                            else
                            {
                                *((int*)pBackbuffer) = WHITE;
                            }
                            pBackbuffer += 4;
                        }
                    }
                    else
                    {
                        for (int x = 0; x < myImage.Width; x++)
                        {
                            if (y >= MARGIN && y - MARGIN <= _numPixelsInStitch_Y * numVertical && x >= MARGIN && x - MARGIN <= _numPixelsInStitch_X * numHorizontal && (x - MARGIN) % _numPixelsInStitch_X == 0)
                            {
                                *((int*)pBackbuffer) = BLACK;
                            }
                            else
                            {
                                *((int*)pBackbuffer) = WHITE;
                            }
                            pBackbuffer += 4;
                        }
                    }                 
                }                               
            }

            _writeableBitmap.AddDirtyRect(new Int32Rect(0, 0, (int)this.ActualWidth, (int)this.ActualHeight));
            _writeableBitmap.Unlock();
        }


        /// <summary>
        /// Redraw the rectangle-pattern in the given area.
        /// </summary>
        /// <param name="startPixelX"></param>
        /// <param name="startPixelY"></param>
        /// <param name="numPixelX"></param>
        /// <param name="numPixelY"></param>
        private void reDrawRectanglesInArea(int startPixelX, int startPixelY, int numPixelX, int numPixelY)
        {
            int pBackbuffer = (int)_writeableBitmap.BackBuffer;
            //int startOfRow;

            int stopPixelX = startPixelX + numPixelX;
            int stopPixelY = startPixelY + numPixelY;          

            _writeableBitmap.Lock();
            unsafe
            {
                for (int y = startPixelY; y < stopPixelY; y++)
                {
                    //startOfRow = pBackbuffer;

                    if (y >= MARGIN && y <= _pixelsY + MARGIN && (y - MARGIN) % _numPixelsInStitch_Y == 0) // Horisontal line
                    {
                        for (int x = 0; x < myImage.Width; x++)
                        {
                            if (x > MARGIN && x - MARGIN < _numPixelsInStitch_X * _numHorizontal)
                            {
                                *((int*)pBackbuffer) = BLACK;
                            }
                            else
                            {
                                *((int*)pBackbuffer) = WHITE;
                            }
                            pBackbuffer += 4;
                        }
                    }
                    else
                    {
                        for (int x = startPixelX; x < stopPixelX; x++)
                        {
                            if (y >= MARGIN && y - MARGIN <= _numPixelsInStitch_Y * _numVertical && x >= MARGIN && x - MARGIN <= _numPixelsInStitch_X * _numHorizontal && (x - MARGIN) % _numPixelsInStitch_X == 0)
                            {
                                *((int*)pBackbuffer) = BLACK;
                            }
                            else
                            {
                                *((int*)pBackbuffer) = WHITE;
                            }
                            pBackbuffer += 4;
                        }
                    }
                }
            }

            _writeableBitmap.AddDirtyRect(new Int32Rect(startPixelX, startPixelY, numPixelX, numPixelY));
            _writeableBitmap.Unlock();
        }


        private void reDrawStitches(int xIndex, int yIndex, int numXstitches, int numYstitches)
        {
            int startpixelX = xIndex * _numPixelsInStitch_X + MARGIN;
            int startpixelY = yIndex * _numPixelsInStitch_Y + MARGIN;

            int stopPixelX = (xIndex + numXstitches + 1) * _numPixelsInStitch_X + MARGIN;
            int stopPixelY = (yIndex + numYstitches + 1) * _numPixelsInStitch_Y + MARGIN;

            int[,] currentPattern;

            try
            {
                switch (_stitches[xIndex, yIndex].stitchType)
                {
                    case TypeSelector.ButtonType.NONE:
                        currentPattern = typeSelector.type2Array;
                        break;

                    case TypeSelector.ButtonType.TYPE1:
                        currentPattern = typeSelector.type1Array;
                        break;

                    case TypeSelector.ButtonType.TYPE2:
                        currentPattern = typeSelector.type2Array;
                        break;

                    default:
                        currentPattern = typeSelector.typeDefaultArray; ;
                        break;

                }
            }
            catch(Exception ex)
            {
                Logger.Log("reDrawStitches, exception: " + ex.Message);
                currentPattern = typeSelector.typeDefaultArray;
            }

            double actualRepeat = (double)_numPixelsInStitch_X / typeSelector.type1Array.GetLength(0);
            int repeat = (int)Math.Floor(actualRepeat);
            int numRepeatsX = 0;
            int numRepeatsY = 0;

            int pBackbuffer = (int)_writeableBitmap.BackBuffer;

            int px = 0;
            int py = 0;
            int currentcolor;

            pBackbuffer += startpixelX * 4 + (startpixelY + 1) * _writeableBitmap.BackBufferStride + 4;
                       
            _writeableBitmap.Lock();
            unsafe
            {
                for (int y = 0; y < _numPixelsInStitch_Y - 1; y++)
                {
                    for (int x = 0; x < _numPixelsInStitch_X - 1; x++)
                    {
                        pBackbuffer += 4;

                        currentcolor = currentPattern[px, py];
                        if (currentPattern[px,py] == 1)
                            *((int*)pBackbuffer) = BLACK;
                        else
                            *((int*)pBackbuffer) = WHITE;                       

                        if (++numRepeatsX % repeat == 0 && px < 4)
                        {
                            px++;
                        }
                    }

                    pBackbuffer += _writeableBitmap.BackBufferStride - _numPixelsInStitch_X * 4 + 4;
                    if (++numRepeatsY % repeat == 0 && py < 4)
                    {
                        py++;
                    }
                    px = 0;
                }
            }
            _writeableBitmap.AddDirtyRect(new Int32Rect(startpixelX, startpixelY, _numPixelsInStitch_X, _numPixelsInStitch_Y));
            _writeableBitmap.Unlock();

        }

        private void reDrawStitches()
        {

        }

        private void myImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {          
            Point hit = e.GetPosition(myImage);

            double X = (hit.X - MARGIN) / _numPixelsInStitch_X;
            int xIndex = (int)Math.Floor(X);

            double Y = (hit.Y - MARGIN) / _numPixelsInStitch_Y;
            int yIndex = (int)Math.Floor(Y);

            Logger.Log("Hit! " + hit.ToString() + " x: " + xIndex + " y: " + yIndex);

            if (xIndex < 0 || xIndex > _numHorizontal)
            {
                Logger.Log("Invalid x-index (" + xIndex + ")");
                return;
            }
            if (yIndex < 0 || yIndex > _numVertical)
            {
                Logger.Log("Invalid y-index (" + yIndex + ")");
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
                _stitches[xIndex, yIndex].stitchType = buttonType;
            }
            catch (Exception ex)
            {
                Logger.Log("myCanvas_MouseLeftButtonUp, exception " + ex.Message);
                try
                {
                    _stitches[xIndex, yIndex] = new Stitch(buttonType);
                }
                catch
                {
                    Logger.Log("Unable to create new stitch in " + xIndex + ", " + yIndex);
                }
                return;
            }

            reDrawStitches(xIndex, yIndex, 1, 1);


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
