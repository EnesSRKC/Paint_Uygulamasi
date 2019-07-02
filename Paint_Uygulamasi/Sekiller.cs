using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Paint_Uygulamasi
{
    class Sekiller
    {
        public int BaslaX, BaslaY;
        public int genislik, yukseklik;
        public Pen kalem;

    }

    class Dikdortgen:Sekiller
    {
        public Dikdortgen(int x, int y, Pen kalem)
        {
            this.BaslaX = x;
            this.BaslaY = y;
            this.kalem = kalem;
            
        }
        public void Ciz(PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(this.kalem, this.BaslaX, this.BaslaY, this.genislik, this.yukseklik);
        }

        public void Guncelle(int genislik, int yukseklik)
        {
            this.genislik = genislik;
            this.yukseklik = yukseklik;
        }
       
    }
}
