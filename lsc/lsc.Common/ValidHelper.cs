using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace bnuxq.Common
{
    public static class ValidHelper
    {
        /// <summary>
        /// 将时间转化为时间戳
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long TryToUnixTime(this DateTime time)
        {
            TimeSpan ts = time - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);
        }

        /// <summary>
        /// 判断字符串是否为空
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNull(this object obj)
        {
            if (obj == null)
                return true;
            if (obj is string)
            {
                return string.IsNullOrEmpty(obj.ToString());
            }
            return false;
        }

        /// <summary>
        /// Obj To String
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string TryToString(this object obj)
        {
            if (obj != null)
                return obj.ToString();
            return "";
        }

        /// <summary>
        /// 字符串转化Int
        /// </summary>
        /// <param name="str"></param>
        /// <param name="fail"></param>
        /// <returns></returns>
        public static int TryToInt(this object str, int fail = -1)
        {
            int res = 0;
            if (str is decimal)
            {
                try
                {
                    res = Decimal.ToInt32(str.TryToDecimal());
                }
                catch (OverflowException ex)
                {
                    res = fail;
                }
            }
            else
            {
                if (!int.TryParse(str.TryToString(), out res))
                    res = fail;
            }

            return res;
        }
        /// <summary>
        /// 字符串转化Double
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static double TryToDouble(this object str)
        {
            double d = 0;
            if (!double.TryParse(str.TryToString(), out d))
                d = 0;
            return d;
        }
        /// <summary>
        /// 字符串转化Float
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static float TryToFloat(this object str)
        {
            float f = 0;
            if (!float.TryParse(str.TryToString(), out f))
                f = 0;
            return f;
        }

        /// <summary>
        /// 字符串转化为DateTime 转化失败值等于DateTime.MinValue
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime TryToDateTime(this object str)
        {
            DateTime res = DateTime.MinValue;
            if (!DateTime.TryParse(str.TryToString(), out res))
                res = DateTime.MinValue;
            return res;
        }
        /// <summary>
        /// 字符串转化Boolean
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool TryToBoolean(this object str)
        {
            bool res = false;
            if (!bool.TryParse(str.TryToString(), out res))
                res = false;
            return res;
        }
        /// <summary>
        /// 字符串转化decimal
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static decimal TryToDecimal(this object str)
        {
            decimal res = 0;
            if (!decimal.TryParse(str.TryToString(), out res))
                res = 0;
            return res;
        }

        /// <summary>
        /// 字符串转化成LONG
        /// </summary>
        /// <param name="str"></param>
        /// <param name="fail"></param>
        /// <returns></returns>
        public static long TryToLong(this object str, int fail = -1)
        {
            long res = 0;
            if (!long.TryParse(str.TryToString(), out res))
                res = fail;
            return res;
        }
        /// <summary>
        /// MD5函数(utf-8编码)
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <returns>MD5结果</returns>
        public static string MD5(this string str)
        {
            if (str.IsNull())
                return string.Empty;
            byte[] b = Encoding.UTF8.GetBytes(str);
            b = new MD5CryptoServiceProvider().ComputeHash(b);
            string ret = "";
            for (int i = 0; i < b.Length; i++)
                ret += b[i].ToString("x").PadLeft(2, '0');

            return ret.ToLower();
        }

        /// <summary>
        /// MD5函数
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <returns>MD5结果</returns>
        public static string MD5(this string str, Encoding encoding)
        {
            if (str.IsNull())
                return string.Empty;
            byte[] b = encoding.GetBytes(str);
            //byte[] b = Encoding.UTF8.GetBytes(str);
            b = new MD5CryptoServiceProvider().ComputeHash(b);
            string ret = "";
            for (int i = 0; i < b.Length; i++)
                ret += b[i].ToString("x").PadLeft(2, '0');

            return ret.ToLower();
        }
        /// <summary>
        /// SHA1加密字符串(utf-8编码)
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <returns>加密后的字符串</returns> 
        public static string SHA1(this string str)
        {
            if (str.IsNull())
                return string.Empty;

            byte[] value = Encoding.UTF8.GetBytes(str);
            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] result = sha.ComputeHash(value);
            string delimitedHexHash = BitConverter.ToString(result);
            string hexHash = delimitedHexHash.Replace("-", "");

            return hexHash;
        }
        /// <summary>
        /// SHA1加密字符串
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <returns>加密后的字符串</returns> 
        public static string SHA1(this string str, Encoding encoding)
        {
            if (str.IsNull())
                return string.Empty;

            byte[] value = encoding.GetBytes(str);
            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] result = sha.ComputeHash(value);
            string delimitedHexHash = BitConverter.ToString(result);
            string hexHash = delimitedHexHash.Replace("-", "");

            return hexHash;
        }
        /// <summary>
        /// MD5函数
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string MD5(this byte[] b)
        {
            if (b == null)
                return string.Empty;

            b = new MD5CryptoServiceProvider().ComputeHash(b);

            string strHashData = System.BitConverter.ToString(b);
            strHashData = strHashData.Replace("-", "");
            return strHashData.ToLower();
        }

        /// <summary>
        /// author:liushuaichao
        /// create date:2011.10.11
        /// description:验证是否为手机号
        /// @"^((13[0-9])|(15[0|1|2|3|5|6|7|8|9])|(18[0|2|3|5|6|7|8|9]))\d{8}$"
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool IsMobile(this string inputData)
        {
            Regex reg = new Regex(@"^13\d{9}$");
            Regex reg1 = new Regex(@"^15\d{9}$");
            Regex reg2 = new Regex(@"^18[0,1,2,3,,5,6,7,8,9]\d{8}$");
            Regex reg3 = new Regex(@"^14[0,1,2,3,,5,6,7,8,9]\d{8}$");
            if (reg.Match(inputData).Success || reg1.Match(inputData).Success || reg2.Match(inputData).Success || reg3.Match(inputData).Success)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 验证是否为数字
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool IsNum(this string inputData)
        {
            Regex reg = new Regex("^[0-9]+$");
            if (reg.Match(inputData).Success)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 验证是否为大写字母
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool IsCapital(this string inputData)
        {
            Regex reg = new Regex("^[A-Z]+$");
            if (reg.Match(inputData).Success)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 校验是否为邮箱
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool IsEmail(this string inputData)
        {
            Regex reg = new Regex("^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$");
            if (reg.Match(inputData).Success)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 截取字符串，如果字符串长度大于length则截取length长度的字符串并且在后边加上...
        /// </summary>
        /// <param name="inputData"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string SubStr(this string inputData, int length)
        {
            string result = inputData;
            if (inputData.Length > length)
            {
                result = inputData.Substring(0, length) + "...";
            }
            return result;
        }
        /// <summary>
        /// 过滤掉文本中的HTML标签
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static string FilterHtml(this string inputData)
        {
            string Htmlstring = Regex.Replace(inputData, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<[^>]+>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"–>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!–.*", "", RegexOptions.IgnoreCase);
            //Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"",RegexOptions.IgnoreCase);
            //Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            //Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<",RegexOptions.IgnoreCase);
            //Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">",RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
            //Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            //Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            //Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            //Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            //Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
            return Htmlstring;
        }
    }
}
