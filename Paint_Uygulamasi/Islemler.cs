using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint_Uygulamasi
{
    class Islemler
    {
        OpenFileDialog ofd = new OpenFileDialog();
        SaveFileDialog sfd = new SaveFileDialog();

        public void DosyaOku(Dikdortgen dikdortgen, Ucgen ucgen, Cember cember, Besgen besgen, Cizgi cizgi, Sekiller sekil)
        {
            ofd.Filter = "text Files (*.txt) | *.txt";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);

                sekil.sekillers.Clear();
                string[] veriler = new string[10];


                string veri = sr.ReadLine();
                while (veri != null)
                {
                    veriler = veri.Split(' ');

                    if (veriler[0] == "Dikdortgen")
                    {
                        dikdortgen = new Dikdortgen("Dikdortgen", Convert.ToInt16(veriler[2]), Convert.ToInt16(veriler[3]), new Pen(System.Drawing.Color.FromArgb(((int)(((byte)(Convert.ToInt16(veriler[6]))))), ((int)(((byte)(Convert.ToInt16(veriler[7]))))), ((int)(((byte)(Convert.ToInt16(veriler[8])))))), Convert.ToInt16(veriler[9])));
                        dikdortgen.Genislik = Convert.ToInt16(veriler[4]);
                        dikdortgen.Yukseklik = Convert.ToInt16(veriler[5]);
                        sekil.sekillers.Add(dikdortgen);
                    }
                    else if (veriler[0] == "Ucgen")
                    {
                        ucgen = new Ucgen("Ucgen", Convert.ToInt16(veriler[2]), Convert.ToInt16(veriler[3]), new Pen(System.Drawing.Color.FromArgb(((int)(((byte)(Convert.ToInt16(veriler[8]))))), ((int)(((byte)(Convert.ToInt16(veriler[9]))))), ((int)(((byte)(Convert.ToInt16(veriler[10])))))), Convert.ToInt16(veriler[11])));
                        ucgen.Guncelle(Convert.ToInt16(veriler[4]), Convert.ToInt16(veriler[5]));
                        ucgen.points = ucgen.NoktaGetir();
                        sekil.sekillers.Add(ucgen);
                        
                    }
                    else if (veriler[0] == "Cember")
                    {
                        cember = new Cember("Cember", Convert.ToInt16(veriler[2]), Convert.ToInt16(veriler[3]), new Pen(System.Drawing.Color.FromArgb(((int)(((byte)(Convert.ToInt16(veriler[6]))))), ((int)(((byte)(Convert.ToInt16(veriler[7]))))), ((int)(((byte)(Convert.ToInt16(veriler[8])))))), Convert.ToInt16(veriler[9])));
                        cember.Genislik = Convert.ToInt16(veriler[4]);
                        cember.Yukseklik = Convert.ToInt16(veriler[5]);
                        sekil.sekillers.Add(cember);

                    }
                    else if (veriler[0] == "Besgen")
                    {
                        besgen = new Besgen("Besgen", Convert.ToInt16(veriler[5]), Convert.ToInt16(veriler[2]), new Pen(System.Drawing.Color.FromArgb(((int)(((byte)(Convert.ToInt16(veriler[6]))))), ((int)(((byte)(Convert.ToInt16(veriler[7]))))), ((int)(((byte)(Convert.ToInt16(veriler[8])))))), Convert.ToInt16(veriler[9])));
                        besgen.Guncelle(Convert.ToInt16(veriler[3]), Convert.ToInt16(veriler[4]));
                        besgen.points = besgen.NoktaGetir();
                        sekil.sekillers.Add(besgen);

                    }
                    else if (veriler[0] == "Cizgi")
                    {
                        cizgi = new Cizgi("Cizgi", Convert.ToInt16(veriler[2]), Convert.ToInt16(veriler[3]), new Pen(System.Drawing.Color.FromArgb(((int)(((byte)(Convert.ToInt16(veriler[6]))))), ((int)(((byte)(Convert.ToInt16(veriler[7]))))), ((int)(((byte)(Convert.ToInt16(veriler[8])))))), Convert.ToInt16(veriler[9])));
                        cizgi.Guncelle(Convert.ToInt16(veriler[4]), Convert.ToInt16(veriler[5]));
                        cizgi.points = cizgi.NoktaGetir();
                        sekil.sekillers.Add(cizgi);
                    }

                    veri = sr.ReadLine();
                }
                
                sr.Close();
                fs.Close();
            }

        }
        public void DosyaYaz(Dikdortgen dikdortgen, Ucgen ucgen, Cember cember, Besgen besgen,Cizgi cizgi, List<Sekiller> sekiller)
        {
            sfd.InitialDirectory = @"./";
            sfd.Filter = "text Files (*.txt) | *.txt";
            sfd.DefaultExt = "txt";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Stream fs = sfd.OpenFile();
                StreamWriter sw = new StreamWriter(fs);
                try
                {
                    foreach (var item in sekiller)
                    {
                        if (item.sekilAd == "Dikdortgen")
                            sw.WriteLine(item.sekilAd + " : " + item.BaslaX + " " + item.BaslaY + " " + item.Genislik + " " + item.Yukseklik + " " + item.Kalem.Color.R + " " + item.Kalem.Color.G + " " + item.Kalem.Color.B + " " + item.Kalem.Width);
                        else if (item.sekilAd == "Ucgen")
                            sw.WriteLine(item.sekilAd + " : " + item.points[0].X + " " + item.points[0].Y + " " + item.points[1].X + " " + item.points[1].Y + " " + item.points[2].X + " " + item.points[2].Y + " " + item.Kalem.Color.R + " " + item.Kalem.Color.G + " " + item.Kalem.Color.B + " " + item.Kalem.Width);
                        else if (item.sekilAd == "Cember")
                            sw.WriteLine(item.sekilAd + " : " + item.BaslaX + " " + item.BaslaY + " " + item.Genislik + " " + item.Yukseklik + " " + item.Kalem.Color.R + " " + item.Kalem.Color.G + " " + item.Kalem.Color.B + " " + item.Kalem.Width);
                        else if (item.sekilAd == "Besgen")
                            sw.WriteLine(item.sekilAd + " : " + item.points[0].Y + " " + item.points[1].X + " " + item.points[2].Y + " " + item.points[4].X + " " + item.Kalem.Color.R + " " + item.Kalem.Color.G + " " + item.Kalem.Color.B + " " + item.Kalem.Width);
                        else if (item.sekilAd == "Cizgi")
                            sw.WriteLine(item.sekilAd + " : " + item.points[0].X + " " + item.points[0].Y + " " + item.points[1].X + " " + item.points[1].Y + " " + item.Kalem.Color.R + " " + item.Kalem.Color.G + " " + item.Kalem.Color.B + " " + item.Kalem.Width);
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message,"Uyarı");
                }

                sw.Close();
                fs.Close();

            }
        }
    }
}
