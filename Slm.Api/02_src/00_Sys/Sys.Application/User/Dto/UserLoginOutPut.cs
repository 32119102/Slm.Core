
using Sys.Domain.Shared.User;
namespace Sys.Application.User.Dto;

public class UserLoginOutPut
{
    /// <summary>
    /// 用户id
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 账号名称
    /// </summary>
    public string Account { get; set; }

    /// <summary>
    /// 真实姓名
    /// </summary>
    public string RealName { get; set; }


    /// <summary>
    /// 账号类型
    /// </summary>
    public UserAccountTypeEnum AccountType { get; set; } = UserAccountTypeEnum.NormalUser;

    /// <summary>
    /// 头像
    /// </summary>
    public string Avatar { get; set; }


    /// <summary>
    /// 机构Id
    /// </summary>
    public long OrgId { get; set; }

    /// <summary>
    /// 机构名称
    /// </summary>
    public string OrgName { get; set; }



    /// <summary>
    /// 按钮权限集合,前端
    /// </summary>
    public List<string> Buttons { get; set; }

    /// <summary>
    /// 角色集合
    /// </summary>
    public List<long> RoleIds { get; set; }

}
