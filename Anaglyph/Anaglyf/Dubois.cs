using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anaglyph.Anaglyf
{

    class DuboisRight : lab01biometria.Visitor
    {
        protected float[,] wskaznik;
        public DuboisRight()
        {
            wskaznik = new float[,] { { -0.0434706F, -0.0879388F, -0.00155529F }, { 0.378476F, 0.73364F, -0.0184503F }, { -0.0721527F, -0.112961F, 1.2264F } };

        }
        public void rob(lab01biometria.image_as_tab image)
        {
            image.Accept(this);



        }
        public void Visit(lab01biometria.image_RGB rgb)
        {
            doAnaglfy(rgb);

        }

        public void Visit(lab01biometria.image_Gray Grey)
        {

        }
        public void doAnaglfy(lab01biometria.image_RGB rgb)
        {

            List<int[,]> ListNewp = new List<int[,]>();
            ListNewp.Add(Anaglyfy.Anaglifyoperation.copyImage(rgb));
            ListNewp.Add(Anaglyfy.Anaglifyoperation.copyImage(rgb));
            ListNewp.Add(Anaglyfy.Anaglifyoperation.copyImage(rgb));



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
            Anaglyfy.Anaglifyoperation.chackafterdubois(ListNewp.ElementAt(0),rgb.w,rgb.h);
            Anaglyfy.Anaglifyoperation.chackafterdubois(ListNewp.ElementAt(1),rgb.w,rgb.h);
             Anaglyfy.Anaglifyoperation.chackafterdubois(ListNewp.ElementAt(2),rgb.w,rgb.h);
            Anaglyfy.Anaglifyoperation.tab2int(ListNewp.ElementAt(0), ListNewp.ElementAt(1), ListNewp.ElementAt(2), rgb);

        }

    }
    class DuboisLeft : DuboisRight
    {
       
        public DuboisLeft()
        {
            wskaznik = new float[,] { { 0.4561F, 0.500484F, 0.176381F }, { -0.0400822F, -0.0378246F, -0.0157589F }, { -0.0152161F, -0.0205971F, -0.00546856F } };

        }




    }
}
