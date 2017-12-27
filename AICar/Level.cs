using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace AICar
{
    public class Level
    {

        public List<LineSegment> borders;
        public List<Player> players;
        public Point BBoxMin, BBoxMax;
        public int BBoxSize;
        public Point start;
        public int generation;
        public int test;
        public NeuralNet bestNet;
        public long bestTime = 0;

        public bool drawPoints = true;
        public bool drawSight = true;
        public bool drawRays = true;
        public bool dontEvolve = false;

        public Level()
        {
            players = new List<Player>();
            players.Add(new Player());
            borders = new List<LineSegment>();
            BBoxMax = BBoxMin = new Point();
            generation = 0;
            test = 0;
        }

        public void ImportDXF(string filename)
        {
            borders = new List<LineSegment>();
            string[] lines = File.ReadAllLines(filename);
            BBoxMin = new Point(1000000, 100000);
            BBoxMax = new Point(-1000000, -100000);
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] != "AcDbLine") continue;
                LineSegment seg = new LineSegment();
                seg.start = new Point(Convert.ToInt32(lines[i + 2]), Convert.ToInt32(lines[i + 4]));
                seg.end = new Point(Convert.ToInt32(lines[i + 6]), Convert.ToInt32(lines[i + 8]));
                if (seg.start.X < BBoxMin.X) BBoxMin.X = seg.start.X;
                if (seg.start.Y < BBoxMin.Y) BBoxMin.Y = seg.start.Y;
                if (seg.start.X > BBoxMax.X) BBoxMax.X = seg.start.X;
                if (seg.start.Y > BBoxMax.Y) BBoxMax.Y = seg.start.Y;
                borders.Add(seg);
            }
            BBoxSize = (int)Math.Sqrt(Helper.Pow2(BBoxMax.X - BBoxMin.X) + Helper.Pow2(BBoxMax.Y - BBoxMin.Y));
        }

        public void Load(string filename)
        {
            borders = new List<LineSegment>();
            string[] lines = File.ReadAllLines(filename);
            start = new Point();
            foreach (string line in lines)
            {
                string[] parts = line.Split(';');
                switch (parts[0])
                {
                    case "bbminx": BBoxMin.X = Convert.ToInt32(parts[1]); break;
                    case "bbminy": BBoxMin.Y = Convert.ToInt32(parts[1]); break;
                    case "bbmaxx": BBoxMax.X = Convert.ToInt32(parts[1]); break;
                    case "bbmaxy": BBoxMax.Y = Convert.ToInt32(parts[1]); break;
                    case "bbsize": BBoxSize = Convert.ToInt32(parts[1]); break;
                    case "posx": start.X = Convert.ToInt32(parts[1]); break;
                    case "posy": start.Y = Convert.ToInt32(parts[1]); break;                        
                    case "seg_b":
                        LineSegment seg = new LineSegment();
                        seg.start = new Point(Convert.ToInt32(parts[1]), Convert.ToInt32(parts[2]));
                        seg.end = new Point(Convert.ToInt32(parts[3]), Convert.ToInt32(parts[4]));
                        borders.Add(seg);
                        break;
                }
            }
            bestNet = players[0].net.Clone();
            foreach (Player p in players)
                p.pos = start;
        }

        public void Save(string filename)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("bbminx;" + BBoxMin.X);
            sb.AppendLine("bbminy;" + BBoxMin.Y);
            sb.AppendLine("bbmaxx;" + BBoxMax.X);
            sb.AppendLine("bbmaxy;" + BBoxMax.Y);
            sb.AppendLine("bbsize;" + BBoxSize);
            sb.AppendLine("posx;" + (int)players[0].pos.X);
            sb.AppendLine("posy;" + (int)players[0].pos.Y);
            foreach (LineSegment seg in borders)
                sb.AppendFormat("seg_b;{0};{1};{2};{3}\n", seg.start.X, seg.start.Y, seg.end.X, seg.end.Y);
            File.WriteAllText(filename, sb.ToString());
        }

        public Bitmap Render(int sizeX, int sizeY)
        {
            Bitmap result = new Bitmap(sizeX, sizeY);
            GC.Collect();
            Graphics g = Graphics.FromImage(result);
            g.Clear(Color.White);
            float dx = sizeX / (float)(BBoxMax.X - BBoxMin.X);
            float dy = sizeY / (float)(BBoxMax.Y - BBoxMin.Y);
            foreach (LineSegment seg in borders)
                g.DrawLine(Pens.Black, (seg.start.X - BBoxMin.X) * dx, sizeY - (seg.start.Y - BBoxMin.Y) * dy, (seg.end.X - BBoxMin.X) * dx, sizeY - (seg.end.Y - BBoxMin.Y) * dy);
            int count = 0;
            foreach (Player player in players)
            {
                player.Render(g, dx, dy, sizeY, BBoxMin, drawSight, drawRays);
                player.crashed = false;
                CheckRayCollisions(g, dx, dy, sizeY, player);
                CheckCarCollisions(g, dx, dy, sizeY, player);
                g.DrawString(player.running.ElapsedMilliseconds + " / " + bestTime, SystemFonts.DefaultFont, new SolidBrush(Color.Black), new Point(10, 30 + (count++) * 10));
            }
            g.DrawString(test.ToString(), SystemFonts.DefaultFont, new SolidBrush(Color.Black), new Point(10, 10));
            g.DrawString(generation.ToString(), SystemFonts.DefaultFont, new SolidBrush(Color.Black), new Point(10, 20));
            return result;
        }

        public void Update()
        {
            foreach (Player p in players)
            {
                if (!p.running.IsRunning) p.running.Start();
                p.rot += p.steer * 20f;
                if (!p.crashed)
                {
                    p.pos.X += Helper.Cos(p.rot) * p.speed;
                    p.pos.Y -= Helper.Sin(p.rot) * p.speed;
                    long time = p.running.ElapsedMilliseconds;
                    if (time > bestTime + 10000)// 10 sec better?
                    {
                        test++;
                        generation++;
                        bestTime = time;
                        if (!dontEvolve) bestNet = p.net.Clone();
                        p.pos = start;
                        p.rot = 0;
                        p.running.Restart();
                        p.baseRayDis = new List<float>(new float[7]);
                    }
                }
                else
                {
                    test++;
                    p.running.Stop();
                    long time = p.running.ElapsedMilliseconds;
                    if (time > bestTime)//better?
                    {
                        generation++;
                        bestTime = time;
                        if (!dontEvolve) 
                            bestNet = p.net.Clone();
                        else
                            p.net = bestNet;
                    }
                    else
                    {
                        p.net = bestNet.Clone();
                        if(!dontEvolve) p.net.Randomize();
                    }
                    p.pos = start;
                    p.rot = 0;
                    p.running.Restart();
                    p.baseRayDis = new List<float>(new float[7]);
                }
            }
        }

        public void CheckCarCollisions(Graphics g, float dx, float dy, int sizeY, Player p)
        {
            foreach (LineSegment seg in p.baseCar)
            {
                float r = p.rot;
                float x1 = (seg.start.X * Helper.Cos(r) + seg.start.Y * Helper.Sin(r)) + p.pos.X;
                float y1 = (-seg.start.X * Helper.Sin(r) + seg.start.Y * Helper.Cos(r)) + p.pos.Y;
                float x2 = (seg.end.X * Helper.Cos(r) + seg.end.Y * Helper.Sin(r)) + p.pos.X;
                float y2 = (-seg.end.X * Helper.Sin(r) + seg.end.Y * Helper.Cos(r)) + p.pos.Y;
                LineSegment transformed = new LineSegment(new Point((int)x1, (int)y1), new Point((int)x2, (int)y2));
                Point result = new Point();
                foreach(LineSegment wall in borders)
                    if (Helper.FindIntersectionPoint(transformed, wall, out result))
                    {
                        p.crashed = true;
                        if (drawPoints) Helper.DrawMarker(g, Color.Blue, result, dx, dy, sizeY, BBoxMin);
                        return;
                    }
            }
        }

        public void CheckRayCollisions(Graphics g, float dx, float dy, int sizeY, Player player)
        {
            for(int i=0;i<player.baseRays.Count;i++)
            {
                LineSegment baseRay = player.baseRays[i];
                float r = player.rot;
                float x1 = (baseRay.start.X * Helper.Cos(r) + baseRay.start.Y * Helper.Sin(r)) + player.pos.X;
                float y1 = (-baseRay.start.X * Helper.Sin(r) + baseRay.start.Y * Helper.Cos(r)) + player.pos.Y;
                float x2 = (baseRay.end.X * Helper.Cos(r) + baseRay.end.Y * Helper.Sin(r)) + player.pos.X;
                float y2 = (-baseRay.end.X * Helper.Sin(r) + baseRay.end.Y * Helper.Cos(r)) + player.pos.Y;
                List<Point> list = new List<Point>();
                Point result = new Point();
                foreach (LineSegment seg in borders)
                    if (Helper.FindRayCollisionPoint(x1, y1, x2 - x1, y2 - y1, seg.start.X, seg.start.Y, seg.end.X, seg.end.Y, out result))
                        list.Add(new Point(result.X, result.Y));
                if (list.Count == 0)
                {
                    player.baseRayDis[i] = player.sightRadius;
                    continue;
                }
                Point collision = list[0];
                float dis = Helper.Pow2(collision.X - x1) + Helper.Pow2(collision.Y - y1);
                foreach (Point p in list)
                {
                    float tmp = Helper.Pow2(p.X - x1) + Helper.Pow2(p.Y - y1);
                    if (tmp < dis)
                    {
                        dis = tmp;
                        collision = p;
                    }
                }
                if (drawPoints) Helper.DrawMarker(g, Color.DarkGreen, collision, dx, dy, sizeY, BBoxMin);
                player.baseRayDis[i] = (float)Math.Sqrt(dis);
                if (player.baseRayDis[i] > player.sightRadius)
                    player.baseRayDis[i] = player.sightRadius;
            }
        }
    }
}
