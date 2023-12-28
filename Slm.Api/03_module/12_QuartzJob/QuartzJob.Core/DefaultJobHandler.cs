
using Slm.QuartzJob.Core.Abstractions;

namespace Slm.QuartzJob.Core
{
    public class DefaultJobHandler : IJobHandler
    {
        /// <summary>
        /// 恢复任务
        /// </summary>
        /// <returns></returns>
        public async Task<bool> RecoveryTaskAsync()
        {
           return await Task.FromResult(true);  
        }
    }
}
