using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anaglyfy
{
    abstract class Anaglifyoperation:lab01biometria.Visitor
    {
        public void  rob(lab01biometria.image_as_tab image)
        {
            image.Accept(this);
            
            

        }
        public void   Visit(lab01biometria.image_RGB rgb)
        {
            doAnaglfy(rgb);
            
        }

        public void  Visit(lab01biometria.image_Gray Grey)
        {
           
        }
        public float[,] wskaznik = new float[3, 3];
        
        public void doAnaglfy(lab01biometria.image_RGB rgb)
        {

            List<int[,]> ListNewp = new List<int[,]>();
            ListNewp.Add(copyImage(rgb));
            ListNewp.Add(copyImage(rgb));
            ListNewp.Add(copyImage(rgb));



            Parallel.For(0, 3, t =>
            {

                for (int i = 0; i < rgb.w; i++)
                {

                    for (int j = 0; j < rgb.h; j++)
                    {
                        ListNewp.ElementAt(t)[i, j] = (int)Math.Round(rgb.R[i][j] * wskaznik[t, 0]
                                        + rgb.G[i][j] * wskaznik[t, 1]
                                        + rgb.B[i][j] * wskaznik[t, 2]);

                    }


                }
            });
            tab2int(ListNewp.ElementAt(0), ListNewp.ElementAt(1), ListNewp.ElementAt(2), rgb);

        }
        public lab01biometria.image_RGB addImagesAnaglfy(lab01biometria.image_RGB rgbL, lab01biometria.image_RGB rgbP)
        {
            lab01biometria.image_as_tab o = rgbL.copy();
            lab01biometria.image_RGB orginal = new lab01biometria.image_RGB(o.utab, o.w, o.h);

            Parallel.For (0,rgbL.w,i=>
            {

                for (int j = 0; j < rgbL.h; j++)
                {
                    orginal.R[i][j] =(byte) (rgbL.R[i][j] + rgbP.R[i][j]);
                    orginal.G[i][j] = (byte)(rgbL.G[i][j] + rgbP.G[i][j]);
                    orginal.B[i][j] = (byte)(rgbL.B[i][j] + rgbP.B[i][j]);

                }


            });
            return orginal;

    }

        public static int[,] copyImage(lab01biometria.image_RGB rgb)
        {

            int[,] Temp = new int[rgb.w, rgb.h];
            return Temp;
        }

        public static void tab2int(int[,] R, int[,] G, int[,] B, lab01biometria.image_RGB rgb)
        {
            for (int c = 0; c < rgb.w; c++)
            {
                for (int p = 0; p < rgb.h; p++)
                {
                    rgb.R[c][p] = (byte)R[c, p];
                    rgb.G[c][p] = (byte)G[c, p];
                    rgb.B[c][p] = (byte)B[c, p];
                }


            }


        }
        public  static void chackafterdubois(int[,] rgb, int w, int h)
        {
            Parallel.For(0, w, i =>
            {

                for (int j = 0; j <h; j++)
                {

                    rgb[i, j] = (byte)(rgb[i, j] > 255 ? 255 : rgb[i, j]);

                    rgb[i, j] = (byte)(rgb[i, j] < 0 ? 0 : rgb[i, j]);
                   

                }


            });
        }


    }
}