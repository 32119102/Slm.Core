using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slm.Data.Abstractions.Attributes;

/// <summary>
/// 特性事务,方法需要使用virtual标识
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class TransactionAttribute : Attribute
{

}
