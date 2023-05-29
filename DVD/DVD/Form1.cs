using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVD
{
    public partial class Form1 : Form
    {
        
        Graphics g;
        DVD dvd;
        bool gameloop = false;
        
        public Form1()
        {
            InitializeComponent();
            dvd = new DVD();

        }
        
        private void timerTick(float timedelta)
        {
            if (g != null)
            {
                dvd.Draw(g, timedelta);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void Form1_Paint(object sender, PaintEventArgs e)
        {
            g = CreateGraphics();
            
            if (!gameloop)
            {
                gameloop = true;
                Task.Run(() => {
                    DateTime last = DateTime.Now;
                    while (gameloop)
                    {
                        if ((DateTime.Now - last).TotalSeconds>1.0/25.0)
                        {
                            DateTime nw = DateTime.Now;
                            float delta = (float)((nw - last).TotalSeconds);
                            last = nw;
                            timerTick(1.0f/25.0f);
                        }
                    }
                });
            }
        }
    }
}
