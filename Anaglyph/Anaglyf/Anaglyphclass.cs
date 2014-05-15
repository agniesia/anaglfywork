using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anaglyfy
{
    class TrueAnaglyphsLeft : Anaglifyoperation
    {
        public TrueAnaglyphsLeft()
        {
            wskaznik = new float[,] { { 0.299F, 0.587F, 0.114F }, { 0F, 0F, 0F }, { 0F, 0F, 0F } };
        }
    }
    class TrueAnaglyphsRight : Anaglifyoperation
    {
        public TrueAnaglyphsRight()
        {
            wskaznik = new float[,] { { 0F, 0F, 0F }, { 0F, 0F, 0F }, { 0.299F, 0.587F, 0.114F } };
        }
    }
    class GrayAnaglyphsLeft : Anaglifyoperation
    {
        public GrayAnaglyphsLeft()
        {
            wskaznik = new float[,] { { 0.299F, 0.587F, 0.114F }, { 0F, 0F, 0F }, { 0F, 0F, 0F } };
        }
    }
    class GrayAnaglyphsRight : Anaglifyoperation
    {
        public GrayAnaglyphsRight()
        {
            wskaznik = new float[,] { { 0F, 0F, 0F }, { 0.299F, 0.587F, 0.114F }, { 0.299F, 0.587F, 0.114F } }; 
        }
    }
    class ColorAnaglyphsLeft : Anaglifyoperation
    {
        public ColorAnaglyphsLeft()
        {
            wskaznik = new float[,] { { 1F, 0F, 0F }, { 0F, 0F, 0F }, { 0F, 0F, 0F } }; 
        }
    }
    class ColorAnaglyphsRight : Anaglifyoperation
    {
        public ColorAnaglyphsRight()
        {
            wskaznik = new float[,] { { 0F, 0F, 0F }, { 0F, 1F, 0F }, { 0F, 0F, 1F } }; 
        }
    }
    class HalfColorAnaglyphsLeft : Anaglifyoperation
    {
        public HalfColorAnaglyphsLeft()
        {
            wskaznik = new float[,] { { 0.299F, 0.587F, 0.114F }, { 0F, 0F, 0F }, { 0F, 0F, 0F } };
        }
    }
    class HalfColorAnaglyphsRight : Anaglifyoperation
    {
        public HalfColorAnaglyphsRight()
        {
            wskaznik = new float[,] { { 0F, 0F, 0F }, { 0F, 1F, 0F }, { 0F, 0F, 1F } }; 
        }
    }
    class DuboisLeft :lab01biometria.Visitor
    {
        float[,] wskaznik;
        public DuboisLeft()
        {
            wskaznik = new float[,] { { 0.4561F, 0.500484F, 0.176381F }, { -0.0400822F, -0.0378246F, -0.0157589F }, { -0.0152161F,-0.0205971F,  - 0.00546856F } }; 

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


    }
    class DuboisRight : lab01biometria.Visitor
    {
        float[,] wskaznik;
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

    }
    class OptimizedAnaglyphsRight: Anaglifyoperation
    {
        public OptimizedAnaglyphsRight()
        {
            wskaznik = new float[,] { { 0F, 0.7F, 0.3F }, { 0F, 0F, 0F }, { 0F, 0F, 0F } }; 

        }


    }
    class OptimizedAnaglyphsLeft : Anaglifyoperation
    {
        public OptimizedAnaglyphsLeft()
        {
            wskaznik = new float[,] { { 0F, 0F, 0F }, { 0F, 1F, 0F }, { 0F, 0F, 1F } }; 
        }
    }
    
}
