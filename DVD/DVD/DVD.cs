using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVD
{
    internal class DVD
    {
        Plane first, second;
        public DVD() {
            Plane.InitShape();
            first = new Plane(new PointF(100, 100), 25, new PointF(100, 100));
            second = new Plane(new PointF(200, 220), 25, new PointF(-100, -100));
            
        }
        
        bool FixBorderCollisions(Plane p, Graphics g)
        {
            if (p.position.X + p.radius > g.VisibleClipBounds.Width)
            {
                p.Flip(true, g.VisibleClipBounds.Width);
                return true;
            }else if(p.position.X - p.radius < 0)
            {
                p.Flip(true, 0);
                return true;
            }else if(p.position.Y + p.radius > g.VisibleClipBounds.Height)
            {
                p.Flip(false, g.VisibleClipBounds.Height);
                return true;
            }else if(p.position.Y - p.radius < 0)
            {
                p.Flip(false, 0);
                return true;
            }

            return false;
        }
        bool FixPlaneCollisions()
        {
            if( first.position.X-first.radius<second.position.X+second.radius &&
                first.position.X+first.radius>second.position.X-second.radius &&
                first.position.Y - first.radius < second.position.Y + second.radius &&
                first.position.Y + first.radius > second.position.Y - second.radius)
            {
                float w = first.position.X - second.position.X;
                float h = first.position.Y - second.position.Y;
                if (Math.Abs(w) > Math.Abs(h))
                {
                    w = first.position.X - w / 2;
                    first.Flip(true, w);
                    second.Flip(true, w);
                }
                else
                {
                    h = first.position.Y - h / 2;
                    first.Flip(false, h);
                    second.Flip(false, h);
                }
                return true;
            }
            return false;
        }
        public void Draw(Graphics g,float seconds)
        {
            
            Move(seconds);
            bool collisions = true;
            while (collisions)
            {
                collisions = false;
                bool bordcol = FixBorderCollisions(first, g) || FixBorderCollisions(second,g);
                bool planecol = FixPlaneCollisions();
                collisions = bordcol || planecol;
            }
            g.Clear(Color.Black);
            g.ResetTransform();
            g.TranslateTransform(first.position.X, first.position.Y);
            g.DrawPolygon(first.pen, first.points);
            g.ResetTransform();
            g.TranslateTransform(second.position.X, second.position.Y);
            g.DrawPolygon(second.pen, second.points);
        }
        void Move(float seconds)
        {
            first.Move(seconds);
            second.Move(seconds);
        }
    }
}
