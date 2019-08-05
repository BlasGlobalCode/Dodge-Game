using System;
using System.Drawing;
using System.Windows.Forms;

namespace Dodge_Game
{
    public partial class MainGame : Form
    {
        int fallGeschwindigkeit = 10;
        bool springen = false;
        bool links = false;
        bool recht = false;
        int staerke = 8;
        int coins = 0;
        public MainGame()
        {
            InitializeComponent();
            MessageBox.Show(" 'ESCAPE' to EXIT");
        }
        private void isGedrueckt(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) { links = true; }
            if (e.KeyCode == Keys.Right) { recht = true; }
            if(e.KeyCode == Keys.Escape) { this.Close(); }
            if(e.KeyCode == Keys.Up && !springen) { springen = true; }
        }
        private void isLosGelassen(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) { links = false; }
            if (e.KeyCode == Keys.Right) { recht = false; }
            if (springen) { springen = false; }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            spieler.Top += fallGeschwindigkeit;

            if(springen && staerke < 0) { springen = false; }
            if (links) { spieler.Left -= 15; }
            if (recht) { spieler.Left += 15; }
            if (springen) { fallGeschwindigkeit = -20;  staerke -= 1; } else { fallGeschwindigkeit = 15; }

            foreach (Control prueffer in this.Controls)
            {
                if (prueffer is PictureBox && prueffer.Tag == "platform")
                {
                    //prueffer.Visible = false;

                    if (spieler.Bounds.IntersectsWith(prueffer.Bounds) && !springen)
                    {
                        staerke = 20;
                        spieler.Top = prueffer.Top - spieler.Height;
                    }
                }
                if(prueffer is PictureBox && prueffer.Tag == "Coins")
                {
                    prueffer.BackColor = Color.Blue;
                    if (spieler.Bounds.IntersectsWith(prueffer.Bounds) && !springen)
                    {
                        coins++;
                        this.Controls.Remove(prueffer);
                        lblScore.Text = coins + " UC".ToString();
                    }
                }
                if (prueffer is Button && prueffer.Tag == "Dead")
                {
                    prueffer.Enabled = false;
                    if (spieler.Bounds.IntersectsWith(prueffer.Bounds))
                    {
                        timer1.Stop();
                        Won.Text = "DU HAST VERLOREN !!";
                        Won.Visible = true;
                    }
                }
            }

            if (spieler.Bounds.IntersectsWith(Ziel.Bounds)){
                timer1.Stop();
                Won.Text = "DU HAST GEWONNEN !!";
                Won.Visible = true;
            }
        }
    }
}
