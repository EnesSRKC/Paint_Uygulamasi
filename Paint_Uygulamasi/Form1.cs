﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        bool selSecilimi = false;
        bool copTiklandimi = false;
        bool secimDogrumu = false;



        Dikdortgen dikdortgen;
        Ucgen ucgen;
        Cember cember;
        Besgen besgen;
        Secim secim;
        Cizgi cizgi;

        Sekiller sekil = new Sekiller();
        Pen pen;

        Pen secimKalem = new Pen(Color.Gray, 3) { DashPattern = new float[] { 5, 1.5f } };
        private void Cizim_Alani_MouseDown(object sender, MouseEventArgs e)
        {
            
            X = e.X;
            Y = e.Y;
            isMouseDown = true;
            Color renk = pb_RenkSecim.BackColor;
            int boyut;
            try
            {
                boyut = Convert.ToInt16(comboBox1.Text);
                pen = new Pen(renk, boyut);
            }
            catch (Exception)
            {
                MessageBox.Show("Boyut olarak tam sayı giriniz, ondalıklı sayılar desteklenmemektedir.", "Uyarı");
                isMouseDown = false;
            }

            if (dikSecilimi)
                dikdortgen = new Dikdortgen("Dikdortgen", X, Y, pen);
            else if (kalemSecilimi)
                cizgi = new Cizgi("Cizgi", X, Y, pen);
            else if (ucgenSecilimi)
                ucgen = new Ucgen("Ucgen", X, Y, pen);
            else if (cemberSecilimi)
                cember = new Cember("Cember", X, Y, pen);
            else if (besgenSecilimi)
                besgen = new Besgen("Besgen", X, Y, pen);
            else if (selSecilimi)
            {

                if (sekil.sekillers.Count > 0)
                {
                    for (int i = sekil.sekillers.Count - 1; i >= 0; i--)
                    {
                        /*Sekil koordinatlarinin icerisine tiklanip tiklanmadigi kontrol ediliyor.
                         * e.X ve e.Y' ye 10 eklenmesi veya cikarilmasi secme isleminin cok hassas olmamasini istedigimiz icindir.
                         */
                        if (e.X + 10 > sekil.sekillers[i].BaslaX && e.X - 10 <= sekil.sekillers[i].BaslaX + sekil.sekillers[i].Genislik && e.Y + 10 >= sekil.sekillers[i].BaslaY && e.Y - 10 <= sekil.sekillers[i].BaslaY + sekil.sekillers[i].Yukseklik)
                        {
                            secimDogrumu = true;
                            secim = new Secim(sekil.sekillers[i].BaslaX - 20, sekil.sekillers[i].BaslaY - 20, sekil.sekillers[i].Genislik, sekil.sekillers[i].Yukseklik, secimKalem);
                            foreach (var item in sekil.sekillers)
                            {
                                if (item == sekil.sekillers[i])
                                    item.Secilmismi = true;
                                else
                                    item.Secilmismi = false;

                            }
                            break;
                        }
                        else
                            secimDogrumu = false;
                    }
                }
                else
                    secimDogrumu = false;
            }
            Refresh();
        }

        


        private void Cizim_Alani_MouseMove(object sender, MouseEventArgs e)
        {
            
            if (isMouseDown)
            {
                if (dikSecilimi)
                {
                    dikdortgen.Guncelle(X, Y, e.X, e.Y);
                    Refresh();

                }
                else if (kalemSecilimi)
                {
                    cizgi.Guncelle(e.X, e.Y);
                    Refresh();
                }
                else if (ucgenSecilimi)
                {
                    ucgen.Guncelle(e.X, e.Y);
                    Refresh();
                }
                else if (cemberSecilimi)
                {
                    cember.Guncelle(X, Y, e.X, e.Y);
                    Refresh();
                }
                else if (besgenSecilimi)
                {
                    besgen.Guncelle(e.X, e.Y);
                    Refresh();
                }


            }
        }

        private void Cizim_Alani_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            if (dikSecilimi)
                sekil.sekillers.Add(dikdortgen);
            else if (ucgenSecilimi)
            {
                sekil.sekillers.Add(ucgen);
                ucgen.points = ucgen.NoktaGetir();
            }
            else if (cemberSecilimi)
                sekil.sekillers.Add(cember);
            else if (besgenSecilimi)
            {
                sekil.sekillers.Add(besgen);
                besgen.points = besgen.NoktaGetir();
            }
            else if (kalemSecilimi)
            {
                sekil.sekillers.Add(cizgi);
                cizgi.points = cizgi.NoktaGetir();
            }
        }

        private void Pb_Cop_MouseUp(object sender, MouseEventArgs e)
        {
            pb_Cop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            if (selSecilimi)
            {
                foreach (var item in sekil.sekillers)
                {
                    if (item.Secilmismi)
                    {
                        sekil.sekillers.Remove(item);
                        copTiklandimi = true;
                        Refresh();
                        break;
                    }
                }
                copTiklandimi = false;
            }
        }

        private void Cizim_Alani_Paint(object sender, PaintEventArgs e)
        {

            foreach (var item in sekil.sekillers) 
            {
                item.Ciz(e);
            }
            if (dikSecilimi)
                dikdortgen.Ciz(e);
            else if (kalemSecilimi)
                cizgi.Ciz(e);
            else if (ucgenSecilimi)
                ucgen.Ciz(e);
            else if (cemberSecilimi)
                cember.Ciz(e);
            else if (besgenSecilimi)
                besgen.Ciz(e);
            else if (selSecilimi && secim != null && !copTiklandimi && secimDogrumu)
                secim.Ciz(e);

        }


        Islemler islem = new Islemler();
        private void KaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            islem.DosyaYaz(dikdortgen, ucgen, cember, besgen, cizgi, sekil.sekillers);
        }

        private void AçToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dikSecilimi = false;
            ucgenSecilimi = false;
            cemberSecilimi = false;
            besgenSecilimi = false;
            kalemSecilimi = false;
            selSecilimi = false;

            pb_Pen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            pb_Ucgen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            pb_Cember.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            pb_Besgen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            pb_Dikdortgen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            pb_Select.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));

            islem.DosyaOku(dikdortgen, ucgen, cember, besgen, cizgi, sekil);
            Refresh();
        }





        /************************ GORSELLIK ICIN YAZILAN KODLAR *****************************/







        private void Pb_Pen_MouseEnter(object sender, EventArgs e)
        {
            pb_Pen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
        }

        private void Pb_Pen_MouseLeave(object sender, EventArgs e)
        {
            if(!kalemSecilimi)
                pb_Pen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
        }


        private void Pb_Pen_Click(object sender, EventArgs e)
        {
            kalemSecilimi = true;
            dikSecilimi = false;
            ucgenSecilimi = false;
            cemberSecilimi = false;
            besgenSecilimi = false;
            selSecilimi = false;

            pb_Pen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            pb_Ucgen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            pb_Cember.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            pb_Besgen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            pb_Dikdortgen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            pb_Select.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
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
            selSecilimi = false;

            pb_Ucgen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            pb_Cember.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            pb_Besgen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            pb_Pen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            pb_Select.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
        }

        private void Pb_Ucgen_Click(object sender, EventArgs e)
        {
            dikSecilimi = false;
            ucgenSecilimi = true;
            cemberSecilimi = false;
            besgenSecilimi = false;
            kalemSecilimi = false;
            selSecilimi = false;

            pb_Dikdortgen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            pb_Cember.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            pb_Besgen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            pb_Pen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            pb_Select.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
        }

        private void Pb_Cember_Click(object sender, EventArgs e)
        {
            dikSecilimi = false;
            ucgenSecilimi = false;
            cemberSecilimi = true;
            besgenSecilimi = false;
            kalemSecilimi = false;
            selSecilimi = false;

            pb_Ucgen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            pb_Dikdortgen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            pb_Besgen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            pb_Pen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            pb_Select.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
        }

        private void Pb_Besgen_Click(object sender, EventArgs e)
        {
            dikSecilimi = false;
            ucgenSecilimi = false;
            cemberSecilimi = false;
            besgenSecilimi = true;
            kalemSecilimi = false;
            selSecilimi = false;

            pb_Ucgen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            pb_Cember.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            pb_Dikdortgen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            pb_Pen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            pb_Select.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
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

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 4;
        }

        private void Pb_Select_MouseEnter(object sender, EventArgs e)
        {
            pb_Select.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
        }

        private void Pb_Select_MouseLeave(object sender, EventArgs e)
        {
            if(!selSecilimi)
                pb_Select.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
        }

        private void Pb_Select_Click(object sender, EventArgs e)
        {
            selSecilimi = true;
            dikSecilimi = false;
            ucgenSecilimi = false;
            cemberSecilimi = false;
            besgenSecilimi = false;
            kalemSecilimi = false;

            pb_Ucgen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            pb_Cember.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            pb_Dikdortgen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            pb_Pen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            pb_Besgen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
        }

        private void Pb_Cop_MouseDown(object sender, MouseEventArgs e)
        {
            pb_Cop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
        }

        
    }
}
