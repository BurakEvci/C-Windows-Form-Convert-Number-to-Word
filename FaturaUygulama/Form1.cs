/****************************************************************************
** SAKARYA ÜNİVERSİTESİ
** BİLGİSAYAR VE BİLİŞİM BİLİMLERİ FAKÜLTESİ
** BİLGİSAYAR MÜHENDİSLİĞİ BÖLÜMÜ
** NESNEYE DAYALI PROGRAMLAMA DERSİ
** 2021-2022 BAHAR DÖNEMİ
**
** ÖDEV NUMARASI..........: 2
** ÖĞRENCİ ADI............: Burak Can Evci
** ÖĞRENCİ NUMARASI.......: G211210091
** DERSİN ALINDIĞI GRUP...: 2B
****************************************************************************/


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FaturaUygulama
{
    public partial class Form1 : Form
    {
        Label lblYazi = new Label();
        Label lblTutar = new Label();
        Label lblYaziyla = new Label();
        Label lblBaslik = new Label();
        Label lblBaslik2 = new Label();
        Label lblBaslik3 = new Label();

        TextBox txtSayi = new TextBox();
        public Form1()
        {
            InitializeComponent();
            lblBaslik.Top = 40;
            lblBaslik.Left = 70;
            lblBaslik.Width = 1000;
            lblBaslik.Text = "Kuruş İçin Virgül Kullanınız.";
            this.Controls.Add(lblBaslik);

            lblBaslik3.Top = 15;
            lblBaslik3.Left = 70;
            lblBaslik3.Width = 300;
            lblBaslik3.ForeColor = Color.Red;
            lblBaslik3.BorderStyle = BorderStyle.FixedSingle;
            lblBaslik3.Text = "Fatura Tutar Çeviriciye Hoşgeldiniz.";
            lblBaslik3.BackColor = Color.Yellow;
            this.Controls.Add(lblBaslik3);

            lblBaslik2.Left = 70;
            lblBaslik2.Top = 60;
            lblBaslik2.Width = 1000;
            lblBaslik2.Text = "Rakam Dışında Karakter Kullanmayınız.";
            this.Controls.Add(lblBaslik2);


            lblTutar.Top = 100;
            lblTutar.Left = 70;
            lblTutar.Text = "Fatura Tutarı:";
            lblTutar.BackColor = Color.Aqua;
            lblTutar.BorderStyle = BorderStyle.Fixed3D;
            this.Controls.Add(lblTutar);

            lblYaziyla.Top = 130;
            lblYaziyla.Left = 70;
            lblYaziyla.Text = "Tutar Yazıyla:";
            lblYaziyla.BorderStyle = BorderStyle.Fixed3D;
            lblYaziyla.BackColor = Color.Aqua;
            this.Controls.Add(lblYaziyla);

            lblYazi.Top = 130;
            lblYazi.Left = 200;
            lblYazi.Width = 400;
            lblYazi.BorderStyle = BorderStyle.FixedSingle;
            lblYazi.BackColor = Color.GreenYellow;
            this.Controls.Add(lblYazi);


            txtSayi.Top = 100;
            txtSayi.Left = 200;
            txtSayi.Focus();
            txtSayi.BackColor = Color.GreenYellow;
            this.Controls.Add(txtSayi);


            Button btnHesapla = new Button();
            btnHesapla.Text = "Hesapla";
            btnHesapla.Top = 200;
            btnHesapla.Left = 200;
            btnHesapla.BackColor = Color.GreenYellow;
            btnHesapla.FlatStyle = FlatStyle.Popup;
            AcceptButton = btnHesapla;
            this.Controls.Add(btnHesapla);


            btnHesapla.Click += new EventHandler(Olustur);

            BackColor = Color.Pink;
        }

        void Olustur(object sender, EventArgs e)
        {
            char aranan = ',';
            int virgulKontrol;
            virgulKontrol = txtSayi.Text.IndexOf(aranan);//virgülün olup olmadığı ve varsa kaçıncı basamakta bulunduğu
            if (virgulKontrol == -1) //virgül olmadığında
            {
                if (txtSayi.Text.Length <= 5) //basamak kontrol ediliyor
                {
                    string yaziyaCevir(decimal tutar)
                    {
                        string stringTutar = tutar.ToString("F2").Replace('.', ',');            
                        string TL = stringTutar.Substring(0, stringTutar.IndexOf(',')); //virgülün sol kısmı
                        string kurus = stringTutar.Substring(stringTutar.IndexOf(',') + 1, 2); //virgülün sağ kısmı
                        string yazi = "";

                        string[] birlerbasamagı = { "", " Bir", " İki", " Üç", " Dört", " Beş", " Altı", " Yedi", " Sekiz", " Dokuz" };
                        string[] onlarbasamagı = { "", " On", " Yirmi", " Otuz", " Kırk", " Elli", " Altmış", " Yetmiş", " Seksen", " Doksan" };
                        string[] binlerbasamagı = {  " Bin", "" };

                        int grupSayi = 2; 
                                            

                        TL = TL.PadLeft(grupSayi * 3, '0'); //sayının soluna '0' ekleniyor ve 'grup sayısı x 3' basamaklı yapılıyor.            

                        string grupDeger;

                        for (int i = 0; i < grupSayi * 3; i += 3) //sayı 3'erli gruplara ayrılıp çalıştırılıyor.
                        { 
                            grupDeger = "";

                            if (TL.Substring(i, 1) != "0")
                                grupDeger += birlerbasamagı[Convert.ToInt32(TL.Substring(i, 1))] + " Yüz"; //yüzlerbasamağı                

                            if (grupDeger == " Bir Yüz") //biryüz yazmaması için.
                                grupDeger = " Yüz";

                            grupDeger += onlarbasamagı[Convert.ToInt32(TL.Substring(i + 1, 1))]; //onlarbasamağı 

                            grupDeger += birlerbasamagı[Convert.ToInt32(TL.Substring(i + 2, 1))]; //birlerbasamağı                

                            if (grupDeger != "") //binlerbasamağı
                                grupDeger += binlerbasamagı[i / 3];

                            if (grupDeger == " Bir Bin") //birbin yazmaması için.
                                grupDeger = " Bin";

                            yazi += grupDeger;
                        }

                        if (yazi != "")
                            yazi += " TL ";

                        int yazininUzunlugu = yazi.Length;

                        if (kurus.Substring(0, 1) != "0") //onlar Kuruş için
                            yazi += onlarbasamagı[Convert.ToInt32(kurus.Substring(0, 1))];

                        if (kurus.Substring(1, 1) != "0") //birler Kuruş için
                            yazi += birlerbasamagı[Convert.ToInt32(kurus.Substring(1, 1))];

                        if (yazi.Length > yazininUzunlugu)
                            yazi += " Kuruş";
                        else
                            yazi += "Sıfır Kuruş";




                        return yazi;
                    }

                    lblYazi.Text = yaziyaCevir(Convert.ToDecimal(txtSayi.Text));
                }
                else
                {
                    MessageBox.Show("Sayı 5 basamaktan fazla olamaz.");
                }
            }

            if(virgulKontrol!=-1) // virgül olduğunda
            {
                if (txtSayi.Text.Length <= 8)//basamak kontrolü
                {
                    string yaziyaCevir(decimal tutar)
                    {
                        string stringTutar = tutar.ToString("F2").Replace('.', ','); // Replace('.',',') ondalık ayracının . olma durumu için            
                        string TL = stringTutar.Substring(0, stringTutar.IndexOf(',')); //virgülün sol kısmı 
                        string kurus = stringTutar.Substring(stringTutar.IndexOf(',') + 1, 2); // virgülün sağ kısmı
                        string yazi = "";

                        string[] birlerbasamagı = { "", " Bir", " İki", " Üç", " Dört", " Beş", " Altı", " Yedi", " Sekiz", " Dokuz" };
                        string[] onlarbasamagı = { "", " On", " Yirmi", " Otuz", " Kırk", " Elli", " Altmış", " Yetmiş", " Seksen", " Doksan" };
                        string[] binlerbasamagı = {  " Bin", "" }; 

                        int grupSayi = 2; 
                                           

                        TL = TL.PadLeft(grupSayi * 3, '0'); //sayının soluna '0' ekleniyor ve 'grup sayısı x 3' basamaklı yapılıyor.            

                        string grupDeger;

                        for (int i = 0; i < grupSayi * 3; i += 3) //sayı 3'erli gruplara ayrılıp çalıştırılıyor.
                        {
                            grupDeger = "";

                            if (TL.Substring(i, 1) != "0")
                                grupDeger += birlerbasamagı[Convert.ToInt32(TL.Substring(i, 1))] + " Yüz"; //yüzlerbasamağı                

                            if (grupDeger == " Bir Yüz") //biryüz düzeltiliyor.
                                grupDeger = " Yüz";

                            grupDeger += onlarbasamagı[Convert.ToInt32(TL.Substring(i + 1, 1))]; //onlarbasamağı 

                            grupDeger += birlerbasamagı[Convert.ToInt32(TL.Substring(i + 2, 1))]; //birlerbasamağı                

                            if (grupDeger != "") //binlerbasamağı
                                grupDeger += binlerbasamagı[i / 3];

                            if (grupDeger == " Bir Bin") //birbin düzeltiliyor.
                                grupDeger = " Bin";

                            yazi += grupDeger;
                        }

                        if (yazi != "")
                            yazi += " TL ";

                        int yazininUzunlugu = yazi.Length;

                        if (kurus.Substring(0, 1) != "0") //onlar Kuruş için
                            yazi += onlarbasamagı[Convert.ToInt32(kurus.Substring(0, 1))];

                        if (kurus.Substring(1, 1) != "0") //birler Kuruş için
                            yazi += birlerbasamagı[Convert.ToInt32(kurus.Substring(1, 1))];

                        if (yazi.Length > yazininUzunlugu)
                            yazi += " Kuruş";
                        else
                            yazi += "Sıfır Kuruş";




                        return yazi;
                    }

                    

                    lblYazi.Text = yaziyaCevir(Convert.ToDecimal(txtSayi.Text));

                }
                else
                {
                    MessageBox.Show("Sayı 5 basamaktan fazla olamaz.");
                }

            }
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
