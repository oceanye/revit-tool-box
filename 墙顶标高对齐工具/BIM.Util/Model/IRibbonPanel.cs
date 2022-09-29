using Autodesk.Revit.UI;

namespace BIM.Util.Model
{
    /// <summary>
    /// Revit 面板接口
    /// </summary>
    public interface IRibbonPanel
    {
        /// <summary>
        /// 创建 PushButton 并添加到面板
        /// </summary>
        /// <param name="ribbonPanel"> 面板</param>
        void PushButtons(RibbonPanel ribbonPanel);

        /// <summary>
        /// 版本
        /// </summary>
        int Version { set; get; }
    }
}
