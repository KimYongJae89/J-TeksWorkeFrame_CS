using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KiyLib.General
{
    // 복소수를 표현한 클래스, 퓨리에 변환에 사용할 목적으로 만듦
    /// <summary>
    /// 복소수 클래스
    /// </summary>
    public class Complex
    {
        private double _re;
        private double _im;

        /// <summary>
        /// 실수
        /// </summary>
        public double Real
        {
            get { return _re; }
            set { _re = value; }
        }

        /// <summary>
        /// 허수
        /// </summary>
        public double Imaginary
        {
            get { return _im; }
            set { _im = value; }
        }


        public Complex() { }

        /// <summary>
        /// 실수, 허수 입력 생성자
        /// </summary>
        /// <param name="real">실수부</param>
        /// <param name="imaginary">허수부</param>
        public Complex(double real, double imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }


        /// <summary>
        /// Complex객체 끼리 곱한다
        /// </summary>
        /// <param name="a">대상 객체 1</param>
        /// <param name="b">대상 객체 2</param>
        /// <returns></returns>
        public static Complex Multiply(Complex a, Complex b)
        {
            Complex c = new Complex()
            {
                Real = a.Real * b.Real - a.Imaginary * b.Imaginary,
                Imaginary = a.Imaginary * b.Real + a.Real * b.Imaginary
            };

            return c;
        }
    }
}
