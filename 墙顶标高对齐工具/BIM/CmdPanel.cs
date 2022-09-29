using Autodesk.Revit.UI;
using BIM.Util.Extensions;
using BIM.Util.Model;

namespace BIM
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
        public CmdPanel(int version)
        {
            Version = version;
        }

        /// <summary>
        /// 绑定功能
        /// </summary>
        /// <param name="ribbonPanel"></param>
        public void PushButtons(RibbonPanel ribbonPanel)
        {

            var pb0 = new PushButtonModel
            {
                ButtonName = "墙对齐",
                Tooltip = "墙顶部底部对齐到楼板或梁面",

                NameSpace = typeof(Core.WallParameter.Cmd).FullName,
                ImageName = Res.Properties.Resources.Painting_A_Wall_32px,
            };

            // 添加功能
            ribbonPanel.AddItem(pb0.PushButtonData());

        }

    }
}
