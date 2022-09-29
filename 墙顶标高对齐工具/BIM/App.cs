using Autodesk.Revit.UI;

namespace BIM
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    public class App : IExternalApplication
    {
        /// <summary>
        /// 注销
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public Result OnShutdown(UIControlledApplication application) => Result.Succeeded;

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public Result OnStartup(UIControlledApplication application)
        {
            var boundingApp = new BoundingApp(application);
            boundingApp.Init(2020);
            return Result.Succeeded;
        }

    }
}
