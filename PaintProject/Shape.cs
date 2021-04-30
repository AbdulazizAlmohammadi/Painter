using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace PaintProject
{
    public abstract class Shape
    {
        public int startX;
        public int startY;
        public int endX;
        public int endY;
        
        public Color Color { get; set; }
        public Pen Pen { get; set; }

        public GraphicsPath Path;

        public int getWidth()
        {
            return Math.Abs(endX - startX);
        }
        public int getHight()
        {
            return Math.Abs(endY - startY);
        }
        public int getX()
        {
            if (startX > endX)
            {
                return endX;
            }
            return startX;
        }
        public int getY()
        {
            if (startY > endY)
            {
                return endY;
            }
            return startY;
        }
    }

    public class Rectangle : Shape{}
    public class Circle : Shape{}
    public class Line : Shape{}
}
