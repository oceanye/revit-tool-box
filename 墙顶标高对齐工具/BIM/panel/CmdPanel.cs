using Autodesk.Revit.UI;
using BIM.Util.Extensions;
using BIM.Util.Model;

namespace BIM.Panel
{
    internal class CmdPanel : IRibbonPanel
    {
        /// <summary>
        /// 版本
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="version"></param>
        public CmdPanel(int version = 2018)
        {
            Version = version;
        }

        /// <summary>
        /// 绑定功能
        /// </summary>
        /// <param name="ribbonPanel"></param>
        public void PushButtons(RibbonPanel ribbonPanel)
        {
            #region 标注详图线
            var pb0 = new PushButtonModel
            {
                InName = "标注详图线",
                ButtonName = "标注\n详图线",
                Tooltip = "自动标注当前视图中可见的详图线",

                AssemblyName = $"BIM.Core.dll",
                NameSpace = typeof(BIM.Core.Dim.CmdDetail).FullName,

                ImageName = BIM.Res.Properties.Resources.diameter_32px,
            };
            #endregion

            // 添加功能
            ribbonPanel.AddItem(pb0.PushButtonData());

        }

    }
}
