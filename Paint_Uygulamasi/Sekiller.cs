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
        //public int BaslaX, BaslaY;
        public int BaslaX { get; set; }
        public int BaslaY { get; set; }

        //public int genislik, yukseklik;
        public int Genislik { get; set; }
        public int Yukseklik { get; set; }
        //public Pen kalem;
        public Pen Kalem { get; set; }

        public virtual void Ciz(PaintEventArgs e)
        {

        }

        
        //public List<Dikdortgen> dikdortgens = new List<Dikdortgen>();
        //public List<Ucgen> ucgens = new List<Ucgen>();
        public List<Sekiller> sekillers = new List<Sekiller>();

        
    }

    class Dikdortgen:Sekiller
    {
        public Dikdortgen(int x, int y, Pen kalem)
        {
            this.BaslaX = x;
            this.BaslaY = y;
            this.Kalem = kalem;
            
        }
        public override void Ciz(PaintEventArgs e)
        {
            Rectangle dikdortgen = new Rectangle(this.BaslaX, this.BaslaY, this.Genislik, this.Yukseklik);
            e.Graphics.DrawRectangle(this.Kalem, dikdortgen);

        }

        public void Guncelle(int x, int y, int genislik, int yukseklik)
        {
            this.BaslaX = x;
            this.BaslaY = y;
            this.Genislik = genislik;
            this.Yukseklik = yukseklik;
        }
       
    }

    class Ucgen:Sekiller
    {
        Point p1,p2,p3;
        public Ucgen(int x, int y, Pen kalem)
        {
            this.BaslaX = x;
            this.BaslaY = y;
            this.Kalem = kalem;
            p1 = new Point(x, y);
        }

        public override void Ciz(PaintEventArgs e)
        {
            e.Graphics.DrawLine(Kalem, p1, p2);
            e.Graphics.DrawLine(Kalem, p2, p3);
            e.Graphics.DrawLine(Kalem, p1, p3);
        }
        public void Guncelle(int x, int y)
        {
            p2 = new Point(x, y);
            p3 = new Point(BaslaX - (x - BaslaX), y);
            
        }

    }
}
