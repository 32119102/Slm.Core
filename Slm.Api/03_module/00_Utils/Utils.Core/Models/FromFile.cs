

namespace Slm.Utils.Core.Models;


/// <summary>
/// 表单文件列表
/// </summary>
public class FromFile
{
    /// <summary>
    /// 主键
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// 文件名称
    /// </summary>
    public string fileName { get; set; }


    /// <summary>
    /// 文件大小
    /// </summary>
    public int fileSize { get; set; }


    /// <summary>
    /// 文件url
    /// </summary>
    public string fileUrl { get; set; }
}
