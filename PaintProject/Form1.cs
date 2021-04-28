using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PaintProject;
namespace PaintProject
{

    public partial class Form1 : Form
    {


        public Color c=Color.Black;
        public Shape paintSh = new Line();
        public int penSize = 0;
        public string dashStyle = "Solid";
        List<Shape> paints = new List<Shape> { };
        Shape selectedShape = null;
        int dx = 0;
        int dy = 0;
        bool isSelected = false;
        bool isMouseDown = false;
        string type;
        public Form1()
        {
            
            InitializeComponent();
        }


       
        private void button1_Click(object sender, EventArgs e)
        {
            this.paintSh = new Line();
            button3.BackColor = Color.WhiteSmoke;
            button2.BackColor = Color.WhiteSmoke;
            button1.BackColor = Color.LightGray;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.paintSh = new Rectangle();
            button3.BackColor = Color.WhiteSmoke;
            button2.BackColor = Color.LightGray;
            button1.BackColor = Color.WhiteSmoke;

        }

        private void button3_Click(object sender, EventArgs e)
        {

            this.paintSh = new Circle();
            button3.BackColor = Color.LightGray;
            button2.BackColor = Color.WhiteSmoke;
            button1.BackColor = Color.WhiteSmoke;

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {

           
            if (selectedShape == null && !isSelected)
            {
                paintSh.start = new Point(e.X, e.Y);
            }
            else if(isIntract(e))
            {
                isMouseDown = true;
                dx = e.Location.X - selectedShape.getX();
                dy = e.Location.Y - selectedShape.getY();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            if (selectedShape == null && !isSelected)
            {
                paintSh.end = new Point(e.X, e.Y);
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
                paintSh.Pen = p;
                paintSh.IsSelected = false;
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
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            foreach (var paint in paints)
            {

                if (paint is Rectangle)
                {


                    g.DrawRectangle(paint.Pen, paint.getX(), paint.getY(), paint.Width, paint.Hight);
                    if (paint.IsSelected)
                    {
                        Pen p = new Pen(Brushes.Blue, 2);
                        p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                        g.DrawRectangle(p, paint.getX(), paint.getY(), paint.Width, paint.Hight);
                       /* g.FillEllipse(new SolidBrush(Color.Black), (x + width / 2 - 5), (y + higth / 2 - 5), 10, 10); // middle
                        g.FillEllipse(new SolidBrush(Color.Blue), (x + width / 2 - 5), (y - 5), 10, 10); // top 
                        g.FillEllipse(new SolidBrush(Color.Blue), (x - 5), (y + higth / 2 - 5), 10, 10);//left
                        g.FillEllipse(new SolidBrush(Color.Blue), (x + width - 5), (y + higth / 2 - 5), 10, 10); // right
                        g.FillEllipse(new SolidBrush(Color.Blue), (x + width / 2 - 5), (y - 5 + higth), 10, 10); // bottom*/
                    }
                }
                else if (paint is Circle)
                {


                    g.DrawEllipse(paint.Pen, paint.getX(), paint.getY(), paint.Width, paint.Hight);
                    //g.DrawEllipse(paint.Pen, paint.getX(), paint.getY(), 4, 4);

                }
                else if (paint is Line)
                {
                    g.DrawLine(paint.Pen, paint.start.X, paint.start.Y, paint.end.X, paint.end.Y);
                }
            }
        }
        public void checkIntract(MouseEventArgs mouse)
        {
            System.Drawing.Rectangle rect1;
            System.Drawing.Rectangle rect2 = new System.Drawing.Rectangle(mouse.X, mouse.Y, 10, 10);
            foreach (var paint in paints)
            {
                rect1 = new System.Drawing.Rectangle(paint.getX(), paint.getY(), paint.Width, paint.Hight);

                if (rect1.IntersectsWith(rect2))
                {
                    this.selectedShape = paint;
                    paint.IsSelected = true;
                    isSelected = true;
                    this.Invalidate();
                    return;
                }
            }
            selectedShape = null;
            isSelected = false;
            

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
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            this.penSize = (int)this.numericUpDown1.Value;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.dashStyle = this.comboBox1.SelectedItem.ToString();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            paints = new List<Shape> { };
            this.Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.BackColor = Color.LightGray;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Extension read = new Extension();
             paints = read.DisplayFileFromList();
            this.Invalidate();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Extension save = new Extension();
            if (paints.Count > 0)
            {
                save.WriteNewFileFromList(paints);
                MessageBox.Show("تم الحفظ بنجاح");
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!isSelected)
            {
                foreach (var paint in paints)
                {
                    paint.IsSelected = false;
                }
                checkIntract(e);
            }
            else if(!isIntract(e))
            {
                 foreach (var paint in paints)
                {
                    paint.IsSelected = false;
                }
                isSelected = false;
                this.Invalidate();
            }
            
            
        }

        public bool isIntract(MouseEventArgs mouse)
        {
            var rect1 = new System.Drawing.Rectangle(this.selectedShape.getX(), this.selectedShape.getY(), this.selectedShape.Width, this.Height);
            var rect2 = new System.Drawing.Rectangle(mouse.X, mouse.Y, 10, 10);
            return rect1.IntersectsWith(rect2);

        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isSelected && isMouseDown)
            {
                selectedShape.start.Offset(e.Location.X- dx, e.Location.Y -dy);
                this.Invalidate();
            }
        }
    }
}
