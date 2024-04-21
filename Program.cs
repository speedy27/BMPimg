using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace pbscientfique
{
    internal class Program
    {
        static void Main(string[] args)
        {

            
            //Console.WriteLine(im.toString());
            Process.Start("coco.bmp");

            for(int i = 0; i <= 100; i = i + 10)
            {
                Image im = new Image("coco.bmp");
                im.rotalpha(i);

                im.From_Image_To_File("test00"+i+".bmp");
                Process.Start("test00"+i+".bmp");
            }
            

            




            Console.ReadKey();





            /*int[,] mat = new int[50,50];
            for (int i = 0; i < mat.GetLength(0); i++)
            {
                for (int j = 0; j < mat.GetLength(1); j++)
                {
                    if (i == j)
                    {
                        mat[i,j] = 1;
                    }
                    else
                    {
                        mat[i, j] = 0;
                    }
                    Console.Write(mat[i, j]+ " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\n\n\n");

            double rad = (40 * Math.PI) / 180;
            int maxLargeur = (int)Math.Ceiling(Math.Abs(50 * Math.Cos(rad)) + Math.Abs(50 * Math.Sin(rad)));
            int maxHauteur = (int)Math.Ceiling(Math.Abs(50 * Math.Sin(rad)) + Math.Abs(50 * Math.Cos(rad)));
            int[,] rep = new int[maxHauteur, maxLargeur];

            int x = 0, y = 0;
            int cX = 50 / 2, cY = 50 / 2;
            for (int i = 0; i < 50; i++)
            {
                for (int j = 0; j < 50; j++)
                {

                    if(i-cY < 0 && j - cX < 0) { }
                    if(i - cY > 0 && j - cX < 0) { }
                    if (i - cY < 0 && j - cX > 0) { }
                    if (i - cY > 0 && j - cX > 0) { }
                    x = (int)Math.Round((j-cX) * Math.Cos(rad) - (i - cY) * Math.Sin(rad)+cX);
                    y = (int)Math.Round((j - cX) * Math.Sin(rad) + (i-cY) * Math.Cos(rad)+cY);
                    if (x >= 0 && x < maxHauteur && y >= 0 && y < maxLargeur)
                    {
                        rep[y, x] = mat[i, j];
                    }
                }
            }





            for (int i = 0; i < mat.GetLength(0); i++)
            {
                for (int j = 0; j < mat.GetLength(1); j++)
                {
                    Console.Write(rep[i, j] + " ");
                }
                Console.WriteLine();
            }





            */

            Console.ReadKey();




        }
    }
}
