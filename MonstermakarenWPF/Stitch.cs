using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;


namespace MonstermakarenWPF
{
    public class Stitch
    {
        private Point _topLeft, _topRight;
        private Point _bottomLeft, _bottomRight;
        
        private TypeSelector.ButtonType _stitchType;

        private Shape _shape;

        public Stitch()
        {
            _stitchType = TypeSelector.ButtonType.NONE;
        }

        public Stitch(Point topLeft, Point topRight, Point bottomLeft, Point bottomRight) : base()
        {
            _topLeft = topLeft;
            _topRight = topRight;
            _bottomLeft = bottomLeft;
            _bottomRight = bottomRight;

        }

        public TypeSelector.ButtonType stitchType
        {
            get
            {
                return _stitchType;
            }
            set
            {
                _stitchType = value;
            }
        }

        

    }
}
