using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AICar
{
    public class Player
    {
        public NeuralNet net;
        public List<LineSegment> baseCar;
        public List<LineSegment> baseRays;
        public List<float> baseRayDis;
        public PointF pos;
        public float rot;
        public float steer;
        public float speed;
        public int sightRadius = 60;
        public bool crashed;
        public Stopwatch running;

        public Player()
        {
            baseCar = new List<LineSegment>();
            baseCar.Add(new LineSegment(new Point(-10, 5), new Point(10, 5)));
            baseCar.Add(new LineSegment(new Point(10, 5), new Point(10, -5)));
            baseCar.Add(new LineSegment(new Point(10, -5), new Point(-10, -5)));
            baseCar.Add(new LineSegment(new Point(-10, -5), new Point(-10, 5)));
            baseRays = new List<LineSegment>();
            baseRays.Add(new LineSegment(new Point(4, 3), new Point(20, 23)));
            baseRays.Add(new LineSegment(new Point(4, 3), new Point(20, 13)));
            baseRays.Add(new LineSegment(new Point(4, 3), new Point(20, 8)));
            baseRays.Add(new LineSegment(new Point(4, 3), new Point(20, 3)));
            baseRays.Add(new LineSegment(new Point(4, 3), new Point(20, -2)));
            baseRays.Add(new LineSegment(new Point(4, 3), new Point(20, -7)));
            baseRays.Add(new LineSegment(new Point(4, 3), new Point(20, -17)));
            baseRayDis = new List<float>();
            baseRayDis.AddRange(new float[7]);
            net = new NeuralNet();
            running = new Stopwatch();
        }

        public void Render(Graphics g, float dx, float dy, int sizeY, Point BBoxMin, bool drawSight, bool drawRays)
        {
            Helper.DrawTransformed(g, Color.Red, baseCar, pos, rot, dx, dy, sizeY, BBoxMin);
            if(drawRays) Helper.DrawTransformed(g, Color.Blue, baseRays, pos, rot, dx, dy, sizeY, BBoxMin);
            if(drawSight) g.DrawEllipse(Pens.Gray, (pos.X - BBoxMin.X) * dx - sightRadius * dx, sizeY - ((pos.Y - BBoxMin.Y) * dy + sightRadius * dy), sightRadius * dx * 2, sightRadius * dy * 2);
            float[] inputs = new float[7];
            for (int i = 0; i < 7; i++)
                inputs[i] = (baseRayDis[i] / (float)sightRadius) * 2 - 1;
            float[] output;
            net.CalcOutput(inputs, out output);
            steer = output[0];
            speed = (output[1] + 1) * 5 + 2;
        }
    }
}
