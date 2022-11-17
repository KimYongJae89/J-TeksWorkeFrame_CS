using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KiyLib.General
{
    /// <summary>
    /// 공용으로 자주 사용되는 기능들을 위한 클래스
    /// </summary>
    public class KCommon
    {
        /// <summary>
        /// 열린 Form중에서 입력한 이름을 가진 Form이 존재하는지 검사합니다.(대소문자 구분)
        /// </summary>
        /// <param name="frmName">검사할 Form의 이름입니다.</param>
        /// <param name="activeIfOpened">Form이 열려있다면 활성화 시킬지의 여부입니다.</param>
        /// <returns>존재하면 true, 그렇지 않으면 false</returns>
        public static bool IsFormOpened(string frmName, bool activeIfOpened = true)
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.Name == frmName)
                {
                    if (activeIfOpened)
                        frm.Activate();

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 양끝에서 0이 아닌 최초의 배열의 인덱스를 반환합니다. (보통 히스토그램에서 사용)
        /// </summary>
        /// <param name="input">검색할 배열입니다.</param>
        /// <param name="startToEnd">시작부터 끝까지 검색해서 0이 아닌 최초의 인덱스를 뜻합니다.</param>
        /// <param name="endToStart">끝까지 시작까지 검색해서 0이 아닌 최초의 인덱스를 뜻합니다.</param>
        public static void IndexOfFirstNonZeroAtBothSide(int[] input, out int startToEnd, out int endToStart)
        {
            startToEnd = Array.FindIndex(input, item => item != 0);
            endToStart = Array.FindLastIndex(input, item => item != 0);
        }

        /// <summary>
        /// 양끝에서 0이 아닌 최초의 배열의 인덱스를 반환합니다. (보통 히스토그램에서 사용)
        /// </summary>
        /// <param name="input">검색할 배열입니다.</param>
        /// <param name="startToEnd">시작부터 끝까지 검색해서 0이 아닌 최초의 인덱스를 뜻합니다.</param>
        /// <param name="endToStart">끝까지 시작까지 검색해서 0이 아닌 최초의 인덱스를 뜻합니다.</param>
        public static void IndexOfFirstNonZeroAtBothSide(float[] input, out int startToEnd, out int endToStart)
        {
            startToEnd = Array.FindIndex(input, item => item != 0);
            endToStart = Array.FindLastIndex(input, item => item != 0);
        }

        /// <summary>
        /// t가 값 형식(int, string, bool...) 인지 확인한다. 제네릭 메서드에 사용하려고 만듦
        /// </summary>
        /// <typeparam name="T">값 형식인지 확인할 대상</typeparam>
        /// <returns>true=이면 값 형식, false이면 아니다</returns>
        public static bool IsValueType(Type t)
        {
            if (!(t == typeof(bool)) &&

                !(t == typeof(byte)) &&

                !(t == typeof(short)) &&
                !(t == typeof(int)) &&
                !(t == typeof(long)) &&

                !(t == typeof(ushort)) &&
                !(t == typeof(uint)) &&
                !(t == typeof(ulong)) &&

                !(t == typeof(float)) &&
                !(t == typeof(double)) &&
                !(t == typeof(decimal)) &&

                !(t == typeof(char)) &&
                !(t == typeof(string)))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// string을 T 형식으로 변환해 반환한다. 여기서 T는 값 형식이다.
        /// T가 값 형식인지는 KCommon.IsValueType(Type t) 메서드로 알수 있다.
        /// </summary>
        /// <typeparam name="T">변환될 값 형식</typeparam>
        /// <param name="value">값 형식으로 변환할 string</param>
        /// <returns>변환된 값 형식을 반환한다</returns>
        public static T ConvertToValueType<T>(string value)
        {
            Type t = typeof(T);

            if (!IsValueType(t))
                throw new FormatException("값 형식이 아닙니다.");

            if (t == typeof(bool))
                return (T)(object)Convert.ToBoolean(value);

            if (t == typeof(byte))
                return (T)(object)Convert.ToByte(value);

            if (t == typeof(short))
                return (T)(object)Convert.ToInt16(value);
            if (t == typeof(int))
                return (T)(object)Convert.ToInt32(value);
            if (t == typeof(long))
                return (T)(object)Convert.ToInt64(value);

            if (t == typeof(ushort))
                return (T)(object)Convert.ToUInt16(value);
            if (t == typeof(uint))
                return (T)(object)Convert.ToUInt32(value);
            if (t == typeof(ulong))
                return (T)(object)Convert.ToUInt64(value);

            if (t == typeof(float))
                return (T)(object)Convert.ToSingle(value);
            if (t == typeof(double))
                return (T)(object)Convert.ToDouble(value);
            if (t == typeof(decimal))
                return (T)(object)Convert.ToDecimal(value);

            if (t == typeof(char))
                return (T)(object)Convert.ToChar(value);
            if (t == typeof(string))
                return (T)(object)value;

            else
                return default(T);
        }

        /// <summary>
        /// 변수가 Integer타입, 또는 Integer타입으로 변환할수 있는지 알아낸다
        /// </summary>
        /// <param name="value">검사할 변수</param>
        /// <returns>결과 값</returns>
        public static bool IsInteger(ValueType value)
        {
            return (value is SByte  ||  value is Int16   || value is Int32
                 || value is Int64  ||  value is Byte    || value is UInt16
                 || value is UInt32 ||  value is UInt64);
        }

        /// <summary>
        /// 변수가 Float타입, 또는 Float타입으로 변환할수 있는지 알아낸다
        /// </summary>
        /// <param name="value">검사할 변수</param>
        /// <returns>결과 값</returns>
        public static bool IsFloat(ValueType value)
        {
            return (value is float | value is double | value is Decimal);
        }

        /// <summary>
        /// 변수가 Numeric타입, 또는 Numeric타입으로 변환할수 있는지 알아낸다
        /// </summary>
        /// <param name="value">검사할 변수</param>
        /// <returns>결과 값</returns>
        public static bool IsNumeric(ValueType value)
        {
            return (value is Byte ||
                    value is Int16 ||
                    value is Int32 ||
                    value is Int64 ||
                    value is SByte ||
                    value is UInt16 ||
                    value is UInt32 ||
                    value is UInt64 ||
                    value is Decimal ||
                    value is Double ||
                    value is Single);
        }
    }
}
