using Slm.Utils.Core.DependencyInjection;
using System.Text;

namespace Slm.Utils.Core.Helpers;


public class PinYinHelper : ISingletonDependency
{
    /// <summary>
    /// 取汉字拼音的首字母
    /// </summary>
    /// <param name="UnName">汉字</param>
    /// <returns>首字母</returns>
    public  string GetCodstring(string UnName)
    {
        int i = 0;
        ushort key = 0;
        string strResult = string.Empty;

        Encoding unicode = Encoding.Unicode;
        Encoding gbk = Encoding.GetEncoding(936);
        byte[] unicodeBytes = unicode.GetBytes(UnName);
        byte[] gbkBytes = Encoding.Convert(unicode, gbk, unicodeBytes);
        while (i < gbkBytes.Length)
        {
            if (gbkBytes[i] <= 127)
            {
                strResult = strResult + (char)gbkBytes[i];
                i++;
            }
            #region 生成汉字拼音简码,取拼音首字母
            else
            {
                key = (ushort)(gbkBytes[i] * 256 + gbkBytes[i + 1]);
                if (key >= '\uB0A1' && key <= '\uB0C4')
                {
                    strResult = strResult + "A";
                }
                else if (key >= '\uB0C5' && key <= '\uB2C0')
                {
                    strResult = strResult + "B";
                }
                else if (key >= '\uB2C1' && key <= '\uB4ED')
                {
                    strResult = strResult + "C";
                }
                else if (key >= '\uB4EE' && key <= '\uB6E9')
                {
                    strResult = strResult + "D";
                }
                else if (key >= '\uB6EA' && key <= '\uB7A1')
                {
                    strResult = strResult + "E";
                }
                else if (key >= '\uB7A2' && key <= '\uB8C0')
                {
                    strResult = strResult + "F";
                }
                else if (key >= '\uB8C1' && key <= '\uB9FD')
                {
                    strResult = strResult + "G";
                }
                else if (key >= '\uB9FE' && key <= '\uBBF6')
                {
                    strResult = strResult + "H";
                }
                else if (key >= '\uBBF7' && key <= '\uBFA5')
                {
                    strResult = strResult + "J";
                }
                else if (key >= '\uBFA6' && key <= '\uC0AB')
                {
                    strResult = strResult + "K";
                }
                else if (key >= '\uC0AC' && key <= '\uC2E7')
                {
                    strResult = strResult + "L";
                }
                else if (key >= '\uC2E8' && key <= '\uC4C2')
                {
                    strResult = strResult + "M";
                }
                else if (key >= '\uC4C3' && key <= '\uC5B5')
                {
                    strResult = strResult + "N";
                }
                else if (key >= '\uC5B6' && key <= '\uC5BD')
                {
                    strResult = strResult + "O";
                }
                else if (key >= '\uC5BE' && key <= '\uC6D9')
                {
                    strResult = strResult + "P";
                }
                else if (key >= '\uC6DA' && key <= '\uC8BA')
                {
                    strResult = strResult + "Q";
                }
                else if (key >= '\uC8BB' && key <= '\uC8F5')
                {
                    strResult = strResult + "R";
                }
                else if (key >= '\uC8F6' && key <= '\uCBF9')
                {
                    strResult = strResult + "S";
                }
                else if (key >= '\uCBFA' && key <= '\uCDD9')
                {
                    strResult = strResult + "T";
                }
                else if (key >= '\uCDDA' && key <= '\uCEF3')
                {
                    strResult = strResult + "W";
                }
                else if (key >= '\uCEF4' && key <= '\uD1B8')
                {
                    strResult = strResult + "X";
                }
                else if (key >= '\uD1B9' && key <= '\uD4D0')
                {
                    strResult = strResult + "Y";
                }
                else if (key >= '\uD4D1' && key <= '\uD7F9')
                {
                    strResult = strResult + "Z";
                }
                else
                {
                    strResult = strResult + "?";
                }
                i = i + 2;
            }
            #endregion
        }
        return strResult;
    }
}
