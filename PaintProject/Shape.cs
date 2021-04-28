using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PaintProject
{
    public abstract class Shape
    {
        public Point start { get; set; }
        public Point end { get; set; }
        public Color Color { get; set; }
        public Pen Pen { get; set; }
        public int Hight { get { return Math.Abs(end.Y - start.Y); } set { } } 
        public int Width { get { return Math.Abs(end.X - start.X); } set { } } 
        public bool IsSelected { get; set; }
        public bool Middle { get; set; }
        public bool Right { get; set; }
        public bool Top { get; set; }
        public bool Bottom { get; set; }
        public bool Left { get; set; }

        //public abstract void draw(Graphics g);
        public int getWidth()
        {
            return Math.Abs(end.X - start.X);
        }
        public int getHight()
        {
            return Math.Abs(end.Y - start.Y);
        }
        public int getX()
        {
            if (start.X > end.X)
            {
                return end.X;
            }
            return start.X;
        }
        public int getY()
        {
            if (start.Y > end.Y)
            {
                return end.Y;
            }
            return start.Y;
        }
    }

    public class Rectangle : Shape
    {

    }
    public class Circle : Shape
    {

    }
    public class Line : Shape
    {

    }
}
