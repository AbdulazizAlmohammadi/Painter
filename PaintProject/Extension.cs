using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace PaintProject
{
    class Extension
    {
        private string fileName = "paint.p";

        public void WriteNewFileFromList(List<Shape> shapes)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(fileName, FileMode.Create)))
            {
                writer.Write(135);
                writer.Write(shapes.Count);
                foreach (var shape in shapes)
                {
                    if(shape is Rectangle)
                    {
                        writer.Write(0);
                    } else if (shape is Circle)
                    {
                        writer.Write(1);
                    } else if (shape is Line)
                    {
                        writer.Write(2);
                    }

                    writer.Write(shape.Color.ToArgb());
                    writer.Write(shape.Pen.Color.ToArgb());
                    writer.Write(shape.Pen.Width);
                    writer.Write(shape.Pen.DashStyle.ToString());
                    writer.Write(shape.start.X);
                    writer.Write(shape.start.Y);
                    writer.Write(shape.end.X);
                    writer.Write(shape.end.Y);
                }

            }
        }

        public List<Shape> DisplayFileFromList()
        {
            int MagicNumber;
            int length;
            List<Shape> shapes = new List<Shape> { } ;
            Shape shape ;
            int type;
            string dashStyle = "";
            if (File.Exists(fileName))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
                {
                    MagicNumber = reader.ReadInt32();
                    length = reader.ReadInt32();
                    for (int i = 0; i < length; i++)
                    {
                        type = reader.ReadInt32();
                        if(type == 0)
                        {
                            shape = new Rectangle();
                        } else if(type == 1)
                        {
                            shape = new Circle();
                        } else
                        {
                            shape = new Line();
                        }

                        
                        shape.Color = Color.FromArgb(reader.ReadInt32());
                        shape.Pen.Color = Color.FromArgb(reader.ReadInt32());
                        
                        shape.Pen.Width = reader.ReadInt32();
                        
                        dashStyle = reader.ReadString();
                        if (dashStyle == "Solid")
                        {
                            shape.Pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;

                        }
                        else if (dashStyle == "Dot")
                        {
                            shape.Pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

                        }
                        else if (dashStyle == "Dash")
                        {
                            shape.Pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

                        }
                        else if (dashStyle == "Dash dot")
                        {
                            shape.Pen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;

                        }
                        else if (dashStyle == "Dash dot dot")
                        {
                            shape.Pen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot;

                        }
                        shape.start = new Point(reader.ReadInt32(), reader.ReadInt32());
                       
                        shape.end = new Point(reader.ReadInt32(), reader.ReadInt32());

                        shapes.Add(shape);
                    }
                }


            }
            return shapes;
        }
    }
}
