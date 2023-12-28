using Slm.Utils.Core.Extensions;

namespace Slm.Utils.Core.Models;

/// <summary>
/// 返回结果
/// </summary>
public class ResultModel<T>
{
    /// <summary>
    /// 处理是否成功
    /// </summary>
    public bool Successful { get; set; }

    /// <summary>
    /// 错误信息
    /// </summary>
    public string Msg { get; set; }

    /// <summary>
    /// 业务码
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// 时间戳
    /// </summary>
    public long Timestamp { get; set; }

    /// <summary>
    /// 返回数据
    /// </summary>
    public T Data { get; set; }

    /// <summary>
    /// 成功
    /// </summary>
    /// <param name="data">数据</param>
    /// <param name="msg">说明</param>
    public ResultModel<T> Success(T data = default, string msg = "success")
    {
        Successful = true;
        Data = data;
        Msg = msg;

        return this;
    }

    /// <summary>
    /// 失败
    /// </summary>
    /// <param name="msg">说明</param>
    public ResultModel<T> Failed(string msg = "failed")
    {
        Successful = false;
        Msg = msg;
        return this;
    }

    public ResultModel()
    {
        Timestamp = DateTime.Now.ToTimestamp();
    }
}

/// <summary>
/// 返回结果
/// </summary>
public static class ResultModel
{
    /// <summary>
    /// 成功
    /// </summary>
    /// <param name="data">返回数据</param>
    /// <returns></returns>
    public static ResultModel<T> Success<T>(T data = default)
    {
        return new ResultModel<T>().Success(data);
    }

    /// <summary>
    /// 成功
    /// </summary>
    /// <param name="task">任务</param>
    /// <returns></returns>
    public static async Task<ResultModel<T>> SuccessAsync<T>(Task<T> task = default)
    {
        if (task != null)
            return new ResultModel<T>().Success(await task);

        return new ResultModel<T>();
    }

    /// <summary>
    /// 成功
    /// </summary>
    /// <returns></returns>
    public static ResultModel<string> Success()
    {
        return Success<string>();
    }

    /// <summary>
    /// 失败
    /// </summary>
    /// <param name="error">错误信息</param>
    /// <returns></returns>
    public static ResultModel<T> Failed<T>(string error = null)
    {
        return new ResultModel<T>().Failed(error ?? "failed");
    }

    /// <summary>
    /// 失败
    /// </summary>
    /// <returns></returns>
    public static ResultModel<string> Failed(string error = null)
    {
        return Failed<string>(error);
    }


    /// <summary>
    /// 系统异常
    /// </summary>
    /// <param name="msg">消息</param>
    /// <param name="code">编码</param>
    /// <returns></returns>
    public static AppException Exception(string msg = null, string code = null)
    {
        return new AppException(msg, code);
    }



    /// <summary>
    /// 根据布尔值返回结果
    /// </summary>
    /// <param name="success"></param>
    /// <returns></returns>
    public static ResultModel<T> Result<T>(bool success)
    {
        return success ? Success<T>() : Failed<T>();
    }

    /// <summary>
    /// 根据布尔值返回结果
    /// </summary>
    /// <param name="success"></param>
    /// <returns></returns>
    public static async Task<ResultModel<string>> Result(Task<bool> success)
    {
        return await success ? Success() : Failed();
    }

    /// <summary>
    /// 根据布尔值返回结果
    /// </summary>
    /// <param name="success"></param>
    /// <returns></returns>
    public static ResultModel<string> Result(bool success)
    {
        return success ? Success() : Failed();
    }

}