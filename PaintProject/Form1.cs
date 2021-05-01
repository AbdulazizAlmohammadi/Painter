﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using PaintProject;
namespace PaintProject
{

    public partial class Form1 : Form
    {
        public Canvse canvse = new Canvse();
        
        public Color c=Color.Black;
        public Shape paintSh = new Line();
        public int penSize = 0;
        public string dashStyle = "Solid";
        
        public static List<Shape> paints = new List<Shape> { };
        public static List<Source> source = new List<Source> { };
        string type;
        public bool IsEditing = false; 
        public bool IsDraowing = true;
        public bool IsMovieng = false;
        public bool isResizeing = false;
        public string resize = "";

        public Shape selectedShape = null;
        public int selectedWidth;
        public int selectedHeight;

        public int deltaX = 0;
        public int deltaY = 0;
        public Form1()
        {
            
            InitializeComponent();
        }
        
        



        private void Form1_Load(object sender, EventArgs e)
        {
            LineButton.BackColor = Color.LightGray;
        }
        private void LineButton_Click(object sender, EventArgs e)
        {
            this.paintSh = new Line();
            IsEditing = false;
            IsMovieng = false;
            selectedShape = null;
            IsDraowing = true; 
            button3.BackColor = Color.WhiteSmoke;
            button2.BackColor = Color.WhiteSmoke;
            LineButton.BackColor = Color.LightGray;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.paintSh = new Rectangle();
            IsEditing = false;
            IsMovieng = false;
            selectedShape = null;
            IsDraowing = true; 
            button3.BackColor = Color.WhiteSmoke;
            button2.BackColor = Color.LightGray;
            LineButton.BackColor = Color.WhiteSmoke;

        }

        private void button3_Click(object sender, EventArgs e)
        {

            this.paintSh = new Circle();
            IsEditing = false;
            IsMovieng = false;
            selectedShape = null;
            IsDraowing = true; 
            button3.BackColor = Color.LightGray;
            button2.BackColor = Color.WhiteSmoke;
            LineButton.BackColor = Color.WhiteSmoke;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            MyDialog.AllowFullOpen = true;
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                
                this.c = MyDialog.Color;
                this.button4.BackColor = c;
                paintSh.Color = c;
                if (selectedShape != null && IsEditing)
                {
                    selectedShape.Pen.Color = c;
                    this.tabPage1.Invalidate();
                    ToSource(paints);
                }
                
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            this.penSize = (int)this.numericUpDown1.Value;
            if (selectedShape != null)
            {
                selectedShape.Pen.Width = (int) this.numericUpDown1.Value;
                ToSource(paints);
                this.tabPage1.Invalidate();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.dashStyle = this.comboBox1.SelectedItem.ToString();
            if (selectedShape != null)
            {
                if(dashStyle == "Solid")
                {
                    selectedShape.Pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;

                }else if (dashStyle == "Dot")
                {
                    selectedShape.Pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

                } else if (dashStyle == "Dash")
                {
                    selectedShape.Pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                }
                else if(dashStyle == "Dash dot")
                {
                    selectedShape.Pen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;

                } else if (dashStyle == "Dash dot dot")
                {
                    selectedShape.Pen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot;
                }
                ToSource(paints);
                this.tabPage1.Invalidate();
            }
        }
        
        private void button5_Click(object sender, EventArgs e)
        {
            paints = new List<Shape> { }; // edit
            ToSource(paints);
            selectedShape = null;
            this.tabPage1.Invalidate();
        }


        private void button6_Click(object sender, EventArgs e)
        {
            IsEditing = true;
            IsDraowing = false; 
        }

        private void tabPage1_Paint(object sender, PaintEventArgs e)
        {

            Graphics g = e.Graphics;

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
                g.FillEllipse(Brushes.Gray, (selectedShape.getX() + selectedShape.getWidth() / 2) - 5, selectedShape.getY() - 15, 10, 10); //top
                g.FillEllipse(Brushes.Gray, selectedShape.getX() - 15, (selectedShape.getY() + selectedShape.getHight() / 2) - 5, 10, 10); //left
                g.FillEllipse(Brushes.Gray, selectedShape.getX() + selectedShape.getWidth() + 5, (selectedShape.getY() + selectedShape.getHight() / 2) - 5, 10, 10); //right
                g.FillEllipse(Brushes.Gray, (selectedShape.getX() + selectedShape.getWidth() / 2) - 5, selectedShape.getY() + selectedShape.getHight() + 5, 10, 10); //bottom
            }
        }

        private void tabPage1_MouseClick(object sender, MouseEventArgs e)
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
              
                this.tabPage1.Invalidate();
            }
        }

        private void tabPage1_MouseDown(object sender, MouseEventArgs e)
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

        private void tabPage1_MouseUp(object sender, MouseEventArgs e)
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
                this.tabPage1.Invalidate();

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
            ToSource(paints);

        }
        private void tabPage1_MouseMove(object sender, MouseEventArgs e)
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
                }
                else
                {
                    selectedShape.endX = e.X - deltaX;
                    selectedShape.startX = e.X + selectedWidth - deltaX;
                    AfterMoveX = selectedShape.endX;
                }
                if (selectedShape.endY > selectedShape.startY)
                {
                    selectedShape.startY = e.Y - deltaY;
                    selectedShape.endY = e.Y + selectedHeight - deltaY;
                    AfterMoveY = selectedShape.startY;
                }
                else
                {
                    selectedShape.endY = e.Y - deltaY;
                    selectedShape.startY = e.Y + selectedHeight - deltaY;
                    AfterMoveY = selectedShape.endY;
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


               
                this.tabPage1.Invalidate();
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

                this.tabPage1.Invalidate();
            }
        }

        public void ToSource(List<Shape> shapes)
        {
            source= new List<Source> { };
            Source s;
            foreach (var shape in shapes)
            {
                
                s = new Source();
                if (shape is Line)
                {
                    s.name = "line";
                }
                else if (shape is Circle)
                {
                    s.name = "circ";

                }
                else if (shape is Rectangle)
                {
                    s.name = "rect";

                }
                s.x1 = shape.startX;
                s.y1 = shape.startY;
                s.x2 = shape.endX;
                s.y2 = shape.endY;
                s.R = shape.Pen.Color.R;
                s.G = shape.Pen.Color.G;
                s.B = shape.Pen.Color.B;
                s.penSize = (int)shape.Pen.Width;
                s.dashStyle = shape.Pen.DashStyle.ToString();
                source.Add(s);

            }
            

            
            String sourcCode = "";

            foreach (var sc in source)
            {
                sourcCode += $"{sc.name} , {sc.x1} , {sc.y1} , {sc.x2} , {sc.y2}  , {sc.R} ,{sc.G} , {sc.B} , {sc.penSize} , {sc.dashStyle} "+Environment.NewLine;
            }
            this.textBox1.Text = sourcCode;
        }


    }
}
