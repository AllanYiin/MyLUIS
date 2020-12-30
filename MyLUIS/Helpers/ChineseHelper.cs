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

        public static string ToProportional(string InputString)
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



    }
}
