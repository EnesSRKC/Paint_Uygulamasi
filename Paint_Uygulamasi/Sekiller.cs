﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Paint_Uygulamasi
{
    class Sekiller
    {
        public string sekilAd { get; set; }
        public int BaslaX { get; set; }
        public int BaslaY { get; set; }
        public int Genislik { get; set; }
        public int Yukseklik { get; set; }

        public bool Secilmismi { get; set; }
        public Pen Kalem { get; set; }


        public virtual void Ciz(PaintEventArgs e)
        {

        }

       
        public List<Sekiller> sekillers = new List<Sekiller>();

        public List<Point> points = new List<Point>();

        
    }

    class Dikdortgen:Sekiller
    {
        int basX, basY;
        public Dikdortgen(string ad, int x, int y, Pen kalem)
        {
            this.Secilmismi = false;
            this.sekilAd = ad;
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
        int BasX, BasY;
        public Ucgen(string ad, int x, int y, Pen kalem)
        {
            this.Secilmismi = false;
            this.sekilAd = ad;
            this.Kalem = kalem;
            this.BaslaX = x;
            this.BaslaY = y;
            BasX = x;
            BasY = y;

        }

        public override void Ciz(PaintEventArgs e)
        {
            e.Graphics.DrawLine(Kalem, p1, p2);
            e.Graphics.DrawLine(Kalem, p2, p3);
            e.Graphics.DrawLine(Kalem, p1, p3);
        }
        public void Guncelle(int x, int y)
        {
            if(x > BasX && y > BasY)
            {
                this.Genislik = x - BasX;
                this.Yukseklik = y - BasY;
                p1 = new Point(Genislik/2 + BasX, BasY);
                p2 = new Point(x, y);
                p3 = new Point(BasX, y);
            }
            else if(x < BasX && y > BasY)
            {
                this.BaslaX = x;
                this.BaslaY = BasY;
                this.Genislik = BasX - x;
                this.Yukseklik = y - BasY;
                p1 = new Point(Genislik / 2 + x, BasY);
                p2 = new Point(x, y);
                p3 = new Point(BasX, y);
            }
            else if(x < BasX && y < BasY)
            {
                this.BaslaX = x;
                this.BaslaY = y;
                this.Genislik = BasX - x;
                this.Yukseklik = BasY - y;
                p1 = new Point(Genislik / 2 + x, y);
                p2 = new Point(x, BasY);
                p3 = new Point(BasX, BasY);
            }
            else if(x > BasX && y < BasY)
            {
                this.BaslaX = BasX;
                this.BaslaY = y;
                this.Genislik = x - BasX;
                this.Yukseklik = BasY - y;
                p1 = new Point(Genislik / 2 + BasX, y);
                p2 = new Point(x, BasY);
                p3 = new Point(BasX, BasY);
            }
        }

        public List<Point> NoktaGetir()
        {
            List<Point> p = new List<Point>();
            p.Add(p1);
            p.Add(p2);
            p.Add(p3);

            return p;
        }

    }

    class Cember : Sekiller
    {
        int basX, basY;
        public Cember(string ad, int x, int y, Pen kalem)
        {
            this.Secilmismi = false;
            this.sekilAd = ad;
            this.BaslaX = x;
            this.BaslaY = y;
            basX = x;
            basY = y;
            this.Kalem = kalem;
        }

        public override void Ciz(PaintEventArgs e)
        {
            e.Graphics.DrawEllipse(Kalem, BaslaX, BaslaY, Genislik, Yukseklik);
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

    class Besgen : Sekiller
    {
        Point p1, p2, p3, p4, p5;
        int BasX, BasY;

        public Besgen(string ad, int x, int y, Pen kalem)
        {
            this.Secilmismi = false;
            this.sekilAd = ad;
            this.BaslaX = x;
            this.BaslaY = y;
            BasX = x;
            BasY = y;
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
            p1 = new Point(BasX + (x -BasX)/2, BasY);
            p2 = new Point(x, BasY + (y - BasY) / 3);
            p3 = new Point(BasX + 3 * (x - BasX) / 4, y);
            p4 = new Point(BasX + (x - BasX) / 4, y);
            p5 = new Point(BasX, BasY + (y - BasY) / 3);

            if (x > BasX && y > BasY)
            {
                this.BaslaX = BasX;
                this.BaslaY = BasY;
                this.Genislik = x - BasX;
                this.Yukseklik = y - BasY;
                
            }
            else if (x < BasX && y > BasY)
            {
                this.BaslaX = x;
                this.BaslaY = BasY;
                this.Genislik = BasX - x;
                this.Yukseklik = y - BasY;
                
            }
            else if (x < BasX && y < BasY)
            {
                this.BaslaX = x;
                this.BaslaY = y;
                this.Genislik = BasX - x;
                this.Yukseklik = BasY - y;
                
            }
            else if (x > BasX && y < BasY)
            {
                this.BaslaX = BasX;
                this.BaslaY = y;
                this.Genislik = x - BasX;
                this.Yukseklik = BasY - y;
                
            }


        }

        public List<Point> NoktaGetir()
        {
            List<Point> p = new List<Point>();
            p.Add(p1);
            p.Add(p2);
            p.Add(p3);
            p.Add(p4);
            p.Add(p5);

            return p;
        }
    }

    class Cizgi : Sekiller
    {

        Point p1, p2;
        int BasX, BasY;
        public Cizgi(string name, int x , int y,Pen kalem)
        {
            this.Secilmismi = false;
            this.sekilAd = name;
            this.BaslaX = x;
            this.BaslaY = y;
            BasX = x;
            BasY = y;
            this.Kalem = kalem;
            p1 = new Point(BaslaX, BaslaY);
            p2 = new Point(BaslaX, BaslaY);
        }

        public override void Ciz(PaintEventArgs e)
        {
            e.Graphics.DrawLine(Kalem, p1, p2);
        }
        public void Guncelle(int x, int y)
        {
            p2 = new Point(x, y);

            if (x > BasX && y >= BasY)
            {
                this.Genislik = x - BasX;
                this.Yukseklik = y - BasY;
                this.BaslaX = BasX;
                this.BaslaY = BasY;
            }
            else if (x <= BasX && y > BasY)
            {
                
                this.BaslaX = x;
                this.BaslaY = BasY;
                this.Genislik = BasX - x;
                this.Yukseklik = y - BasY;
                
            }
            else if (x < BasX && y <= BasY)
            {
                this.BaslaX = x;
                this.BaslaY = y;
                this.Genislik = BasX - x;
                this.Yukseklik = BasY - y;
            }
            else if (x > BasX && y < BasY)
            {
                this.BaslaX = BasX;
                this.BaslaY = y;
                this.Genislik = x - BasX;
                this.Yukseklik = BasY - y;
            } 
        }

        public List<Point> NoktaGetir()
        {
            List<Point> noktalar = new List<Point>();
            noktalar.Add(p1);
            noktalar.Add(p2);

            return noktalar;
        }

    }

    class Secim : Sekiller
    {
        public Secim(int x,int y,int genislik, int yukseklik, Pen kalem)
        {
            this.Kalem = kalem;
            this.BaslaX = x;
            this.BaslaY = y;
            this.Genislik = genislik+40;
            this.Yukseklik = yukseklik+40;
        }
        
        public override void Ciz(PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(Kalem, BaslaX, BaslaY, Genislik, Yukseklik);
        }

    }
}
