using MediatR;

namespace Sys.Application.Company.Event;

/// <summary>
/// 新增公司事件参数
/// </summary>
public class AddCompanyEvent : IRequest<bool>
{
    /// <summary>
    /// 新增公司事件处理
    /// </summary>
    public class AddCompanyEventHandler : IRequestHandler<AddCompanyEvent, bool>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public AddCompanyEventHandler()
        {
        }

        /// <summary>
        /// 事件处理
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> Handle(AddCompanyEvent dto, CancellationToken cancellationToken)
        {
            return await Task.FromResult(true);
        }
    }
}

/*
 * AddCompanyEvent 定义参数
 * IRequest<T> T为返回的参数
 */

/*
  bool isSu = await this._mediator.Send(new AddCompanyEvent
            {
               
            });
 */