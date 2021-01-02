using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using Microsoft.International.Converters.TraditionalChineseToSimplifiedConverter;

namespace MyLUIS
{
    public static class ChineseHelper
    {
        public static string ToSimplifiedChinese(string InputString)
        {
            return ChineseConverter.Convert(InputString, ChineseConversionDirection.TraditionalToSimplified);
         
        }
        public static string ToTraditionalChinese(string InputString)
        {
            return ChineseConverter.Convert(InputString, ChineseConversionDirection.SimplifiedToTraditional);
        }

        public static string ChineseToUnicode(string str)
        {
            byte[] bts = Encoding.Unicode.GetBytes(str);
            string r = "";
            for (int i = 0; i < bts.Length; i += 2) r += "\\u" + bts[i + 1].ToString("x").PadLeft(2, '0') + bts[i].ToString("x").PadLeft(2, '0');
            return r;
        }
        /// <summary>
        /// 將Unicode編碼轉換爲漢字字符串
        /// </summary>
        /// <param name="str">Unicode編碼字符串</param>
        /// <returns>漢字字符串</returns>

        public static string UnicodeToChinese(string str)
        {
            string r = "";
            MatchCollection mc = Regex.Matches(str, @"\\u([\w]{2})([\w]{2})", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            byte[] bts = new byte[2];
            foreach (Match m in mc)
            {
                bts[0] = (byte)int.Parse(m.Groups[2].Value, NumberStyles.HexNumber);
                bts[1] = (byte)int.Parse(m.Groups[1].Value, NumberStyles.HexNumber);
                r += Encoding.Unicode.GetString(bts);
            }
            return r;
        }

        //全形字元的unicode編碼從65281 ~65374 （十六進位制 0xFF01 ~ 0xFF5E），
        //對應到半形字元unicode編碼從33 ~126 （十六進位制 0x21~ 0x7E）
        //空格比較特殊，全形為 12288（0x3000），半形為 32（0x20）
        public static string ToHalfWidth(string InputString)
        {
            string result = "";
            for (int i = 0; i < InputString.Length; i++)
            {
                int stringcode = (int)InputString[i];
                if (stringcode >= 65281 && stringcode < 65375)
                {
                    result += Microsoft.VisualBasic.Strings.Chr(stringcode - 65248);
                }
                else if (stringcode == 12288)
                {
                    result += Microsoft.VisualBasic.Strings.Chr(32);
                }
                else
                {
                    result += InputString[i];
                }
            }
            return result;
        }

        public static string ToFullWidth(string InputString)
        {
            string result = "";
            for (int i = 0; i < InputString.Length; i++)
            {
                int stringcode = (int)InputString[i];
                if (stringcode+65248 >= 65281 && stringcode < 65375)
                {
                    result += Microsoft.VisualBasic.Strings.Chr(stringcode +65248);
                }
                else if (stringcode == 32)
                {
                    result += Microsoft.VisualBasic.Strings.Chr(12288);
                }
                else
                {
                    result += InputString[i];
                }
            }
            return result;
        }

    }
}
