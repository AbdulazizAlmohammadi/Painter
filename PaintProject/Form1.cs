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
            
            paintSh.start = new Point(e.X, e.Y);
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            paintSh.end = new Point(e.X, e.Y);
            Pen p = new Pen(this.c, penSize);
            if(dashStyle == "Solid")
            {
                p.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;

            }else if (dashStyle == "Dot")
            {
                p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

            } else if (dashStyle == "Dash")
            {
                p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            }
            else if(dashStyle == "Dash dot")
            {
                p.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;

            } else if (dashStyle == "Dash dot dot")
            {
                p.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot;

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

        private void Form1_Paint(object sender, PaintEventArgs e)
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
                    g.DrawLine(paint.Pen, paint.start.X, paint.start.Y, paint.end.X, paint.end.Y);
                }
            }
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
    }
}
