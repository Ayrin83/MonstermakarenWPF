using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MonstermakarenWPF
{
    public class Stitch
    {
        private Point _topLeft, _topRight;
        private Point _bottomLeft, _bottomRight;
        

        private System.Drawing.Color _backColor;

        private int groupNum;

        public Stitch()
        {

        }

        public Stitch(Point topLeft, Point topRight, Point bottomLeft, Point bottomRight)
        {
            _topLeft = topLeft;
            _topRight = topRight;
            _bottomLeft = bottomLeft;
            _bottomRight = bottomRight;
        }

        

    }
}
