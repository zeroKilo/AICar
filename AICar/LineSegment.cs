using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AICar
{
    public class LineSegment
    {
        public Point start;
        public Point end;
        public LineSegment() { }
        public LineSegment(Point s, Point e)
        {
            start = s;
            end = e;
        }
    }
}
