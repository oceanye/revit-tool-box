using Autodesk.Revit.UI;
using BIM.Util;

namespace BIM
{
    internal class BoundingApp
    {
        private static readonly string tabName = "BIM工具箱";
        private static readonly string panelName = "常用工具合集";

        /// <summary>
        /// UIControlledApplication
        /// </summary>
        private UIControlledApplication App { get; set; }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="application"></param>
        public BoundingApp(UIControlledApplication application)
        {
            App = application;
        }

        /// <summary>
        /// 初始化功能
        /// </summary>
        public void Init(int version)
        {
            try
            {
                App.CreateRibbonTab(tabName);
                var Panel1 = App.CreateRibbonPanel(tabName, panelName);
                var cmdPanel = new CmdPanel(version);
                cmdPanel.PushButtons(Panel1);
            }
            catch (System.Exception ex)
            {
                Log.Debug($"初始化异常:{ex.Message}");
            }
        }

    }
}
