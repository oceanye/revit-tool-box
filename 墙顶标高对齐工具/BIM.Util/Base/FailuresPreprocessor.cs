using Autodesk.Revit.DB;
using System.Collections.Generic;

namespace BIM.Util.Base
{
    public class FailuresPreprocessor : IFailuresPreprocessor
    {
        /// <summary>
        /// 忽略警告弹窗
        /// </summary>
        /// <param name="failuresAccessor"> 异常提示</param>
        /// <returns></returns>
        public FailureProcessingResult PreprocessFailures(FailuresAccessor failuresAccessor)
        {
           var listFma = failuresAccessor.GetFailureMessages();
            if (listFma.Count == 0) return FailureProcessingResult.Continue;
            foreach (FailureMessageAccessor fma in listFma)
            {
                if (fma.GetSeverity() == FailureSeverity.Error)
                {
                    if (fma.HasResolutions())
                    {
                        failuresAccessor.ResolveFailure(fma);
                    }
                }
                if (fma.GetSeverity() == FailureSeverity.Warning)
                {
                    failuresAccessor.DeleteWarning(fma);
                }
            }
            return FailureProcessingResult.ProceedWithCommit;
        }
    }
}
