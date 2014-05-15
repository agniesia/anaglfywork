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
