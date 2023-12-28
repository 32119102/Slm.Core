using Microsoft.AspNetCore.Http;
using Slm.Utils.Core.DependencyInjection;
using Slm.Utils.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;


namespace Slm.Utils.Core.Helpers;

public class FileHelper : ISingletonDependency
{




    public async Task<string> SaveAsync(IFormFile formFile, string path,string filePath, string fileName)
    {
        string filePathSave = Path.Combine(path, filePath);
        var oldName = formFile.FileName.Substring(0, formFile.FileName.LastIndexOf('.'));
        var extName = formFile.FileName.Substring(formFile.FileName.LastIndexOf('.') + 1);
  
        // 完整路径
        string strFullFileName = string.Format(@"{0}/{1}.{2}", filePathSave, fileName + oldName, extName);
        string urlResule = $"{filePath}/{fileName + oldName}.{extName}";

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        FileStream fs = new FileStream(strFullFileName, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.ReadWrite);

        try
        {
            using MemoryStream stream = new MemoryStream();
            await formFile.CopyToAsync(stream);

            BinaryWriter writer = new BinaryWriter(fs, Encoding.UTF8); // 写入对象

            long offset = fs.Length; // 字节偏移量

            writer.Seek((int)offset, SeekOrigin.Begin);

            writer.Write(stream.ToArray()); // 读取传入的数据流,并写入buffer二进制数据中
        }
        catch (System.Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(ex.Message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        finally
        {
            fs.Close();
            fs.Dispose();
        }
        return urlResule;
    }




    public async Task<(string oldName, string extName, string url)> CreateAsync(IFormFile formFile,long fileId)
    {
        //FileUpload Path
        string appPath = Directory.GetCurrentDirectory(); 
        string date = DateTime.Now.ToString("yyyy/MM/dd");
        //文件保存路径
        string filePath = $"{appPath}\\{App.app("FileUpload:Path")}\\{date}";

       
        //long fileId = YitIdHelper.NextId();
        var oldName = formFile.FileName.Substring(0, formFile.FileName.LastIndexOf('.'));
        var extName = formFile.FileName.Substring(formFile.FileName.LastIndexOf('.') + 1);


        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }


        // 完整路径
        string strFullFileName = string.Format(@"{0}/{1}.{2}", filePath, fileId, extName);

        if (IsExistFile(strFullFileName))
        {
            DeleteFile(strFullFileName);
        }
        FileStream fs = new FileStream(strFullFileName, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.ReadWrite);

        try
        {
            using MemoryStream stream = new MemoryStream();
            await formFile.CopyToAsync(stream);

            BinaryWriter writer = new BinaryWriter(fs,Encoding.UTF8); // 写入对象

            long offset = fs.Length; // 字节偏移量

            writer.Seek((int)offset, SeekOrigin.Begin);

            writer.Write(stream.ToArray()); // 读取传入的数据流,并写入buffer二进制数据中
        }
        catch (System.Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(ex.Message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        finally
        {
            fs.Close();
            fs.Dispose();
        }
        return (oldName, extName, Path.Combine(App.app("FileUpload:Path"), date).ToPath());

    }

    public bool IsExistFile(string filePath)
    {
        return File.Exists(filePath);
    }

    public void DeleteFile(string filePath)
    {
        if (IsExistFile(filePath))
        {
            File.Delete(filePath);
        }
    }


    public  MemoryStream FileRead(string strFilePath, string strFileName)
    {
        string resultMessage = string.Empty;
        var files = Directory.GetCurrentDirectory();
        string filePath = Path.Combine(files, strFilePath,strFileName);
    
        if (!File.Exists(filePath))
        {
            return null;
        }
        FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);//文件路径
        try
        {
            int b1;
            System.IO.MemoryStream tempStream = new System.IO.MemoryStream();
            while ((b1 = fs.ReadByte()) != -1)
            {
                tempStream.WriteByte(((byte)b1));
            }
            return tempStream;
        }
        catch (System.Exception ex)
        {
            return null;
        }
        finally
        {
            fs.Close();
        }
    }


    public MemoryStream FileReadWwwRoot(string path,string fileName) 
    {
        string filePath = Path.Combine(path, fileName);
        if (!File.Exists(filePath))
        {
            return null;
        }
        FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);//文件路径
        try
        {
            int b1;
            System.IO.MemoryStream tempStream = new System.IO.MemoryStream();
            while ((b1 = fs.ReadByte()) != -1)
            {
                tempStream.WriteByte(((byte)b1));
            }
            return tempStream;
        }
        catch (System.Exception ex)
        {
            return null;
        }
        finally
        {
            fs.Close();
        }

    }


}
