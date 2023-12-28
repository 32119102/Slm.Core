//using Slm.Utils.Core.Const;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Microsoft.Extensions.Logging
//{
//    public static class LoggingExtensions
//    {
//        #region Sql日志
//        public static void _SqlLogInformation(this ILogger logger, string message, params object[]? args)
//        {
//            logger.LogInformation($"{{{SerilogConst.Placeholder}}}:{message}", SerilogConst.Sql, args);

//        }
//        #endregion

//        #region Error日志
//        public static void _ErrorLogInformation(this ILogger logger, string message, params object[]? args)
//        {
//            logger.LogError($"{{{SerilogConst.Placeholder}}}:{message}", SerilogConst.Error, args);

//        }
//        #endregion

//        #region HttpApi请求日志
//        public static void _HttpLogInformation(this ILogger logger, string message, params object[]? args)
//        {
//            logger.LogInformation(message.ToLog(), SerilogConst.Http,args);

//        }
//        #endregion

//        #region Info日志
//        public static void _InfoLogInformation(this ILogger logger, string message, params object[]? args)
//        {
//            logger.LogInformation($"{{{SerilogConst.Placeholder}}}:{message}", SerilogConst.Info, args);

//        }
//        #endregion
//    }
//}
