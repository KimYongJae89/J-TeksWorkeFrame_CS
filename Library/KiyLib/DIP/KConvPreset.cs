using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiyLib.DIP
{
    /// <summary>
    /// 필터의 Convolution연산을 위한 사전정의된 Kernel(커널)을 모아둔 클래스
    /// </summary>
    public class KConvPreset
    {
        public static float[,] Sharp1 ={{ 0.0f, -1.0f,  0.0f},
                                        {-1.0f,  5.0f, -1.0f},
                                        { 0.0f, -1.0f,  0.0f}};

        public static float[,] Sharp2 = {{-1.0f, -1.0f, -1.0f},
                                         {-1.0f,  9.0f, -1.0f},
                                         {-1.0f, -1.0f, -1.0f}};

        public static float[,] Average = {{1.0f / 9.0f, 1.0f / 9.0f, 1.0f / 9.0f},
                                          {1.0f / 9.0f, 1.0f / 9.0f, 1.0f / 9.0f},
                                          {1.0f / 9.0f, 1.0f / 9.0f, 1.0f / 9.0f}};

        public static float[,] HorizonEdge = {{-2.0f, -2.0f, -2.0f},
                                              { 1.0f,  0.0f,  1.0f},
                                              { 2.0f,  2.0f,  2.0f}};

        public static float[,] VerticalEdge = {{-2.0f, 0.0f, 2.0f},
                                               {-2.0f, 0.0f, 2.0f},
                                               {-2.0f, 0.0f, 2.0f}};
    }
}
