namespace Wj.Host.Options;

public class HostOptions
{
    /// <summary>
    /// 绑定的地址(默认：http://*:5000)
    /// </summary>
    public string Urls { get; set; }=string.Empty;
}
