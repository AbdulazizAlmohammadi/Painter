using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PaintProject
{
    public class Canvse : Form
    {
        public Color c;
        public string dashStyle;
        public int deltaX;
        public int deltaY;
        public bool IsDraowing;
        public bool IsEditing;
        public bool IsMovieng;
        public bool isResizeing;
        public Shape paintSh;
        public int penSize;
        public string resize;
        public int selectedHeight;
        public Shape selectedShape;
        public int selectedWidth;
        List<Shape> paints;
        string type;


        public Canvse()
        {
            this.c = Color.Black;
            this.paintSh = new Line();
            this.penSize = 0;
            this.dashStyle = "Solid";

        this.paints = new List<Shape> { };

            this.IsEditing = false;
            this.IsDraowing = true;
            this.IsMovieng = false;
            this.isResizeing = false;
            this.resize = "";

            this.selectedShape = null;
            this.deltaX = 0;
            this.deltaY = 0;

        }


        public void Draw(Graphics g)
        {
           
            foreach (var paint in paints)
            {
                if (paint is Rectangle)
            {
                g.DrawRectangle(paint.Pen, paint.getX(), paint.getY(), paint.getWidth(), paint.getHight());
            }
            else if (paint is Circle)
            {
                g.DrawEllipse(paint.Pen, paint.getX(), paint.getY(), paint.getWidth(), paint.getHight());
            }
            else if (paint is Line)
            {
                g.DrawLine(paint.Pen, paint.startX, paint.startY, paint.endX, paint.endY);
            }
        }

            if (selectedShape != null)
            {
                Pen p = new Pen(Color.Black, 2);
        p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                g.DrawRectangle(p, selectedShape.getX() - 10, selectedShape.getY() - 10, selectedShape.getWidth() + 20, selectedShape.getHight() + 20);
                g.FillEllipse(Brushes.Gray, (selectedShape.getX() + selectedShape.getWidth()/2) - 5,selectedShape.getY() - 15, 10, 10); //top
                g.FillEllipse(Brushes.Gray, selectedShape.getX() - 15, (selectedShape.getY() + selectedShape.getHight()/2) - 5, 10, 10); //left
                g.FillEllipse(Brushes.Gray, selectedShape.getX() + selectedShape.getWidth() + 5, (selectedShape.getY() + selectedShape.getHight()/2) - 5, 10, 10); //right
                g.FillEllipse(Brushes.Gray, (selectedShape.getX() + selectedShape.getWidth()/2) - 5,selectedShape.getY() + selectedShape.getHight() + 5, 10, 10); //bottom
            }
}
    
        public void LineButton()
        {
            this.paintSh = new Line();
            IsEditing = false;
            IsMovieng = false;
            selectedShape = null;
            IsDraowing = true;
        }
        public void RecButton()
        {
            this.paintSh = new Rectangle();
            IsEditing = false;
            IsMovieng = false;
            selectedShape = null;
            IsDraowing = true;
        }
        public void CircButton()
        {
            this.paintSh = new Circle();
            IsEditing = false;
            IsMovieng = false;
            selectedShape = null;
            IsDraowing = true;
        }


        public void MClick(MouseEventArgs e)
        {
            if (IsEditing)
            {

                Point Curser = new Point(e.X, e.Y);
                selectedShape = null;
                foreach (var shape in paints)
                {
                    if (shape.Path.GetBounds().Contains(e.X, e.Y))
                    {
                        selectedShape = shape;
                        selectedWidth = shape.getWidth();
                        selectedHeight = shape.getHight();
                        IsMovieng = true;
                    }
                }
                
            }
        }

        public void MDown(MouseEventArgs e)
        {
            if (IsDraowing)
            {
                //paintSh.start = new Point(e.X, e.Y);
                paintSh.startX = e.X;
                paintSh.startY = e.Y;
            }

            if (selectedShape != null)
            {
                IsMovieng = true;
                deltaX = Math.Abs(selectedShape.startX - e.X);
                deltaY = Math.Abs(selectedShape.startY - e.Y);

                if (selectedShape.Path.GetBounds().Contains(e.X + 15, e.Y + 15) || selectedShape.Path.GetBounds().Contains(e.X - 15, e.Y - 15))
                {
                    System.Drawing.Rectangle mouse = new System.Drawing.Rectangle(e.X - 10, e.Y - 10, 20, 20);
                    System.Drawing.Rectangle top = new System.Drawing.Rectangle((selectedShape.getX() + selectedShape.getWidth() / 2) - 5, selectedShape.getY() - 15, 10, 10);
                    System.Drawing.Rectangle left = new System.Drawing.Rectangle(selectedShape.getX() - 15, (selectedShape.getY() + selectedShape.getHight() / 2) - 5, 10, 10);
                    System.Drawing.Rectangle right = new System.Drawing.Rectangle(selectedShape.getX() + selectedShape.getWidth() + 5, (selectedShape.getY() + selectedShape.getHight() / 2) - 5, 10, 10);
                    System.Drawing.Rectangle bottom = new System.Drawing.Rectangle((selectedShape.getX() + selectedShape.getWidth() / 2) - 5, selectedShape.getY() + selectedShape.getHight() + 5, 10, 10);

                    if (mouse.IntersectsWith(top))
                    {
                        isResizeing = true;
                        IsMovieng = false;
                        resize = "top";
                    }
                    else if (mouse.IntersectsWith(left))
                    {
                        isResizeing = true;
                        IsMovieng = false;
                        resize = "left";
                    }
                    else if (mouse.IntersectsWith(right))
                    {
                        isResizeing = true;
                        IsMovieng = false;
                        resize = "right";
                    }
                    else if (mouse.IntersectsWith(bottom))
                    {
                        isResizeing = true;
                        IsMovieng = false;
                        resize = "bottom";
                    }
                }
            }
        }

        public void MUp(MouseEventArgs e)
        {
            if (IsDraowing)
            {
                //shapePath = new GraphicsPath(); //edit

                //paintSh.end = new Point(e.X, e.Y);
                paintSh.endX = e.X;
                paintSh.endY = e.Y;

                Pen p = new Pen(this.c, penSize);
                if (dashStyle == "Solid")
                {
                    p.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;

                }
                else if (dashStyle == "Dot")
                {
                    p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

                }
                else if (dashStyle == "Dash")
                {
                    p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

                }
                else if (dashStyle == "Dash dot")
                {
                    p.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;

                }
                else if (dashStyle == "Dash dot dot")
                {
                    p.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot;

                }



                if (paintSh is Rectangle)
                {
                    paintSh.Path = new GraphicsPath();
                    System.Drawing.Rectangle rect = new System.Drawing.Rectangle(paintSh.getX(), paintSh.getY(),
                        paintSh.getWidth(), paintSh.getHight());
                    paintSh.Path.AddRectangle(rect);
                }
                else if (paintSh is Circle)
                {
                    paintSh.Path = new GraphicsPath();
                    System.Drawing.Rectangle cir = new System.Drawing.Rectangle(paintSh.getX(), paintSh.getY(),
                        paintSh.getWidth(), paintSh.getHight());
                    paintSh.Path.AddRectangle(cir);
                }
                else if (paintSh is Line)
                {
                    paintSh.Path = new GraphicsPath();

                    Point start = new Point(paintSh.startX, paintSh.startY);
                    Point end = new Point(paintSh.endX, paintSh.endY);

                    //paintSh.Path.AddLine(paintSh.start, paintSh.end);
                    paintSh.Path.AddLine(start, end);
                }

                paintSh.Pen = p;
                paints.Add(paintSh);

                this.Invalidate();

                if (paintSh is Rectangle)
                {


                    paintSh = new Rectangle();

                }
                else if (paintSh is Circle)
                {


                    paintSh = new Circle();


                }
                else if (paintSh is Line)
                {
                    paintSh = new Line();

                }
            }

            if (selectedShape != null)
            {

            }

            IsMovieng = false;
            isResizeing = false;
        }

        public void MMove(MouseEventArgs e)
        {
            if (IsMovieng && selectedShape != null)
            {
                int AfterMoveX = selectedShape.getX();
                int AfterMoveY = selectedShape.getY();

                if (selectedShape.endX > selectedShape.startX)
                {
                    selectedShape.startX = e.X - deltaX;
                    selectedShape.endX = e.X + selectedWidth - deltaX;
                    AfterMoveX = selectedShape.startX;
                    this.Invalidate();
                }
                else
                {
                    selectedShape.endX = e.X - deltaX;
                    selectedShape.startX = e.X + selectedWidth - deltaX;
                    AfterMoveX = selectedShape.endX;
                    this.Invalidate();
                }
                if (selectedShape.endY > selectedShape.startY)
                {
                    selectedShape.startY = e.Y - deltaY;
                    selectedShape.endY = e.Y + selectedHeight - deltaY;
                    AfterMoveY = selectedShape.startY;
                    this.Invalidate();
                }
                else
                {
                    selectedShape.endY = e.Y - deltaY;
                    selectedShape.startY = e.Y + selectedHeight - deltaY;
                    AfterMoveY = selectedShape.endY;
                    this.Invalidate();
                }





                //selctedShape.Path.Reset();
                if (selectedShape is Rectangle || selectedShape is Circle)
                {
                    selectedShape.Path = new GraphicsPath();
                    System.Drawing.Rectangle rect = new System.Drawing.Rectangle(AfterMoveX, AfterMoveY,
                        selectedWidth, selectedHeight);
                    selectedShape.Path.AddRectangle(rect);

                }
                else if (selectedShape is Line)
                {
                    selectedShape.Path = new GraphicsPath();
                    selectedShape.Path.AddLine(selectedShape.startX, selectedShape.startY, selectedShape.endX, selectedShape.endY);
                    
                }


                this.Invalidate();
            }


            if (isResizeing && selectedShape != null)
            {
                int AfterResizeX = selectedShape.getX();
                int AfterResizeY = selectedShape.getY();
                if (resize == "top")
                {
                    if (selectedShape.startY > selectedShape.endY)
                    {
                        selectedShape.endY = e.Y + 10;
                        selectedHeight = selectedShape.startY - selectedShape.endY;
                        AfterResizeY = selectedShape.endY;
                        if (selectedShape.endY > selectedShape.startY)
                        {
                            this.resize = "bottom";
                        }
                    }
                    else
                    {
                        selectedShape.startY = e.Y + 10;
                        selectedHeight = selectedShape.endY - selectedShape.startY;
                        AfterResizeY = selectedShape.startY;
                        if (selectedShape.startY > selectedShape.endY)
                        {
                            //AfterResizeY = selectedShape.endY;
                            this.resize = "bottom";
                        }
                    }


                }
                else if (resize == "bottom")
                {

                    if (selectedShape.startY < selectedShape.endY)
                    {
                        selectedShape.endY = e.Y - 10;
                        selectedHeight = selectedShape.endY - selectedShape.startY;
                        AfterResizeY = selectedShape.startY;
                        if (selectedShape.endY < selectedShape.startY)
                        {
                            this.resize = "top";
                        }
                    }
                    else
                    {
                        selectedShape.startY = e.Y - 10;
                        selectedHeight = selectedShape.startY - selectedShape.endY;
                        AfterResizeY = selectedShape.endY;
                        if (selectedShape.startY < selectedShape.endY)
                        {
                            //MessageBox.Show("Test");
                            this.resize = "top";
                        }
                    }
                }
                if (resize == "left")
                {
                    if (selectedShape.startX > selectedShape.endX)
                    {
                        selectedShape.endX = e.X + 10;
                        selectedWidth = selectedShape.startX - selectedShape.endX;
                        AfterResizeX = selectedShape.endX;
                        if (selectedShape.endX > selectedShape.startX)
                        {
                            this.resize = "right";
                        }
                    }
                    else
                    {
                        selectedShape.startX = e.X + 10;
                        selectedWidth = selectedShape.endX - selectedShape.startX;
                        AfterResizeX = selectedShape.startX;
                        if (selectedShape.startX > selectedShape.endX)
                        {
                            this.resize = "right";
                        }
                    }
                }
                else if (resize == "right")
                {

                    if (selectedShape.startX < selectedShape.endX)
                    {
                        selectedShape.endX = e.X - 10;
                        selectedWidth = selectedShape.endX - selectedShape.startX;
                        AfterResizeX = selectedShape.startX;
                        if (selectedShape.endX < selectedShape.startX)
                        {
                            this.resize = "left";
                        }
                    }
                    else
                    {
                        selectedShape.startX = e.X - 10;
                        selectedWidth = selectedShape.startX - selectedShape.endX;
                        AfterResizeX = selectedShape.endX;
                        if (selectedShape.startX < selectedShape.endX)
                        {
                            //MessageBox.Show("Test");
                            this.resize = "left";
                        }
                    }
                }
                if (selectedShape is Rectangle || selectedShape is Circle)
                {
                    selectedShape.Path = new GraphicsPath();
                    System.Drawing.Rectangle rect = new System.Drawing.Rectangle(AfterResizeX, AfterResizeY,
                        selectedWidth, selectedHeight);
                    selectedShape.Path.AddRectangle(rect);
                }
                else if (selectedShape is Line)
                {
                    selectedShape.Path = new GraphicsPath();
                    selectedShape.Path.AddLine(selectedShape.startX, selectedShape.startY, selectedShape.endX, selectedShape.endY);
                }

                this.Invalidate();
            }
        }

        public void ColorPicker() { 
        }

        public void EnableEditing()
        {
            IsEditing = true;
            IsDraowing = false;
            
        }

        public void MovingShape(MouseEventArgs e)
        {
            if (IsMovieng && selectedShape != null)
            {
                int AfterMoveX = selectedShape.getX();
                int AfterMoveY = selectedShape.getY();

                if (selectedShape.endX > selectedShape.startX)
                {
                    selectedShape.startX = e.X - deltaX;
                    selectedShape.endX = e.X + selectedWidth - deltaX;
                    AfterMoveX = selectedShape.startX;
                    this.Invalidate();
                }
                else
                {
                    selectedShape.endX = e.X - deltaX;
                    selectedShape.startX = e.X + selectedWidth - deltaX;
                    AfterMoveX = selectedShape.endX;
                    this.Invalidate();
                }
                if (selectedShape.endY > selectedShape.startY)
                {
                    selectedShape.startY = e.Y - deltaY;
                    selectedShape.endY = e.Y + selectedHeight - deltaY;
                    AfterMoveY = selectedShape.startY;
                    this.Invalidate();
                }
                else
                {
                    selectedShape.endY = e.Y - deltaY;
                    selectedShape.startY = e.Y + selectedHeight - deltaY;
                    AfterMoveY = selectedShape.endY;
                    this.Invalidate();
                }
            }
        

        }
}
    }
