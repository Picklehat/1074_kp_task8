using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DVD
{
    internal class Plane
    {
        public static PointF[] gpoints { get; private set; }
        static Random rnd = new Random();
        public static void InitShape()
        {
            gpoints = new PointF[13];
            gpoints[0] = new PointF(20, 0);
            gpoints[1] = new PointF(10, 5);
            gpoints[12] = new PointF(10, -5);
            gpoints[2] = new PointF(1, 23);
            gpoints[11] = new PointF(1, -23);
            gpoints[3] = new PointF(-7, 25);
            gpoints[10] = new PointF(-7, -25);
            gpoints[4] = new PointF(-3, 5);
            gpoints[9] = new PointF(-3, -5);
            gpoints[5] = new PointF(-23, 3);
            gpoints[8] = new PointF(-23, -3);
            gpoints[6] = new PointF(-27, 7);
            gpoints[7] = new PointF(-27, -7);
            float s = (float)Math.Sin(Math.PI / 4.0f);
            float c = (float)Math.Cos(Math.PI / 4.0f);
            for (int i = 0; i < 13; i++)
            {
                gpoints[i] = new PointF(gpoints[i].X * c - gpoints[i].Y * s, gpoints[i].X * s + gpoints[i].Y * c);
            }
        }
        public Plane(PointF pos, float rad,PointF spd)
        {
            position = pos;
            radius = rad;
            speed = spd;
            points = new PointF[13];
            for(int i = 0; i < 13; i++)
            {
                points[i] = new PointF(gpoints[i].X, gpoints[i].Y);
            }
            if(speed.X < 0)
            {
                FlipPoints(true);
            }
            if(speed.Y < 0)
            {
                FlipPoints(false);
            }
            ChangePen();
        }
        public PointF[] points { get; private set; }
        public Pen pen { get; private set; }
        public PointF position { get; private set; }
        public float radius { get; private set; }
        public PointF speed { get; private set; }
        public void Move(float seconds)
        {
            position  = new PointF(position.X+speed.X*seconds,position.Y+speed.Y*seconds);
        }
        void ChangePen()
        {
            Color clr = Color.FromArgb((byte)rnd.Next(80, 256), (byte)rnd.Next(80, 256), (byte)rnd.Next(80, 256));
            pen = new Pen(new SolidBrush(clr));
        }
        public void Flip(bool horizontal,float coord)
        {
            if(horizontal)
            {
                if (speed.X < 0)
                {
                    position = new PointF(coord + (coord - (position.X - radius)) + radius,position.Y);
                }
                else
                {
                    position = new PointF(coord-(position.X + radius-coord)-radius,position.Y);
                }
                speed = new PointF(-speed.X, speed.Y);
            }
            else
            {
                if (speed.Y < 0)
                {
                    position = new PointF(position.X, coord + (coord - (position.Y - radius)) + radius);
                }
                else
                {
                    position = new PointF(position.X,coord - (position.Y + radius - coord) - radius);
                }
                speed = new PointF(speed.X, -speed.Y);
                
            }
            FlipPoints(horizontal);
            ChangePen();
        }
        public void FlipPoints(bool horizontal)
        {
            for(int i = 0; i<13; i++)
            {
                if (horizontal)
                {
                    points[i] = new PointF(-points[i].X, points[i].Y);
                }
                else
                {
                    points[i] = new PointF(points[i].X, -points[i].Y);
                }
            }
        }
    }
}
