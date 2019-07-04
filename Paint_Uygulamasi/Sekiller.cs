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
        int mouseX, mouseY;
        int basX, basY;
        public Dikdortgen(int x, int y, Pen kalem)
        {
            this.BaslaX = x;
            this.BaslaY = y;
            basX = x;
            basY = y;
            this.Kalem = kalem;
            
        }
        public override void Ciz(PaintEventArgs e)
        {
            Rectangle dikdortgen = new Rectangle(this.BaslaX, this.BaslaY, this.Genislik, this.Yukseklik);
            e.Graphics.DrawRectangle(this.Kalem, dikdortgen);

        }

        public void Guncelle(int x, int y, int suanX, int suanY)
        {
            
            if (basX < suanX && basY < suanY)
            {
                this.BaslaX = x;
                this.BaslaY = y;
                Genislik = suanX - BaslaX;
                Yukseklik = suanY - BaslaY;
            }
            else if (basX > suanX && basY < suanY)
            {
                BaslaX = suanX;
                BaslaY = y;
                Genislik = basX - suanX;
                Yukseklik = suanY - basY;
            }
            else if (basX > suanX && basY > suanY)
            {
                BaslaX = suanX;
                BaslaY = suanY;
                Genislik = basX - suanX;
                Yukseklik = basY - suanY;
            }
            else if (basX < suanX && basY > suanY)
            {
                BaslaX = x;
                BaslaY = suanY;
                Genislik = suanX - basX;
                Yukseklik = basY - suanY;
            }
            
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

    class Cember : Sekiller
    {
        
        public Cember(int x, int y, Pen kalem)
        {
            this.BaslaX = x;
            this.BaslaY = y;
            this.Kalem = kalem;
        }

        public override void Ciz(PaintEventArgs e)
        {
            e.Graphics.DrawEllipse(Kalem, BaslaX, BaslaY, Genislik, Yukseklik);
        }

        public void Guncelle(int x, int y, int genislik, int yukseklik)
        {
            this.BaslaX = x;
            this.BaslaY = y;
            this.Genislik = genislik;
            this.Yukseklik = yukseklik;
        }

    }

    class Besgen : Sekiller
    {
        Point p1, p2, p3, p4, p5;
        public Besgen(int x, int y, Pen kalem)
        {
            this.BaslaX = x;
            this.BaslaY = y;
            this.Kalem = kalem;
        }

        public override void Ciz(PaintEventArgs e)
        {
            e.Graphics.DrawLine(Kalem, p1, p2);
            e.Graphics.DrawLine(Kalem, p2, p3);
            e.Graphics.DrawLine(Kalem, p3, p4);
            e.Graphics.DrawLine(Kalem, p4, p5);
            e.Graphics.DrawLine(Kalem, p5, p1);
        }

        public void Guncelle(int x, int y)
        {
            p1 = new Point(BaslaX + (x -BaslaX)/2, BaslaY);
            p2 = new Point(x, BaslaY + (y - BaslaY) / 3);
            p3 = new Point(BaslaX + 3 * (x - BaslaX) / 4, y);
            p4 = new Point(BaslaX + (x - BaslaX) / 4, y);
            p5 = new Point(BaslaX, BaslaY + (y - BaslaY) / 3);
        }
    }
}
