using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using BIM.Util;
using BIM.Util.Extensions;

namespace BIM.Core.WallParameter
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class Cmd : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // 初始化
            var uidoc = commandData.Init();
            var service = new Service.WallService(uidoc);
            // 选择
            var walls = service.GetWalls();
            if (walls == null || walls.Count == 0)
            {
                UI.Print("当前视图可见墙总计:0");
                return Result.Cancelled;
            }
            // 更新
            UI.Print($"调整墙实例总计:{service.SetWalls(walls)}");
            return Result.Succeeded;
        }
    }
}
