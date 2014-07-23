using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ev3_test_server
{
    public partial class View : Form
    {

        public int angle = 0;
        public System.Drawing.Pen myPen;
        public System.Drawing.Pen myWhitePen;
        public System.Drawing.Graphics formGraphics;
        public int x = 275;
        public int y = 275;
        public int step = 20;
        


        public View()
        {
            InitializeComponent();
        }

        public void init()
        {
            this.myPen = new System.Drawing.Pen(System.Drawing.Color.Black, 3);
            this.formGraphics = pictureBox1.CreateGraphics();
        }

        public void start()
        {
            System.Drawing.Pen pen2 = new System.Drawing.Pen(System.Drawing.Color.Red, 3);
            formGraphics.DrawLine(pen2, x - 1, y - 1, x + 1, y + 1);
            pen2.Dispose();
        }

        public void rotateLeft()
        {
            angle = angle + 90;
            angle = angle % 360;
        }

        public void rotateRight()
        {
            angle = angle - 90;
            if (angle == -90)
            {
                angle = 270;
            }
        }

        public void forward()
        {
            lineDrawing(-step);
        }

        public void attack()
        {
            lineDrawing(step*2);
        }

        public void backward()
        {
            lineDrawing(step);
        }

        public void slow_forward()
        {
            lineDrawing(-step/2);
        }

        public void slow_backward()
        {
            lineDrawing(step/2);
        }

        public void lineDrawing(int this_step)
        {
            if (angle == 0)
            {
                formGraphics.DrawLine(myPen, x, y, x, y + this_step);
                y = y + this_step;
             
            }
            else if (angle == 90)
            {
                formGraphics.DrawLine(myPen, x, y, x + this_step, y);
                x = x + this_step;

            }
            else if (angle == 180)
            {
                formGraphics.DrawLine(myPen, x, y, x, y - this_step);
                y = y - this_step;
 
            }
            else if (angle == 270)
            {
                formGraphics.DrawLine(myPen, x, y, x - this_step, y);
                x = x - this_step;

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            start();
        }

    }
}
