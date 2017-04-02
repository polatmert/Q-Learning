using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


//HÜSEYİN EMRE ÖZHAN -140201062
//HÜSNÜ MERT POLAT   -140201001

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        StreamReader sr; 
        int OdaSayisi ;
        int OdaSayisiKoku;


        int[,] Rmatrisi;
        double[,] Qmatrisi;
        int[] ENKISAYOL;

        int START = 0;
        int FINISH = 0;
        int ITERASYON = 100;


        public void DENEME(String denenecek)
        {
            listBox1.Items.Add(denenecek);
        }
        
        
        //Gonderilen oda numarasının gidebileği odalarla arasındaki Max olanı bulum döndürmeli
     public int MaxQ(int MaxQBulunacakOda,int yasakOda )
        {
          

            double MaxQDegeri = 0;
            double mevcutDeger=0;
            int MaxQIndis = 0;
            int sonraki=0;

            for (sonraki = 0; sonraki < OdaSayisi; sonraki++)
            {
                mevcutDeger=Rmatrisi[MaxQBulunacakOda, sonraki];
               
               if (mevcutDeger != (-1) && sonraki != MaxQBulunacakOda && sonraki!=yasakOda)
                {

                    if(mevcutDeger >= MaxQDegeri )
                    {
                        MaxQDegeri = mevcutDeger;
                        MaxQIndis = sonraki;
                    }
                    
                }
            }

            return MaxQIndis;
        }

        public void ENKISAYOLUHESAPLA()
        { 
            int []GeciciPath=new int [OdaSayisi];
            int pathSayac = 0;
            //STARTI  ekrana yazdır
            ///STARTDAN başla Q matrisini START Satırının elemanlarını tara en yüksek elemanın Sutun numarasını al YAZDIR o numara FINISH mı ona bak fınıshise bitir
            /// buldugun  numarayı Q matrisinin satırına atla o satırdaki en büyük elemanı bul sutun numarasını al YAZDIR o numara FINISH mı ona bak
            DENEME("Baslangıc-"+START.ToString());
            double EnBuyuk = 0;
            int EnBuyukIndis = 0;
            int sayac=0;
            int satir = START;
            

            for (int dongu = 0; dongu < OdaSayisi;dongu++ )
            {
                for (sayac = 0; sayac < OdaSayisi; sayac++)
                {
                    if ( Qmatrisi[satir, sayac] >= EnBuyuk)
                    {
                        EnBuyuk = Qmatrisi[satir, sayac];
                        EnBuyukIndis = sayac;
                    }

                }

                GeciciPath[pathSayac] = EnBuyukIndis;
                pathSayac++;
                DENEME(EnBuyukIndis.ToString());//listboxa yazdırma

                
              
                satir = EnBuyukIndis;

                if (satir == FINISH) 
                    break;

            }
            //Enkısayol pathi gecici diziden kopyalanarak  ENKISAYOL dizisi elde edilir
            ENKISAYOL = new int[pathSayac+1];
            ENKISAYOL[0] = START;
            for(int jj=0;jj<pathSayac;jj++)
            {
                ENKISAYOL[jj+1] = GeciciPath[jj];
            }




        }

        
        public void ENKISAYOLUCIZ(Graphics g)
        {
           Pen pn =new Pen(Color.Red);
           pn.Width = 5;
            int X1, X2, Y1, Y2;
            int sayac=0;

            while(sayac<ENKISAYOL.GetUpperBound(0))
            { 
                X1 = (ENKISAYOL[sayac] % OdaSayisiKoku) * 40 + 20;
                Y1 = (ENKISAYOL[sayac] / OdaSayisiKoku) * 40 + 20;



                X2 = (ENKISAYOL[sayac+1] % OdaSayisiKoku) * 40 + 20;
                Y2 = (ENKISAYOL[sayac+1] / OdaSayisiKoku) * 40 + 20;

              
                g.DrawLine(pn, X1, Y1, X2, Y2);
                sayac++;
                
            }
        }




        public Form1()
        {
            InitializeComponent();
        }

        public void DikBeyazCizgi(Graphics g,int CizYY,int CizXX)
        {g.DrawLine(new Pen(Color.Transparent,3), 40*CizXX ,40*CizYY ,40*CizXX ,40*CizYY+40);}
        
        public void DikSiyahCizgi(Graphics g, int CizYY, int CizXX)
        {g.DrawLine(new Pen(Color.Black,3), 40 * CizXX, 40 * CizYY, 40 * CizXX, 40 * CizYY + 40); }
        
        public void YataySiyahCizgi(Graphics g, int CizYY, int CizXX)
        { g.DrawLine(new Pen(Color.Black,3), 40 * CizXX, 40 * CizYY, 40 * CizXX + 40, 40 * CizYY);}
        
        public void YatayMaviCizgi(Graphics g,int CizYY,int CizXX)
        {g.DrawLine(new Pen(Color.Transparent,3), 40 * CizXX, 40 * CizYY, 40 * CizXX  +40 ,  40 * CizYY);}

      
        private void button1_Click(object sender, EventArgs e)
        {
          
            //picture box çizgi cizme deneme
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g=Graphics.FromImage(bmp);
            
               
                g.DrawRectangle(new Pen(Color.Black,3), 0, 0, 40 * OdaSayisiKoku, 40 * OdaSayisiKoku);
                //Grid Oluşturma

                //Dikey boşlukları oluşturma
                for (int oda = 0; oda < OdaSayisi-1; oda++)
                {
                    if (Rmatrisi[oda, oda + 1] == 0 || Rmatrisi[oda, oda + 1] == 100)
                    {
                        DikBeyazCizgi(g, oda/OdaSayisiKoku, (oda+1)%OdaSayisiKoku);
                    }
                    else
                       DikSiyahCizgi(g, oda / OdaSayisiKoku, (oda + 1) % OdaSayisiKoku);
                }


                //Yatay Boşluklar oluşturma
                for (int oda = 0; oda < OdaSayisi - OdaSayisiKoku; oda++)
                {
                    if (Rmatrisi[oda, oda + OdaSayisiKoku] == 0 || Rmatrisi[oda, oda + OdaSayisiKoku] == 100)
                    {
                        YatayMaviCizgi(g, (oda / OdaSayisiKoku)+1, (oda) % OdaSayisiKoku);
                    }
                    else
                        YataySiyahCizgi(g, (oda / OdaSayisiKoku) + 1, (oda) % OdaSayisiKoku);
                }
            
            
            //EN KISA YOLU ÇİZİYORUZ
            ENKISAYOLUCIZ(g);


            pictureBox1.Image = bmp;
        }





        private void Hesapla_Click(object sender, EventArgs e)
        {
            
            ///Numericlerden  Başlangıç ve Bitiş konumlarını ve Iterasyon Sayısı Alma 
            START = Convert.ToInt32(numericUpDown1.Value);
            FINISH = Convert.ToInt32(numericUpDown2.Value);
            ITERASYON = Convert.ToInt32(numericUpDown3.Value);

            
            if (START == 0 &&( FINISH == 0 || ITERASYON == 0))
            {
                MessageBox.Show("Lütfen Değerleri Belirleyiniz");
            }
            else
            {



                sr = new StreamReader("input.txt");
                OdaSayisi = File.ReadLines("input.txt").Count();
                OdaSayisiKoku = Convert.ToInt32(Math.Sqrt(Convert.ToDouble(OdaSayisi)));

                String dummy = OdaSayisi.ToString();
                DENEME("satir sayi " + dummy);

                Rmatrisi = new int[OdaSayisi, OdaSayisi];
                Qmatrisi = new double[OdaSayisi, OdaSayisi];


                ///R matrisinin elemanlarını başlangıç için -1 yapıyoruz
                ///geçiş olanları daha sonra 0'a çeviricez
                int ii = 0, jj = 0;
                for (ii = 0; ii < OdaSayisi; ii++)
                {
                    for (jj = 0; jj < OdaSayisi; jj++)
                    { Rmatrisi[ii, jj] = -1; }
                }
                ///Qmatrisini 0'lıyoruz
                for (ii = 0; ii < OdaSayisi; ii++)
                {
                    for (jj = 0; jj < OdaSayisi; jj++)
                    { Qmatrisi[ii, jj] = 0; }
                }



                for (int ii2 = 0; ii2 < OdaSayisi; ii2++)
                {
                    String OkunanDeger = sr.ReadLine();
                    //DENEME("Okunan Satir="+OkunanDeger);
                    String[] Parcalanmis = OkunanDeger.Split(',');//okunan satırı virgullere göre parçalıyor

                    for (int jj2 = 0; jj2 < Parcalanmis.GetUpperBound(0) + 1; jj2++)
                    {
                        int Degistirilecek = Convert.ToInt32(Parcalanmis[jj2]);

                        if (Degistirilecek == FINISH)
                            Rmatrisi[ii2, Degistirilecek] = 100;
                        else
                            Rmatrisi[ii2, Degistirilecek] = 0;
                    }
                }
                Rmatrisi[FINISH, FINISH] = 100;//hedefin kendine dönüşü 100kazançlıdır
                //R matrisi oluşturuldu

                int sayac = 0;
                int durum = START;
                int aksiyon = 0;

                Random rnd = new Random();


                for (sayac = 0; sayac < ITERASYON; sayac++)
                {
                    //aksiyonu belirlemek için
                    //  for (int kk = 0; kk < OdaSayisi; kk++)
                    //    if (Rmatrisi[START, kk] != -1)
                    //      aksiyon = kk;
                    //aksiyonu belirlemek için
                    while (true)
                    {
                        aksiyon = rnd.Next(0, OdaSayisi);
                        if (Rmatrisi[START, aksiyon] != -1)
                            break;
                    }

                    durum = START;

                    int arasayac = 0;
                    while (durum != FINISH && arasayac<OdaSayisi)
                    {
                        int sonrakiAksiyon = MaxQ(aksiyon, durum);
                        Qmatrisi[durum, aksiyon] = Rmatrisi[durum, aksiyon] + 0.8 * Qmatrisi[aksiyon, sonrakiAksiyon];

                        DENEME(durum.ToString() + " " + aksiyon.ToString());

                        durum = aksiyon;
                        aksiyon = sonrakiAksiyon;

                        arasayac++;
                    }
                    DENEME("BİR ITERASYON BİTTİ");
                }


                DENEME("enkısa yol___");
                ENKISAYOLUHESAPLA();

                



                DENEME("bitti");
            }
            button1.Enabled = true;

            OutputDosyasıOlusturma();
        }

        public void OutputDosyasıOlusturma()
        {

            //Eğer dosya daha önceden yapılmış ile kontrol eder ve siler 
            if (File.Exists("Routput.txt"))
            { File.Delete("Routput.txt"); }

            using (System.IO.StreamWriter file = new System.IO.StreamWriter("Routput.txt", true))
            {
                
                file.Write("R Matrisi:\n");
                int satır=0,sutun=0;
                for (satır = 0; satır < OdaSayisi ;satır++ )
                {
                    for (sutun = 0; sutun < OdaSayisi; sutun++)
                    {
                        file.Write(Rmatrisi[satır,sutun]);
                        file.Write("    ");
                    }
                    file.WriteLine("\n");

                }

      
            }

             if (File.Exists("Qoutput.txt"))
            { File.Delete("Qoutput.txt"); }

            using (System.IO.StreamWriter file = new System.IO.StreamWriter("Qoutput.txt", true))
            {
                file.Write("Q Matrisi:\n");
                int satır = 0, sutun = 0;
                for (satır = 0; satır < OdaSayisi; satır++)
                {
                    for (sutun = 0; sutun < OdaSayisi; sutun++)
                    {
                        file.Write(Qmatrisi[satır, sutun]);
                        file.Write("    ");
                    }
                    file.WriteLine("\n");
                }

            }


             if (File.Exists("Path.txt"))
            { File.Delete("Path.txt"); }

             using (System.IO.StreamWriter file = new System.IO.StreamWriter("Path.txt", true))
             {
                 file.Write("En Kısa Yol \n");
                 int sayac=0;
                for(sayac=0;sayac < ENKISAYOL.GetUpperBound(0)+1;sayac++ )
                {
                    file.Write(ENKISAYOL[sayac]);
                    file.Write("    ");
                }

             
             }



        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
                listBox1.Visible = true;
            else
                listBox1.Visible = false;

        }

       

    }
    
}
