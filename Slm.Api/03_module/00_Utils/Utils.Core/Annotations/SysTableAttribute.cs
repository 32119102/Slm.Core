using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slm.Utils.Core.Annotations;


/// <summary>
/// 系统表特性
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public class SysTableAttribute : Attribute
{
}

