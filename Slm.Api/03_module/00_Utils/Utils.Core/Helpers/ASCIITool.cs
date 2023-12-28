using System;
using System.Collections.Generic;
using System.Text;

namespace Slm.Utils.Core.Helpers;

public class ASCIITool
{
    private static string _character = '\x02'.ToString();

    /// <summary>
    /// 字符转ASCII码
    /// </summary>
    /// <param name="character"></param>
    /// <returns></returns>
    public static string ASCToChr()
    {

        return _character;
    }

    /// <summary>
    /// ASCII码转字符
    /// </summary>
    /// <param name="asciiCode"></param>
    /// <returns></returns>
    public static string ChrToASC(int asciiCode)
    {
        if (asciiCode >= 0 && asciiCode <= 255)
        {
            System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
            byte[] byteArray = new byte[] { (byte)asciiCode };
            string strCharacter = asciiEncoding.GetString(byteArray);
            return (strCharacter);
        }
        else
        {
            throw new Exception("ASCII Code is not valid.");
        }
    }
}
