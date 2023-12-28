using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slm.Utils.Core.Helpers;

public static class ConsoleHelper
{
    static object locker = new object();

    public static void WriteColorLine(string str, ConsoleColor color)
    {
        lock (locker)
        {
            ConsoleColor currentForeColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(str);
            Console.ForegroundColor = currentForeColor;
        }
    }

    /// <summary>
    /// 打印错误信息
    /// </summary>
    /// <param name="str">待打印的字符串</param>
    /// <param name="color">想要打印的颜色</param>
    public static void WriteErrorLine(this string str, ConsoleColor color = ConsoleColor.Red)
    {
        lock (locker)
        {
            WriteColorLine(str, color);
        }
    }

    /// <summary>
    /// 打印警告信息
    /// </summary>
    /// <param name="str">待打印的字符串</param>
    /// <param name="color">想要打印的颜色</param>
    public static void WriteWarningLine(this string str, ConsoleColor color = ConsoleColor.Yellow)
    {
        lock (locker)
        {
            WriteColorLine(str, color);
        }
    }
    /// <summary>
    /// 打印正常信息
    /// </summary>
    /// <param name="str">待打印的字符串</param>
    /// <param name="color">想要打印的颜色</param>
    public static void WriteInfoLine(this string str, ConsoleColor color = ConsoleColor.White)
    {
        lock (locker)
        {
            WriteColorLine(str, color);
        }
    }
    /// <summary>
    /// 打印成功的信息
    /// </summary>
    /// <param name="str">待打印的字符串</param>
    /// <param name="color">想要打印的颜色</param>
    public static void WriteSuccessLine(this string str, ConsoleColor color = ConsoleColor.Green)
    {
        lock (locker)
        {
            WriteColorLine(str, color);
        }
    }



    /// <summary>
    /// 启动后打印Banner图案
    /// </summary>
    public static void ConsoleBanner(string? url)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine();
        Console.WriteLine(@" ***************************************************************************************************************");
        Console.WriteLine(@" *                                                                                                             *");
        Console.WriteLine(@" *                         __       __     _____      ______                                                   *");
        Console.WriteLine(@" *                        /  |  _  /  |   /     |    /      \                                                  *");
        Console.WriteLine(@" *                        $$ | / \ $$ |   $$$$$ |   /$$$$$$  |  ______    ______    ______                     *");
        Console.WriteLine(@" *                        $$ |/$  \$$ |      $$ |   $$ |  $$/  /      \  /      \  /      \                    *");
        Console.WriteLine(@" *                        $$ /$$$  $$ | __   $$ |   $$ |      /$$$$$$  |/$$$$$$  |/$$$$$$  |                   *");
        Console.WriteLine(@" *                        $$ $$/$$ $$ |/  |  $$ |   $$ |   __ $$ |  $$ |$$ |  $$/ $$    $$ |                   *");
        Console.WriteLine(@" *                        $$$$/  $$$$ |$$ \__$$ |__ $$ \__/  |$$ \__$$ |$$ |      $$$$$$$$/                    *");
        Console.WriteLine(@" *                        $$$/    $$$ |$$    $$//  |$$    $$/ $$    $$/ $$ |      $$       |                   *");
        Console.WriteLine(@" *                        $$/      $$/  $$$$$$/ $$/  $$$$$$/   $$$$$$/  $$/        $$$$$$$/                    *");

        Console.WriteLine(@" *                                                                                                             *");
        Console.Write(@" *                                      ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(@$"启动成功，欢迎使用 Slm.Core平台~~          ");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(@"                             *");
        Console.WriteLine(@" *                                                                                                             *");
        Console.Write(@" *                                      ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(@"接口地址：" + url);
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(@"                                        *");
        Console.WriteLine(@" *                                                                                                             *");
        Console.WriteLine(@" ***************************************************************************************************************");
        Console.WriteLine();
    }

}

