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
    class GrayAnaglyphs : Anaglifyoperation
    {
        public GrayAnaglyphs()
        {
            wskaznik = new float[,] { { 1, 1, 1 }, { 1, 2, 1 }, { 1, 1, 1 } };
        }
    }
    class ColorAnaglyphs : Anaglifyoperation
    {
        public ColorAnaglyphs()
        {
            wskaznik = new float[,] { { 1, 1, 1 }, { 1, 2, 1 }, { 1, 1, 1 } };
        }
    }
    class HalfColorAnaglyphs : Anaglifyoperation
    {
        public HalfColorAnaglyphs()
        {
            wskaznik = new float[,] { { 1, 1, 1 }, { 1, 2, 1 }, { 1, 1, 1 } };
        }
    }
    class OptimizedAnaglyphs : Anaglifyoperation
    {
        public OptimizedAnaglyphs()
        {
            wskaznik = new float[,] { { 1, 1, 1 }, { 1, 2, 1 }, { 1, 1, 1 } };

        }


    }
}
