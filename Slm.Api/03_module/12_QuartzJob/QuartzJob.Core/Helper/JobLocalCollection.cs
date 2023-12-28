
namespace Slm.QuartzJob.Core.Helper;

public class JobLocalCollection: IReadOnlyCollection<JobTaskInfo>
{
    public static List<JobTaskInfo> _jobTaskInfos;

    static JobLocalCollection()
    {
        _jobTaskInfos = new List<JobTaskInfo>();
    }
    #region  需要完成的接口
    public int Count => _jobTaskInfos.Count;

    public IEnumerator<JobTaskInfo> GetEnumerator()
    {
        foreach (var item in _jobTaskInfos)
        {
            yield return item;
        }
    }
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }



    #endregion


    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="jobTaskInfo"></param>
    /// <returns></returns>
    public static void Add(JobTaskInfo jobTaskInfo)
    {
        _jobTaskInfos.Add(jobTaskInfo);
    }


    /// <summary>
    /// 移除
    /// </summary>
    /// <param name="jobTaskInfo"></param>
    /// <returns></returns>
    public static bool Remove(JobTaskInfo jobTaskInfo)
    {
        return _jobTaskInfos.Remove(jobTaskInfo);
    }


}