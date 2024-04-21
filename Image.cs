using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace pbscientfique
{
    public class Image
    {
        private Pixel[,] image;
        private byte[] header;
        private byte[] headerio;
        private int largeur;
        private int hauteur;
        private int offset;
        private int fichtaille;

        public Image(string file)
        {
            byte[] fichier = File.ReadAllBytes(file);
            this.header = new byte[14];
            this.headerio = new byte[40];
            byte[] taillefich = { fichier[2], fichier[3], fichier[4], fichier[5] };
            this.fichtaille = Convertir_Endian_To_Int(taillefich);


            for (int i = 0; i < 14; i++)
                header[i] = fichier[i];

            for (int i = 14; i < 54; i++)
                headerio[i - 14] = fichier[i];


            byte[] blargeur = { fichier[18], fichier[19], fichier[20], fichier[21] };
            byte[] bhauteur = { fichier[22], fichier[23], fichier[24], fichier[25] };
            byte[] boff = { fichier[10], fichier[11], fichier[12], fichier[13] };

            this.offset = Convertir_Endian_To_Int(boff);
            this.hauteur = Convertir_Endian_To_Int(bhauteur);
            this.largeur = Convertir_Endian_To_Int(blargeur);
            image = new Pixel[hauteur, largeur];
            int k = 54;
            for (int i = 0; i < hauteur; i++)
            {
                for (int j = 0; j < largeur; j++)
                {
                    image[i, j] = new Pixel(fichier[k], fichier[k + 1], fichier[k + 2]);
                    k += 3;

                }
            }
        }


        public void From_Image_To_File(string file)
        {
            
            for (int i = 0; i < 4; i++)
            {
                header[i + 2] = Convertir_Int_To_Endian(fichtaille)[i];
                header[i+10] = Convertir_Int_To_Endian(offset)[i];
                headerio[i + 4] = Convertir_Int_To_Endian(largeur)[i];
                headerio[i + 8] = Convertir_Int_To_Endian(hauteur)[i];
                headerio[i + 20] = Convertir_Int_To_Endian(fichtaille - 54)[i];
            }
            byte[] rep = isconform();

            File.WriteAllBytes(file, rep);
        }
        public byte[] isconform()
        {
            byte[] rep = new byte[54];
            

            for (int i = 0; i < 14; i++)
                rep[i] = header[i];

            for (int i = 14; i < 54; i++)
                rep[i] = headerio[i - 14];

            int k = 0;
            int nbbyte = largeur * 3;
            while (nbbyte % 4 != 0)
            {
                nbbyte++;
            }
            nbbyte = nbbyte - largeur * 3;
            byte[] mat = new byte[image.Length*3+nbbyte*hauteur];

            for (int i = 0; i < image.GetLength(0); i++)
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    mat[k] = image[i, j].Rouge;
                    mat[k + 1] = image[i, j].Vert;
                    mat[k + 2] = image[i, j].Bleu;
                    k += 3;
                }
                for (int l = 0; l < nbbyte; l++)
                {
                    mat[k] = 0;
                    k++;
                }

            }
            rep = Concatenerbyte(rep, mat);
            return rep;
        }

        public byte[] Concatenerbyte(byte[] b1, byte[] b2)
        {
            byte[] newbyte = new byte[b1.Length + b2.Length];
            int i = 0;
            foreach (byte b in b1)
            {
                newbyte[i] = b;
                i++;
            }
            foreach (byte b in b2)
            {
                newbyte[i] = b;
                i++;
            }
            return newbyte;
        }
        public string toString()
        {
            string rep = "";
            for (int i = 0; i < image.GetLength(0); i++)
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                        rep += this.image[i, j].toString();
                }
                rep += "\n";
            }
            return rep;
        }


        // Cette fonction prend en entrée un tableau de bytes, un index de départ et une longueur.
       public int Convertir_Endian_To_Int(byte[] tab)
  {
      return BitConverter.ToInt32(tab, 0);
  }
  public byte[] Convertir_Int_To_Endian(int val)
  {
      return BitConverter.GetBytes(val);
  )

        public void rotation90()
        {

            Pixel[,] rot90im = new Pixel[hauteur, largeur];
            for (int i = 0; i < hauteur; i++)
            {
                for (int j = 0; j < largeur; j++)
                {
                    rot90im[i, j] = image[hauteur - j - 1, i];
                }
            }
            image = rot90im;

        }
        public void rotationim(int angle)
        {
            while (angle % 90 != 0)
            {
                Console.WriteLine("mauvais angle");
                angle = int.Parse(Console.ReadLine());
            }
            if (angle == 0 || angle == 360)
            {

            }
            else if (angle == 90)
            {
                rotation90();
            }
            else if (angle == 180)
            {
                rotation90();
                rotation90();
            }
            else if (angle == 270)
            {
                rotation90();
                rotation90();
                rotation90();
            }



        }

        public void rotalpha(double angle)
        {
            double rad = (angle * Math.PI) / 180;
            int maxLargeur = (int)Math.Ceiling(Math.Abs(largeur * Math.Cos(rad)) + Math.Abs(hauteur * Math.Sin(rad)));
            int maxHauteur = (int)Math.Ceiling(Math.Abs(largeur * Math.Sin(rad)) + Math.Abs(hauteur * Math.Cos(rad)));
            Pixel[,] rep = new Pixel[maxHauteur, maxLargeur];
            

            int x = 0, y = 0;
            for (int i = 0; i < hauteur; i++)
            {
                for (int j = 0; j < largeur; j++)
                {
                    x = (int)Math.Round((j - hauteur / 2) * Math.Cos(rad) - (i - largeur / 2) * Math.Sin(rad) + hauteur / 2);
                    y = (int)Math.Round((j - hauteur / 2) * Math.Sin(rad) + (i - largeur / 2) * Math.Cos(rad) + largeur / 2);
                    if (x >= 0 && x < maxHauteur && y >= 0 && y < maxLargeur)
                    {
                        rep[x, y] = image[i, j];
                    }
                }
            }

            hauteur = maxHauteur;
            largeur = maxLargeur;
            fichtaille = 3*(maxLargeur * maxHauteur)+54;
            

            for (int i = 0; i < hauteur; i++)
            {
                for (int j = 0; j < largeur; j++)
                {
                    if (rep[i, j] == null)
                    {
                        rep[i, j] = Pixelvoisin(rep, i, j);

                    }
                }
            }

            for (int i = 0; i < hauteur; i++)
            {
                for (int j = 0; j < largeur; j++)
                {
                    if (rep[i, j] == null)
                    {
                        rep[i, j] = new Pixel(0, 0, 0);

                    }
                }
            }


            image = rep;

        }


        public Pixel Pixelvoisin(Pixel[,] pixels, int i, int j)
        {
            List<Pixel> rep = new List<Pixel>();
            for (int k = 0; k <= 1; k++)
            {
                for (int l = 0; l <= 1; l++)
                {
                    if (i > 1 && i < pixels.GetLength(0) - 1 && j > 1 && j < pixels.GetLength(1) - 1)
                    {
                        if (pixels[i + k, j + l] != null)
                        {
                            rep.Add(pixels[i + k, j + l]);
                        }
                        if (pixels[i - k, j + l] != null)
                        {
                            rep.Add(pixels[i - k, j + l]);
                        }
                        if (pixels[i + k, j - l] != null)
                        {
                            rep.Add(pixels[i + k, j - l]);
                        }
                        if (pixels[i - k, j - l] != null)
                        {
                            rep.Add(pixels[i - k, j - l]);
                        }
                    }
                }
            }
            Random r = new Random();
            Pixel pix = new Pixel(0, 0, 0);
            if (rep.Count != 0)
            {
                pix = rep[r.Next(rep.Count)];
            }



            return pix;
        }

        
        public void appfiltres(int[,] filtre)
        {
            int filtrehauteur = filtre.GetLength(0);
            int filtrelargeur = filtre.GetLength(1);

            Pixel[,] result = new Pixel[hauteur, largeur];

            for (int y = 0; y < hauteur; y++)
            {
                for (int x = 0; x < largeur; x++)
                {
                    int sumR = 0;
                    int sumG = 0;
                    int sumB = 0;

                    for (int fy = 0; fy < filtrehauteur; fy++)
                    {
                        for (int fx = 0; fx < filtrelargeur; fx++)
                        {
                            int imageX = x - filtrelargeur / 2 + fx;
                            int imageY = y - filtrehauteur / 2 + fy;

                            if (imageX >= 0 && imageX < largeur && imageY >= 0 && imageY < hauteur)
                            {
                                Pixel Pixel = image[imageY, imageX];
                                int filtreVal = filtre[fy, fx];

                                sumR += Pixel.Rouge * filtreVal;
                                sumG += Pixel.Vert * filtreVal;
                                sumB += Pixel.Bleu * filtreVal;
                            }
                        }
                    }

                    byte newR = (byte)Math.Max(0, Math.Min(sumR, 255));
                    byte newG = (byte)Math.Max(0, Math.Min(sumG, 255));
                    byte newB = (byte)Math.Max(0, Math.Min(sumB, 255));
                    result[y, x] = new Pixel(newR, newG, newB);
                }
            }

            image = result;
        }

        public void filtre()
        {
            int[,] filtre =
            {
                  { 0,  0,  -1,  0,  0 },
                  { 0,  0,  -1,  0,  0 },
                  { 0,  0,  2,  0,  0 },
                  { 0,  0,  0,  0,  0 },
                  { 0,  0,  0,  0,  0 }
             };
            int[,] filtre1 =
            {
                  { 0,  1,  0},
                  { 1,  1,  1},
                  { 0,  1,  0},
             };
            int[,] filtre2 =
            {
                  { -1,  -1, -1},
                  { -1,  9, -1},
                  { -1,  -1,  1}
             };

            appfiltres(filtre2);
        }

        public double[,] Prodmat(double[,] A, double[,] B)
        {
            double[,] C = null;
            if (A.GetLength(1) != B.GetLength(0)) { }
            else
            {
                C = new double[A.GetLength(0), B.GetLength(1)];

                for (int i = 0; i < A.GetLength(0); i++)
                {
                    for (int j = 0; j < B.GetLength(1); j++)
                    {
                        C[i, j] = 0;
                        for (int k = 0; k < A.GetLength(1); k++)
                        {
                            C[i, j] += A[i, k] * B[k, j];
                        }
                    }
                }
            }
            return C;
        }

        public void Agrandissement(int c)
        {
            hauteur = hauteur * c;
            largeur = largeur * c;
            Pixel[,] rep = new Pixel[hauteur,largeur];

            for(int i = 0;i < rep.GetLength(0); i++)
            {
                for(int j=0; j < rep.GetLength(1); j++)
                {
                    rep[i,j] = image[i/c,j/c];
                }
            }
            image = rep;
        } // a continuer

    
   

    }
}
