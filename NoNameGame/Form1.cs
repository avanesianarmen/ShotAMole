using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;

using System.Windows.Forms;


namespace NoNameGame
{
    public partial class Form1 : Form
    {
        Mole mole = new Mole();
        Player player = new Player();

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Controls.Remove(panel2);
            fillTable();

        }


        Boolean shot = false;
        Boolean gameOn = false;
        int scorecount = 0;
        int escapedcount  = 0;
        int i = 0;

        List<Point> list = new List<Point>();


        private void Form1_Load(object sender, EventArgs e)
        {
            fillPoints();
            this.DoubleBuffered = true;
            
            
        }



        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;


            if (gameOn)
            {


                if (mole.hit == true)
                {
                    Random r = new Random();
                    int tmp = r.Next(list.Count);

                    while (i == tmp)
                    {
                        tmp = r.Next(list.Count);
                    }

                    i = tmp;

                    mole.x = list[i].X;
                    mole.y = list[i].Y;
                    mole.rect = new Rectangle(mole.x + 30, mole.y + 30, 40, 50);
                    
                    label6.Text = (escapedcount - scorecount).ToString();
                    escapedcount++;
                    

                    mole.hit = false;
                    shot = true;

                }

                if (escapedcount > 20)
                {
                    escapedcount--;
                    label6.Text = (escapedcount-scorecount).ToString();
                    gameOver();
                    return;
                }
                g.DrawImage(mole.mole, mole.x, mole.y);

                if (player.flag == true)
                {
                    g.DrawImage(player.blood, player.bx - 40, player.by - 30);


                }

                g.DrawImage(player.aim, player.mx - 30, player.my - 30);



            }
            base.OnPaint(e);

        }


        private void mole_timer_Tick(object sender, EventArgs e)
        {

            
            mole.hit = true;
            
            this.Refresh();
            
            
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {

            if (mole.rect.Contains(e.X, e.Y) && shot == true)
            {
                
                player.bx = mole.x;
                player.by = mole.y;
                player.flag = true;
                scorecount++;
                
                label1.Text = scorecount.ToString();
                blood_timer.Start();
                this.Refresh();
                shot = false;
                
                
            }


        }



        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            //label1.Text = e.X.ToString();
            
           
                player.mx = e.X;
                player.my = e.Y;
            
            this.Refresh();
            
        }

        private void blood_timer_Tick(object sender, EventArgs e)
        {

            player.flag = false;
            blood_timer.Stop();
            this.Refresh();
            

            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            


            panel1.Controls.Remove(button1);
            panel1.Controls.Remove(button2);
            panel1.Controls.Remove(button3);

            

            panel1.Controls.Add(button4);
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(label2);
            label2.BringToFront();
            textBox1.BringToFront();
            button4.BringToFront();
            this.Refresh();

        }

        private void button3_Click(object sender, EventArgs e)
        {
           
            Application.Exit();
            

        }

        void fillPoints()
        {
            int n = 0;
            int m = 190;
            for (int i = 0; i < 3; i++)
            {
                m = m + n;
                list.Add(new Point(m, 150));
                list.Add(new Point(m, 330));
                n = 240;
            }
            n = 0;
            m = 70;
            for (int i = 0; i < 4; i++)
            {
                m = m + n;
                list.Add(new Point(m, 240));
                list.Add(new Point(m, 440));
                n = 240;

            }
        }

        void fillTable()
        {
            using (StreamReader file = new StreamReader("rec.txt"))
            {
                while (!file.EndOfStream)
                {
                    String line = file.ReadLine();
                    String[] mas = line.Split(' ');
                    //Console.WriteLine(mas.Length);
                   // Console.ReadKey();
                    dataGridView2.Rows.Add(mas);
                   
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Controls.Remove(pictureBox1);
            panel1.Controls.Remove(pictureBox2);
            panel1.Controls.Remove(button1);
            panel1.Controls.Remove(button2);
            panel1.Controls.Remove(button3);

            panel1.Controls.Add(dataGridView2);
            panel1.Controls.Add(button5);
            button5.BringToFront();
            dataGridView2.BringToFront();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {

            scorecount = 0;
            escapedcount = 0;

            mole.hit = true;

            if (textBox1.Text != null && textBox1.Text != "")
                player.name = textBox1.Text;

            level1();
            gameOn = true;
            mole_timer.Start();


            this.Controls.Remove(panel1);

            this.Refresh();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(pictureBox2);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button3);

            button1.BringToFront();
            button2.BringToFront();
            button3.BringToFront();

            panel1.Controls.Remove(dataGridView2);
            panel1.Controls.Remove(button5);


        }


        private void label3_Click(object sender, EventArgs e)
        {
            label6.Text = "0";
            label1.Text = "0";

            gameOn = false;
            mole_timer.Stop();
            this.Controls.Remove(panel2);

            panel1.Controls.Add(button1);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button3);
            button1.BringToFront();
            button2.BringToFront();
            button3.BringToFront();

            panel1.Controls.Remove(button4);
            panel1.Controls.Remove(textBox1);
            panel1.Controls.Remove(label2);

            this.Controls.Add(panel1);
            panel1.BringToFront();
        }


        



        void gameOver()
        {
            gameOn = false;
            mole_timer.Stop();
            label7.Text = "Your score is " + scorecount.ToString() + " out of " + escapedcount.ToString();
            dataGridView2.Rows.Add(player.name ,scorecount.ToString());

            using (StreamWriter fileOut = new StreamWriter("rec.txt",true))
            {

                fileOut.WriteLine(player.name + " " + scorecount.ToString());


            }
            int max = 0;

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (!row.IsNewRow && int.Parse(row.Cells["wefw"].Value.ToString()) > max)

                    max = int.Parse(row.Cells["wefw"].Value.ToString());
                

            }

            if (scorecount >= max)
            {

                panel2.Controls.Add(pictureBox5);
                pictureBox5.BringToFront();
            }
            else
            {

                panel2.Controls.Add(pictureBox4);
                pictureBox4.BringToFront();
            }
            this.Controls.Add(panel2);

        }

        void level1()
        {
            mole_timer.Interval = 5000;
        }

        void level2()
        {
            mole_timer.Interval = 700;
        }

        //private void button6_Click(object sender, EventArgs e)
        //{
        //    level2();
        //    this.Controls.Remove(panel2);
        //    scorecount = 0;
        //    escapedcount = 0;
        //    gameOn = true;
        //    mole_timer.Start();
        //}


  

    }
}
