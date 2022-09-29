using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Collections.Generic;

namespace BIM.Core.WallParameter.Service
{
    public interface IWallService
    {
        /// <summary>
        /// uidoc
        /// </summary>
        UIDocument UIdoc { get; set; }


        /// <summary>
        /// 选择墙
        /// </summary>
        /// <returns></returns>
        List<Wall> GetWalls();


        /// <summary>
        /// 更新墙
        /// </summary>
        /// <param name="walls"></param>
        int SetWalls(List<Wall> walls);

    }
}
