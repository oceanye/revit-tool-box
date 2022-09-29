using Autodesk.Revit.UI;
using BIM.Util.Model;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace BIM.Util.Extensions
{
    public static class PushButtonModelExtension
    {

        /// <summary>
        /// 创建 PushButtonData 模板
        /// </summary>
        /// <param name="buttonParamter"> 按钮参数</param>
        /// <returns></returns>
        public static PushButtonData PushButtonData(this PushButtonModel buttonParamter)
        {
            // 按键集合 - 系统名称, 面板名称, dll文件位置, 命名空间引用
            var buttonData = new PushButtonData(buttonParamter.InName, buttonParamter.ButtonName, buttonParamter.AssemblyName, buttonParamter.NameSpace);
            try
            {
                buttonData.ToolTip = buttonParamter.Tooltip;
                buttonData.LargeImage = buttonParamter.ImageName.GetImageBitmapSource();
                buttonData.Image = buttonParamter.StackedImageName.GetImageBitmapSource();
                buttonData.LongDescription = buttonParamter.Description;
                buttonData.ToolTipImage = buttonParamter.TooltipImage?.GetImageBitmapSource();
                buttonData.SetContextualHelp(new ContextualHelp(ContextualHelpType.Url, @"https://www.JGsteel.com/"));
            }
            catch (Exception ex)
            {
                Log.Debug($"未找到图片资源:{ex.Message}");
            }
            return buttonData;
        }

        /// <summary>
        /// Image 转换 BitmapSource
        /// </summary>
        /// <param name="image"> 资源图片</param>
        /// <returns>BitmapSource</returns>
        private static BitmapSource GetImageBitmapSource(this Image image)
        {
            BitmapImage bitmapImage = new BitmapImage();
            using (MemoryStream stream = new MemoryStream())
            {
                image.Save(stream, ImageFormat.Png);
                stream.Position = 0;
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.UriSource = null;
                bitmapImage.StreamSource = stream;
                bitmapImage.EndInit();
            }
            return bitmapImage;
        }
    }
}
