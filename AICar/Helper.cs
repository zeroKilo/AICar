using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AICar
{
    public static class Helper
    {
        public static Random rnd = new Random();

        public const float pif = 3.1415f / 180f;

        public static void DrawTransformed(Graphics g, Color c, List<LineSegment> list, PointF pos, float r, float dx, float dy, int sizeY, Point BBoxMin)
        {
            Pen p = new Pen(c);
            foreach (LineSegment seg in list)
            {
                float x1 = (seg.start.X * Cos(r) + seg.start.Y * Sin(r)) - BBoxMin.X + pos.X;
                float y1 = (-seg.start.X * Sin(r) + seg.start.Y * Cos(r)) - BBoxMin.Y + pos.Y;
                float x2 = (seg.end.X * Cos(r) + seg.end.Y * Sin(r)) - BBoxMin.X + pos.X;
                float y2 = (-seg.end.X * Sin(r) + seg.end.Y * Cos(r)) - BBoxMin.Y + pos.Y;
                x1 *= dx;
                y1 *= dy;
                x2 *= dx;
                y2 *= dy;
                g.DrawLine(p, x1, sizeY - y1, x2, sizeY - y2);
            }
        }

        public static void DrawMarker(Graphics g, Color c, Point pos, float dx, float dy, int sizeY, Point BBoxMin)
        {
            g.DrawEllipse(new Pen(c), (pos.X - BBoxMin.X) * dx - 5, sizeY - ((pos.Y - BBoxMin.Y) * dy) - 5, 10, 10);
        }

        public static bool FindRayCollisionPoint(float x, float y, float dx, float dy, float x1, float y1, float x2, float y2, out Point result)
        {
            result = new Point();
            float r, s, d;
            if (dy / dx != (y2 - y1) / (x2 - x1))
            {
                d = ((dx * (y2 - y1)) - dy * (x2 - x1));
                if (d != 0)
                {
                    r = (((y - y1) * (x2 - x1)) - (x - x1) * (y2 - y1)) / d;
                    s = (((y - y1) * dx) - (x - x1) * dy) / d;
                    if (r >= 0 && s >= 0 && s <= 1)
                    {
                        result = new Point((int)(x + r * dx), (int)(y + r * dy));
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool FindIntersectionPoint(LineSegment segA, LineSegment segB, out Point result)
        {
            result = new Point();
            bool isAV = (segA.start.X == segA.end.X);
            bool isAH = (segA.start.Y == segA.end.Y);
            bool isBV = (segB.start.X == segB.end.X);
            bool isBH = (segB.start.Y == segB.end.Y);
            if (isAV && isBV) return false;//parallel vertikal
            if (isAH && isBH) return false;//parallel horizontal
            if (isAH && isBV)//intersection h - v
            {
                result = new Point(segB.start.X, segA.start.Y);
                return isPointInSegment(segA, result) && isPointInSegment(segB, result);
            }
            if (isAV && isBH) return FindIntersectionPoint(segB, segA, out result);//intersection v-h
            if (isBV || isBH) return FindIntersectionPoint(segB, segA, out result);//if only one is h or v make it segA
            if (isAH) //only A is horizontal
            {
                result = new Point((int)getXAtY(segB, segA.start.Y), segA.start.Y);
                return isPointInSegment(segA, result) && isPointInSegment(segB, result);
            }
            if (isAV) //only A is vertical
            {
                result = new Point(segA.start.X, (int)getYAtX(segB, segA.start.X));
                return isPointInSegment(segA, result) && isPointInSegment(segB, result);
            }
            //both steep
            //y = Ax + B
            PointF fx1 = getFxForm(segA);
            PointF fx2 = getFxForm(segB);
            //Ax - y = -B
            float A1 = fx1.X;
            float B1 = -1;
            float C1 = -fx1.Y;
            float A2 = fx2.X;
            float B2 = -1;
            float C2 = -fx2.Y;
            //Ax + By = C
            float delta = A1 * B2 - A2 * B1;
            if (delta == 0) return false;
            result.X = (int)((B2 * C1 - B1 * C2) / delta);
            result.Y = (int)((A1 * C2 - A2 * C1) / delta);
            return isPointInSegment(segA, result) && isPointInSegment(segB, result);
        }

        public static bool isPointInSegment(LineSegment seg, Point p)
        {
            LineSegment box = getBBox(seg);
            return (p.X >= box.start.X) &&
                   (p.X <= box.end.X) &&
                   (p.Y >= box.start.Y) &&
                   (p.Y <= box.end.Y);
        }

        public static LineSegment getBBox(LineSegment seg)
        {
            Point min = new Point(1000000, 100000);
            Point max = new Point(-1000000, -100000);
            if (seg.start.X < min.X) min.X = seg.start.X;
            if (seg.start.Y < min.Y) min.Y = seg.start.Y;
            if (seg.start.X > max.X) max.X = seg.start.X;
            if (seg.start.Y > max.Y) max.Y = seg.start.Y;
            if (seg.end.X < min.X) min.X = seg.end.X;
            if (seg.end.Y < min.Y) min.Y = seg.end.Y;
            if (seg.end.X > max.X) max.X = seg.end.X;
            if (seg.end.Y > max.Y) max.Y = seg.end.Y;
            return new LineSegment(new Point(min.X, min.Y), new Point(max.X, max.Y));
        }

        public static float getYAtX(LineSegment seg, int x)
        {
            PointF fx = getFxForm(seg);
            return fx.X * x + fx.Y;
        }

        public static float getXAtY(LineSegment seg, int y)
        {
            PointF fx = getFxForm(seg);
            return (y - fx.Y) / fx.X;
        }

        public static PointF getFxForm(LineSegment seg)
        {
            PointF result = new PointF();
            result.X = (seg.end.Y - seg.start.Y) / (float)(seg.end.X - seg.start.X);
            result.Y = seg.start.Y - (result.X * seg.start.X);
            return result;
        }

        public static float Pow2(float f)
        {
            return f * f;
        }

        public static float Sin(float r)
        {
            return (float)Math.Sin(r * pif);
        }

        public static float Cos(float r)
        {
            return (float)Math.Cos(r * pif);
        }

        public static float Sigmoid(double x)
        {
            return 2 / (1 + (float)Math.Exp(-2 * x)) - 1;
        }

        public static void WriteInt(Stream s, int v)
        {
            s.Write(BitConverter.GetBytes(v), 0, 4);
        }

        public static void WriteFloat(Stream s, float v)
        {
            s.Write(BitConverter.GetBytes(v), 0, 4);
        }

        public static int ReadInt(Stream s)
        {
            byte[] buff = new byte[4];
            s.Read(buff, 0, 4);
            return BitConverter.ToInt32(buff, 0);
        }

        public static float ReadFloat(Stream s)
        {
            byte[] buff = new byte[4];
            s.Read(buff, 0, 4);
            return BitConverter.ToSingle(buff, 0);
        }
    }
}
