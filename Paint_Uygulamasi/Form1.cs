﻿using System;
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

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        private void Panel1_MouseDown(object sender, MouseEventArgs e)
        {
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

        
        bool isMouseDown = false;


        Dikdortgen dikdortgen;
        private void Cizim_Alani_MouseDown(object sender, MouseEventArgs e)
        {
            
            isMouseDown = true;
            dikdortgen = new Dikdortgen(e.X, e.Y, new Pen(Color.Red, 3));
        }

        
        
        
        private void Cizim_Alani_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                dikdortgen.Guncelle(e.X - X, e.Y - Y);
                Refresh();
            }
        }

        private void Cizim_Alani_MouseUp(object sender, MouseEventArgs e)
        {
            dikdortgen.Guncelle(e.X - X, e.Y - Y);
            isMouseDown = false;
        }

        private void Cizim_Alani_Paint(object sender, PaintEventArgs e)
        {
            if (isMouseDown)
                dikdortgen.Ciz(e);
        }
    }
}
