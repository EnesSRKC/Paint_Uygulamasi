using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint_Uygulamasi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        

        private void Panel1_MouseDown(object sender, MouseEventArgs e)
        {

            //Formu hareket ettirebilmek için yazıldı.
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }


        private void PictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));

        }

        private void PictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        int X;
        int Y;
        bool isMouseDown = false;
        bool kalemSecilimi = false;
        bool dikSecilimi = false;
        bool cemberSecilimi = false;
        bool ucgenSecilimi = false;
        bool besgenSecilimi = false;




        Dikdortgen dikdortgen;
        Ucgen ucgen;
        Cember cember;

        Sekiller sekil = new Sekiller();
        Pen pen;
        private void Cizim_Alani_MouseDown(object sender, MouseEventArgs e)
        {
            X = e.X;
            Y = e.Y;
            isMouseDown = true;
            Color renk = pb_RenkSecim.BackColor;
            pen = new Pen(renk, 3);

            if (dikSecilimi)
                dikdortgen = new Dikdortgen(X, Y, pen);
            else if (kalemSecilimi)
            {

            }
            else if (ucgenSecilimi)
                ucgen = new Ucgen(X, Y, pen);
            else if (cemberSecilimi)
                cember = new Cember(X, Y, pen);
        }




        
        private void Cizim_Alani_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                if (dikSecilimi)
                {
                    dikdortgen.Guncelle(X, Y, e.X, e.Y);
                }
                else if (kalemSecilimi)
                {

                }
                else if (ucgenSecilimi)
                {
                    ucgen.Guncelle(e.X, e.Y);
                }
                else if (cemberSecilimi)
                    cember.Guncelle(X, Y, e.X - X, e.Y - Y);

                Refresh();

            }
        }

        private void Cizim_Alani_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            if (dikSecilimi)
                sekil.sekillers.Add(dikdortgen);
            else if (ucgenSecilimi)
                sekil.sekillers.Add(ucgen);
            else if (cemberSecilimi)
                sekil.sekillers.Add(cember);
        }

        private void Cizim_Alani_Paint(object sender, PaintEventArgs e)
        {
            
            
            if (isMouseDown)
            {
                if (sekil.sekillers != null)
                {
                    foreach (var item in sekil.sekillers)
                    {
                        item.Ciz(e);
                    }
                }
                if (dikSecilimi)
                    dikdortgen.Ciz(e);
                else if (kalemSecilimi)
                {

                }
                else if (ucgenSecilimi)
                    ucgen.Ciz(e);
                else if (cemberSecilimi)
                    cember.Ciz(e);
            }
        }

        private void Pb_Pen_MouseEnter(object sender, EventArgs e)
        {
            pb_Pen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
        }

        private void Pb_Pen_MouseLeave(object sender, EventArgs e)
        {
            if(!kalemSecilimi)
                pb_Pen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
        }


        /************************ Gorsellik Icin Click Eventleri *****************************/
        private void Pb_Pen_Click(object sender, EventArgs e)
        {
            kalemSecilimi = true;
            dikSecilimi = false;
            ucgenSecilimi = false;
            cemberSecilimi = false;
            besgenSecilimi = false;
            pb_Pen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            pb_Ucgen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            pb_Cember.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            pb_Besgen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            pb_Dikdortgen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
        }

        private void Pb_Dikdortgen_MouseEnter(object sender, EventArgs e)
        {
            pb_Dikdortgen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
        }

        private void Pb_Ucgen_MouseEnter(object sender, EventArgs e)
        {
            pb_Ucgen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
        }

        private void Pb_Cember_MouseEnter(object sender, EventArgs e)
        {
            pb_Cember.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
        }

        private void Pb_Besgen_MouseEnter(object sender, EventArgs e)
        {
            pb_Besgen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
        }

        private void Pb_Dikdortgen_Click(object sender, EventArgs e)
        {
            dikSecilimi = true;
            ucgenSecilimi = false;
            cemberSecilimi = false;
            besgenSecilimi = false;
            kalemSecilimi = false;

            pb_Ucgen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            pb_Cember.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            pb_Besgen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            pb_Pen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
        }

        private void Pb_Ucgen_Click(object sender, EventArgs e)
        {
            dikSecilimi = false;
            ucgenSecilimi = true;
            cemberSecilimi = false;
            besgenSecilimi = false;
            kalemSecilimi = false;

            pb_Dikdortgen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            pb_Cember.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            pb_Besgen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            pb_Pen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
        }

        private void Pb_Cember_Click(object sender, EventArgs e)
        {
            dikSecilimi = false;
            ucgenSecilimi = false;
            cemberSecilimi = true;
            besgenSecilimi = false;
            kalemSecilimi = false;

            pb_Ucgen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            pb_Dikdortgen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            pb_Besgen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            pb_Pen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
        }

        private void Pb_Besgen_Click(object sender, EventArgs e)
        {
            dikSecilimi = false;
            ucgenSecilimi = false;
            cemberSecilimi = false;
            besgenSecilimi = true;
            kalemSecilimi = false;

            pb_Ucgen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            pb_Cember.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            pb_Dikdortgen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            pb_Pen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
        }

        private void Pb_Dikdortgen_MouseLeave(object sender, EventArgs e)
        {
            if (!dikSecilimi)
                pb_Dikdortgen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
        }

        private void Pb_Ucgen_MouseLeave(object sender, EventArgs e)
        {
            if (!ucgenSecilimi)
                pb_Ucgen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
        }

        private void Pb_Cember_MouseLeave(object sender, EventArgs e)
        {
            if (!cemberSecilimi)
                pb_Cember.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
        }

        private void Pb_Besgen_MouseLeave(object sender, EventArgs e)
        {
            if (!besgenSecilimi)
                pb_Besgen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
        }

        private void Pb_Red_Click(object sender, EventArgs e)
        {
            pb_RenkSecim.BackColor = Color.Red;
        }

        private void Pb_Orange_Click(object sender, EventArgs e)
        {
            pb_RenkSecim.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(28)))));
        }

        private void Pb_Yellow_Click(object sender, EventArgs e)
        {
            pb_RenkSecim.BackColor = Color.Yellow;
        }

        private void Pb_Lime_Click(object sender, EventArgs e)
        {
            pb_RenkSecim.BackColor = Color.Lime;
        }

        private void Pb_Aqua_Click(object sender, EventArgs e)
        {
            pb_RenkSecim.BackColor = Color.Aqua;
        }

        private void Pb_Blue_Click(object sender, EventArgs e)
        {
            pb_RenkSecim.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
        }

        private void Pb_DarkRed_Click(object sender, EventArgs e)
        {
            pb_RenkSecim.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
        }

        private void Pb_Brown_Click(object sender, EventArgs e)
        {
            pb_RenkSecim.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
        }

        private void Pb_DarkYellow_Click(object sender, EventArgs e)
        {
            pb_RenkSecim.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
        }

        private void Pb_DarkLime_Click(object sender, EventArgs e)
        {
            pb_RenkSecim.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
        }

        private void Pb_DarkAqua_Click(object sender, EventArgs e)
        {
            pb_RenkSecim.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
        }

        private void Pb_LightBlue_Click(object sender, EventArgs e)
        {
            pb_RenkSecim.BackColor = Color.Blue;
        }

        private void Pb_Maroon_Click(object sender, EventArgs e)
        {
            pb_RenkSecim.BackColor = Color.Maroon;
        }

        private void Pb_Nergis_Click(object sender, EventArgs e)
        {
            pb_RenkSecim.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
        }

        private void Pb_ZeytinYesili_Click(object sender, EventArgs e)
        {
            pb_RenkSecim.BackColor = Color.Olive;
        }

        private void Pb_Yesil_Click(object sender, EventArgs e)
        {
            pb_RenkSecim.BackColor = Color.Green;
        }

        private void Pb_Teal_Click(object sender, EventArgs e)
        {
            pb_RenkSecim.BackColor = Color.Teal;
        }

        private void Pb_Navy_Click(object sender, EventArgs e)
        {
            pb_RenkSecim.BackColor = Color.Navy;
        }
    }
}
